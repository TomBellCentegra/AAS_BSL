using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using AAS_BSL.Domain.Dtos;
using AAS_BSL.Domain.Enums;

namespace AAS_BSL.Services.HttpClient;

public class BslHttpClient: IBslHttpClient
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public BslHttpClient(System.Net.Http.HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<StatusResult> Subscribe(SubscriptionRequest request)
    {
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://api.ncr.com/transaction-document/transaction-documents/20230317-572122-10100017"),
        };
        var signature = CalculateSignature(httpRequestMessage, request);
        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("AccessKey", signature);
        httpRequestMessage.Headers.Add("nep-organization", request.NepOrganization);
        httpRequestMessage.Headers.Host = "api.ncr.com";
        
        var response = await _httpClient.SendAsync(httpRequestMessage);
        var responseBody = await response.Content.ReadAsStringAsync();
        
        return response.IsSuccessStatusCode ? 
            new StatusResult { Status = Status.Done, Message = responseBody } : 
            new StatusResult { Status = Status.Failed, Message = responseBody };
        
    }

    private string CalculateSignature(HttpRequestMessage requestMessage, SubscriptionRequest request)
    {
        var date = DateTime.UtcNow;
        requestMessage.Headers.Add("date", date.ToString("r"));
        var key = UniqueKey(date, request.SecretKey);
        var sc = SignableContent(requestMessage,requestMessage.RequestUri.ToString(),request.NepOrganization);
        var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
        var signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(sc));
        var signature = Convert.ToBase64String(signatureBytes);

        return $"{request.SharedKey}:{signature}";
    }
    private string UniqueKey(DateTime date, string secretKey)
    {
        var nonce = date.ToString("yyyy-MM-ddTHH:mm:ss") + ".000Z";
        return secretKey + nonce;
    }
    private string SignableContent(HttpRequestMessage requestMessage, string url, string organizationId)
    {
        var requestPath = ReplaceHost(url);

        var paramsList = new[]
            { requestMessage.Method.Method.ToUpper(), requestPath, organizationId };
        return string.Join("\n", paramsList.Where(p => !string.IsNullOrEmpty(p)));
    }
    private string ReplaceHost(string original)
    {
        var builder = new UriBuilder(original);
        return builder.Path;
    }
}