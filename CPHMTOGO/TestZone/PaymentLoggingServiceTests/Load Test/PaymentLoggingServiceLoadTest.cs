namespace PaymentLoggingServiceTests.Load_Test;

using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using PaymentLoggingService.Domain;

[TestFixture]
public class PaymentLoggingServiceLoadTest
{ 
    // HttpClient object to send requests to the Payment Logging Service
    private HttpClient _client;
    
    // Connection string for the PaymentLoggingService
    private string _connectionString = "http://host.docker.internal:5004/";
    
    // Possible payment types
    private string[] _types = { "Visa", "MasterCard", "Amex", "Discover" };
    
    // Random number generator to create random payments
    private Random _random = new Random(42);

    // Delay between requests 
    private int delay;
    
    // Semaphore to limit the number of concurrent requests
    private SemaphoreSlim _semaphore;
    
    // Number of requests to send to the service
    private int numOfRequests;

    // Number of concurrent requests to send to the service
    private int numOfConcurrentRequests;

    
    // Count of the requests that returned an error
    private int _errorCount;
    
    // Stopwatch to measure the test duration
    private Stopwatch _stopwatch;
    
    // List to store the response times of requests
    private List<double> _responseTimes;

    [OneTimeSetUp]
    public void Setup()
    {
        _client = new HttpClient();
        numOfRequests = 1000;
        numOfConcurrentRequests = 50;
        delay = 10;
        _semaphore = new SemaphoreSlim(numOfConcurrentRequests, numOfConcurrentRequests);
        _stopwatch = new Stopwatch();
        _responseTimes = new List<double>();
    }

    [Test]
    public async Task PaymentLoggingService_LoadTest_Post()
    {
        _stopwatch.Start();
        var tasks = Enumerable.Range(0, 1)
            .Select(i => this.PaymentLoggingService_LoadTest_PostSingleRequest())
            .ToList();
        while (tasks.Any())
        {
            var completedTasks = await Task.WhenAny(tasks);
            tasks.Remove(completedTasks);
            await Task.Delay(delay);
        }
        _stopwatch.Stop();
        var testDuration = _stopwatch.ElapsedMilliseconds;
        var avgResponseTime = _responseTimes.Average();
        var errorRate = _errorCount / (double)numOfRequests;

        Console.WriteLine($"Test duration: {testDuration} ms");
        Assert.Less(testDuration,1000);
        Console.WriteLine($"Average response time: {avgResponseTime} ms");
        Assert.Less(avgResponseTime,1000);
        Console.WriteLine($"Error rate: {errorRate}");
        Assert.Less(errorRate,0.1);
    }    


    [Test]
    public async Task PaymentLoggingService_LoadTest_Post_100()
    {
        _stopwatch.Start();
        var tasks = Enumerable.Range(0, 100)
            .Select(i => PaymentLoggingService_LoadTest_PostSingleRequest())
            .ToList();
        while (tasks.Any())
        {
            var completedTasks = await Task.WhenAny(tasks);
            tasks.Remove(completedTasks);
            await Task.Delay(delay);
        }
        _stopwatch.Stop();
        var testDuration = _stopwatch.ElapsedMilliseconds;
        var avgResponseTime = _responseTimes.Average();
        var errorRate = _errorCount / (double)numOfRequests;

        Console.WriteLine($"Test duration: {testDuration} ms");
        Assert.Less(testDuration,5000);
        Console.WriteLine($"Average response time: {avgResponseTime} ms");
        Assert.Less(avgResponseTime,500);
        Console.WriteLine($"Error rate: {errorRate}");
        Assert.Less(errorRate,0.2);
    }

    [Test]
    public async Task PaymentLoggingService_LoadTest_Post_1000()
    {
        // Start the stopwatch to measure the test duration
        _stopwatch.Start();
        
        // Create a list of tasks that will each send a single request to the service
        var tasks = Enumerable.Range(0, 1000)
            .Select(i => PaymentLoggingService_LoadTest_PostSingleRequest())
            .ToList();
        
        // Continuously check for completed tasks and remove them from the list
        while (tasks.Any())
        {
            var completedTasks = await Task.WhenAny(tasks);
            tasks.Remove(completedTasks);
            await Task.Delay(delay);
        }
        
        // Stop the stopwatch and calculate the test duration
        _stopwatch.Stop();
        var testDuration = _stopwatch.ElapsedMilliseconds;
        var avgResponseTime = _responseTimes.Average();
        var errorRate = _errorCount / (double)numOfRequests;
        
        Console.WriteLine($"Test duration: {testDuration} ms");
        Assert.Less(testDuration,30000);
        Console.WriteLine($"Average response time: {avgResponseTime} ms");
        Assert.Less(avgResponseTime,1000);
        Console.WriteLine($"Error rate: {errorRate}");
        Assert.Less(errorRate,2);
    }
    private async Task PaymentLoggingService_LoadTest_PostSingleRequest()
    {
        try
        {
            // Wait for a semaphore slot to be available before sending a request
            await _semaphore.WaitAsync();
            
            // Start a stopwatch to measure the response time
            var sw = Stopwatch.StartNew();

            // Create a new random payment
            var payment = new PaymentLogging()
            {
                From = Guid.NewGuid(),
                To = Guid.NewGuid(),
                Amount = _random.Next(1, 10000),
                Type = _types[_random.Next(0, _types.Length - 1)]
            };
            
            // Convert the payment to JSON and set it as the request content
            var content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");
            
            // Send a POST request to the service
            var response = await _client.PostAsync(_connectionString, content);
            
            // Check if the request was successful
            if (!response.IsSuccessStatusCode)
            {
                _errorCount++;
            }
            
            // Stop the stopwatch and add the response time to the list
            sw.Stop();
            _responseTimes.Add(sw.ElapsedMilliseconds);
        }
        catch(Exception e)
        {
            // If an exception occurred, increment the error count
            _errorCount++;
        }
        finally
        {
            // Release the semaphore
            _semaphore.Release();
        }
    }
}



