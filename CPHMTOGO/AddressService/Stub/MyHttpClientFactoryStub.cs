using Moq;
using Moq.Protected;

namespace AddressService.Stub;

public class MyHttpClientFactoryStub : IHttpClientFactory
{
    private readonly Dictionary<string, HttpResponseMessage> _responses;
    private HttpClient _mockHttpClient;

    public MyHttpClientFactoryStub()
    {
        _responses = new Dictionary<string, HttpResponseMessage>();
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                return _responses[request.RequestUri.ToString()];
            });
        _mockHttpClient = new HttpClient(mockHttpMessageHandler.Object);
    }

    public void SetupGetAsync(string endpoint, HttpResponseMessage response)
    {
        _responses[endpoint] = response;
    }

    public HttpClient CreateClient(string name)
    {
        _mockHttpClient.BaseAddress = new Uri("https://api.dataforsyningen.dk/");
        return _mockHttpClient;
    }
}