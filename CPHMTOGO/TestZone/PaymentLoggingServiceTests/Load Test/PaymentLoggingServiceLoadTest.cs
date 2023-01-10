using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using PaymentLoggingService.Domain;

namespace PaymentLoggingServiceTests.Load_Test;

[TestFixture]
public class PaymentLoggingServiceLoadTest
{
    // HttpClient to send requests
    private HttpClient _client;

    // Number of concurrent threads
    private int _numThreads = 10;

    // Connection string for the PaymentLoggingService
    private string _connectionString = "http://host.docker.internal:5004/";

    // Total number of requests to be sent
    private int _numRequests = 100;

    // Expected response time of the service in milliseconds
    private int _expectedResponseTime = 200;

    // Time to wait between each request during ramp up in milliseconds
    private int _rampUpTime = 15;

    // Time of the test in milliseconds
    private int _expectedTestDuration = 20000;
    
    // Random number generator with fixed seed 
    private Random _random = new Random(42);

    // Possible payment types
    private string[] _types = { "Visa", "MasterCard", "Amex", "Discover" };

    [OneTimeSetUp]
    public void Setup()
    {
        _client = new HttpClient();
    }

    [Test]
    public async Task PaymentLoggingService_LoadTest_Post()
    {
        // Create a Semaphore to limit the number of concurrent connections
        var semaphoreSlim = new SemaphoreSlim(_numThreads);

        // List to store the tasks
        var tasks = new List<Task>();

        // List to store the response times
        var responseTimes = new List<long>();

        // Timer to measure the overall duration of the test
        var timer = new Stopwatch();

        // Start the timer
        timer.Start();

        // Create and start the tasks
        for (int i = 0; i < _numRequests; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                // Wait for a connection slot
                await semaphoreSlim.WaitAsync();

                try
                {
                    // Increase the load gradually
                    await Task.Delay(_rampUpTime * i);

                    // Send a payment logging request to the service
                    // This payment uses a random amount and type from the _types array 
                    var payment = new PaymentLogging
                    {
                        From = Guid.NewGuid(),
                        To = Guid.NewGuid(),
                        Amount = _random.Next(1, 10000),
                        Type = _types[_random.Next(0, _types.Length - 1)]
                    };
                    var content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8,
                        "application/json");
                    
                    // Convert object to json and set the content
                    var json = JsonConvert.SerializeObject(payment);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    content.Headers.ContentLength = json.Length;

                    // Start the timer for this request
                    var requestTimer = new Stopwatch();
                    requestTimer.Start();
                    var response = await _client.PostAsync(_connectionString + "api/PaymentLogging", content);
                    
                    // Stop the timer for this request
                    requestTimer.Stop();

                    // Record the response time
                    responseTimes.Add(requestTimer.ElapsedMilliseconds);

                    // Ensure the request was successful
                    response.EnsureSuccessStatusCode();

                    // Validate the service's response
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PaymentLogging>(jsonResponse);
                    Assert.AreEqual(payment.From, result.From);
                    Assert.AreEqual(payment.To, result.To);
                    Assert.AreEqual(payment.Amount, result.Amount);
                    Assert.AreEqual(payment.Type, result.Type);
                }
                finally
                {
                    // Release the connection slot
                    semaphoreSlim.Release();
                }
            }));
        }

        // Wait for all the tasks to complete
        await Task.WhenAll(tasks);

        // Calculate the standard deviation of the response times
        var standardDeviation = CalculateStandardDeviation(responseTimes);

        // Calculate the max response time
        var maxResponseTime = responseTimes.Max();

        // Assert that the overall test duration is less than or equal to the expected test duration
        Assert.LessOrEqual(timer.ElapsedMilliseconds, _expectedTestDuration, $"Test took longer than expected. Test duration: {timer.ElapsedMilliseconds} milliseconds. Expected test duration: {_expectedTestDuration} milliseconds.");

        // Assert that the average response time is less than or equal to the expected response time
        var averageResponseTime = responseTimes.Average();
        Assert.LessOrEqual(averageResponseTime, _expectedResponseTime, $"Average response time {averageResponseTime} exceeded the expected response time of {_expectedResponseTime} milliseconds.");
        
        // Assert that the standard deviation of the response times is less than or equal to half of the expected response time
        Assert.LessOrEqual(standardDeviation, _expectedResponseTime/2, $"Standard deviation of response times {standardDeviation} exceeded the half of the expected response time of {_expectedResponseTime} milliseconds.");

        // Assert that the maximum response time is less than or equal to twice the expected response time
        Assert.LessOrEqual(maxResponseTime, _expectedResponseTime*2, $"Maximum response time {maxResponseTime} exceeded twice the expected response time of {_expectedResponseTime} milliseconds.");

        // Cleanup
        semaphoreSlim.Dispose();

    }
    private double CalculateStandardDeviation(List<long> values)
    {
        double mean = values.Average();
        double sumOfSquaresOfDifferences = values.Select(val => (val - mean) * (val - mean)).Sum();
        double standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / values.Count);
        return standardDeviation;
    }
}