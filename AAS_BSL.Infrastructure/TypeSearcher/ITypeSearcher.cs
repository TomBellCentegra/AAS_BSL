using System.Reflection;

namespace AAS_BSL.Infrastructure.TypeSearcher;

public interface ITypeSearcher
{
    IList<Assembly> GetAssemblies();

    IEnumerable<Type> ClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

    IEnumerable<Type> ClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies,
        bool onlyConcreteClasses = true);

    IEnumerable<Type> ClassesOfType<T>(bool onlyConcreteClasses = true);
}