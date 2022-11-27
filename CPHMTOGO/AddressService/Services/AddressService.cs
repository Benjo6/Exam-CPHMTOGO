namespace AddressService.Services
{
    public class AddressService
    {
        private readonly IHttpClientFactory _factory;
        private HttpClient _clientDawa;

        public AddressService(IHttpClientFactory factory)
        {
            _factory = factory;
            _clientDawa = _factory.CreateClient("Dawa");
            
        }
    }
}
