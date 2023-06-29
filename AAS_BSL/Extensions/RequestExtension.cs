using System.Text;

namespace AAS_BSL.Extensions;

public static class RequestExtension
{
    public static async Task<string> GetRawBodyAsync(
        this HttpRequest request,
        Encoding encoding = null)
    {
        if (!request.Body.CanSeek)
        {
            request.EnableBuffering();
        }

        request.Body.Position = 0;

        var reader = new StreamReader(request.Body, encoding ?? Encoding.UTF8);

        var body = await reader.ReadToEndAsync().ConfigureAwait(false);

        request.Body.Position = 0;

        return body;
    }
}