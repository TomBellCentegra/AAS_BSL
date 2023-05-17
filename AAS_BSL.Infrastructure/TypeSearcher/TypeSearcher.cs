using System.Diagnostics;
using System.Reflection;

namespace AAS_BSL.Infrastructure.TypeSearcher;

public class TypeSearcher : ITypeSearcher
{

    public IEnumerable<Type> ClassesOfType<T>(bool onlyConcreteClasses = true)
    {
        return ClassesOfType(typeof(T), onlyConcreteClasses);
    }

    public IEnumerable<Type> ClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
    {
        return ClassesOfType(assignTypeFrom, GetAssemblies(), onlyConcreteClasses);
    }

    public IEnumerable<Type> ClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies,
        bool onlyConcreteClasses = true)
    {
        var result = new List<Type>();
        try
        {
            foreach (var a in assemblies)
            {
                Type[] types = null;
                types = a.GetTypes();
                if (types == null)
                    continue;

                foreach (var t in types)
                {
                    if (!assignTypeFrom.IsAssignableFrom(t) && (!assignTypeFrom.IsGenericTypeDefinition ||
                                                                !DoesTypeImplementOpenGeneric(t, assignTypeFrom)))
                        continue;

                    if (t.IsInterface)
                        continue;

                    if (onlyConcreteClasses)
                    {
                        if (t.IsClass && !t.IsAbstract)
                        {
                            result.Add(t);
                        }
                    }
                    else
                    {
                        result.Add(t);
                    }
                }
            }
        }
        catch (ReflectionTypeLoadException ex)
        {
            var msg = string.Empty;
            foreach (var e in ex.LoaderExceptions)
                msg += e.Message + Environment.NewLine;

            var fail = new Exception(msg, ex);
            Debug.WriteLine(fail.Message, fail);

            throw fail;
        }

        return result;
    }
    
    protected virtual bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
    {
        try
        {
            var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
            foreach (var implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
            {
                if (!implementedInterface.IsGenericType)
                    continue;

                var isMatch = genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                return isMatch;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public virtual IList<Assembly> GetAssemblies()
    {
        return AssembliesInAppDomain();
    }

    #region Utilities

    private IList<Assembly> AssembliesInAppDomain()
    {
        var addedAssemblyNames = new List<string>();
        var assemblies = new List<Assembly>();
        var currentAssem = Assembly.GetExecutingAssembly();
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            var product = assembly.GetCustomAttribute<AssemblyProductAttribute>();
            var referencedAssemblies = assembly.GetReferencedAssemblies().ToList();
            if (referencedAssemblies.Where(x => x.FullName == currentAssem.FullName).Any()
                || product?.Product == "aas_deliveroo_v2")
            {
                if (!addedAssemblyNames.Contains(assembly.FullName))
                {
                    assemblies.Add(assembly);
                    addedAssemblyNames.Add(assembly.FullName);
                }
            }
        }

        return assemblies;
    }
    
    protected virtual void LoadMatchingAssemblies()
    {
        var loadedAssemblyNames = new List<string>();
        foreach (Assembly a in GetAssemblies())
        {
            loadedAssemblyNames.Add(a.FullName);
        }
    }

    #endregion
}
