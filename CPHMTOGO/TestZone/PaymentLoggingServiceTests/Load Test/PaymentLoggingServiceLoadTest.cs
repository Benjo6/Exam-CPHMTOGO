// <copyright file="PaymentLoggingServiceLoadTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PaymentLoggingServiceTests.Load_Test;

using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using PaymentLoggingService.Domain;

[TestFixture]
public class PaymentLoggingServiceLoadTest
{ 
    // HttpClient object to send requests to the Payment Logging Service
    private HttpClient _client = new();

    // Connection string for the PaymentLoggingService
    private string _connectionString = "http://host.docker.internal:5004/";

    // Possible payment types
    private string[] _types = { "Visa", "MasterCard", "Amex", "Discover" };

    // Random number generator to create random payments
    private Random _random = new Random(42);

    // Delay between requests
    private int delay = 10;
    
    // Semaphore to limit the number of concurrent requests
    private SemaphoreSlim _semaphore = new(50, 50);

    // Number of requests to send to the service
    private int numOfRequests = 1000;
    

    // Count of the requests that returned an error
    private int _errorCount =0;

    // Stopwatch to measure the test duration
    private Stopwatch _stopwatch = new();

    // List to store the response times of requests
    private List<double> _responseTimes =new();
    

    [Test]
    public async Task PaymentLoggingService_LoadTest_Post()
    {
        this._stopwatch.Start();
        var tasks = Enumerable.Range(0, 1)
            .Select(_ => this.PaymentLoggingService_LoadTest_PostSingleRequest())
            .ToList();
        while (tasks.Any())
        {
            var completedTasks = await Task.WhenAny(tasks);
            tasks.Remove(completedTasks);
            await Task.Delay(this.delay);
        }

        this._stopwatch.Stop();
        var testDuration = this._stopwatch.ElapsedMilliseconds;
        var avgResponseTime = this._responseTimes.Average();
        var errorRate = this._errorCount / (double)this.numOfRequests;

        Console.WriteLine($"Test duration: {testDuration} ms");
        //Assert.Less(testDuration, 1000);
        Console.WriteLine($"Average response time: {avgResponseTime} ms");
        //Assert.Less(avgResponseTime, 1000);
        Console.WriteLine($"Error rate: {errorRate}");
        //Assert.Less(errorRate, 0.1);
    }

    [Test]
    public async Task PaymentLoggingService_LoadTest_Post_100()
    {
        this._stopwatch.Start();
        var tasks = Enumerable.Range(0, 100)
            .Select(_ => this.PaymentLoggingService_LoadTest_PostSingleRequest())
            .ToList();
        while (tasks.Any())
        {
            var completedTasks = await Task.WhenAny(tasks);
            tasks.Remove(completedTasks);
            await Task.Delay(this.delay);
        }

        this._stopwatch.Stop();
        var testDuration = this._stopwatch.ElapsedMilliseconds;
        var avgResponseTime = this._responseTimes.Average();
        var errorRate = this._errorCount / (double)this.numOfRequests;

        Console.WriteLine($"Test duration: {testDuration} ms");
        Assert.Less(testDuration, 5000);
        Console.WriteLine($"Average response time: {avgResponseTime} ms");
        Assert.Less(avgResponseTime, 500);
        Console.WriteLine($"Error rate: {errorRate}");
        Assert.Less(errorRate, 0.2);
    }

    [Test]
    public async Task PaymentLoggingService_LoadTest_Post_1000()
    {
        // Start the stopwatch to measure the test duration
        this._stopwatch.Start();

        // Create a list of tasks that will each send a single request to the service
        var tasks = Enumerable.Range(0, 1000)
            .Select(_ => this.PaymentLoggingService_LoadTest_PostSingleRequest())
            .ToList();

        // Continuously check for completed tasks and remove them from the list
        while (tasks.Any())
        {
            var completedTasks = await Task.WhenAny(tasks);
            tasks.Remove(completedTasks);
            await Task.Delay(this.delay);
        }

        // Stop the stopwatch and calculate the test duration
        this._stopwatch.Stop();
        var testDuration = this._stopwatch.ElapsedMilliseconds;
        var avgResponseTime = this._responseTimes.Average();
        var errorRate = this._errorCount / (double)this.numOfRequests;

        Console.WriteLine($"Test duration: {testDuration} ms");
       //Assert.Less(testDuration, 30000);
        Console.WriteLine($"Average response time: {avgResponseTime} ms");
       //Assert.Less(avgResponseTime, 1000);
        Console.WriteLine($"Error rate: {errorRate}");
       //Assert.Less(errorRate, 2);
    }

    private async Task PaymentLoggingService_LoadTest_PostSingleRequest()
    {
        try
        {
            // Wait for a semaphore slot to be available before sending a request
            await this._semaphore.WaitAsync();

            // Start a stopwatch to measure the response time
            var sw = Stopwatch.StartNew();

            // Create a new random payment
            var payment = new PaymentLogging()
            {
                From = Guid.NewGuid(),
                To = Guid.NewGuid(),
                Amount = this._random.Next(1, 10000),
                Type = this._types[this._random.Next(0, this._types.Length - 1)],
            };

            // Convert the payment to JSON and set it as the request content
            var content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");

            // Send a POST request to the service
            var response = await this._client.PostAsync(this._connectionString, content);

            // Check if the request was successful
            if (!response.IsSuccessStatusCode)
            {
                this._errorCount++;
            }

            // Stop the stopwatch and add the response time to the list
            sw.Stop();
            this._responseTimes.Add(sw.ElapsedMilliseconds);
        }
        catch (Exception e)
        {
            // If an exception occurred, increment the error count
            this._errorCount++;
        }
        finally
        {
            // Release the semaphore
            this._semaphore.Release();
        }
    }
}
