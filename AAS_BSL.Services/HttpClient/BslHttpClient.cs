using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using AAS_BSL.Domain.Dtos;
using AAS_BSL.Domain.Enums;
using AAS_BSL.Domain.Subscription;
using Newtonsoft.Json;

namespace AAS_BSL.Services.HttpClient;

public class BslHttpClient : IBslHttpClient
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public BslHttpClient(System.Net.Http.HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<StatusResult> Subscribe(SubscriptionRequestDto request)
    {
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.ncr.com/transaction-document/v2/subscriptions")
        };
        var signature = CalculateSignature(httpRequestMessage, request);
        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("AccessKey", signature);
        httpRequestMessage.Headers.Add("nep-organization", request.NepOrganization);
        httpRequestMessage.Headers.Host = "api.ncr.com";
            //AccessKey 2b3f21f652584c12af034e2ec0227a07:WUuqYJ3iiARG1Tb0E3CY0n2fobpZ7pTle13y37j7D+3FUiJq+w+bsyhmzpBIWRNHFO9/ZZLcTVtTmJfNRyKeTQ==
            //          2b3f21f652584c12af034e2ec0227a07:PGxQeZlOXRj5T332/A4NzVyL0+8mWzI4NznsfJbx1QmG9uez7Oey9xjyiMtskN5lLqc/SFdxR/p9W3K5nXwXpA==

        var subscription = new SubscriptionRequest()
        {
            //authenticationCredentials = new AuthenticationCredential()
            //{
            //    authenticationType = "BASIC",
            //    credentials = "token"
            //},
            name = request.CompanyName,
            description = $"Subscription for {request.CompanyName}",
            endpoint = new Endpoint
            {
                name = "CentegraBSL",
                description = "Centegra BSL integration",
                destinationUrl = "https://aasbsl-dev.azurewebsites.net/bsl/subscribe"
            },
            topicId = new TdmTopicIdData
            {
                name = "tlog_ext_received"
            }
        };

        var subscriptionJson = JsonConvert.SerializeObject(subscription);
        httpRequestMessage.Content = new StringContent(subscriptionJson, Encoding.UTF8);
        httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await _httpClient.SendAsync(httpRequestMessage);
        var responseBody = await response.Content.ReadAsStringAsync();

        return response.IsSuccessStatusCode
            ? new StatusResult { Status = Status.Done, Message = responseBody }
            : new StatusResult { Status = Status.Failed, Message = responseBody };
    }

    private string CalculateSignature(HttpRequestMessage requestMessage, SubscriptionRequestDto request)
    {
        var date = DateTime.UtcNow;
        requestMessage.Headers.Add("date", date.ToString("r"));
        var key = UniqueKey(date, request.SecretKey);
        var sc = SignableContent(requestMessage, requestMessage.RequestUri.ToString(), request.NepOrganization);
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