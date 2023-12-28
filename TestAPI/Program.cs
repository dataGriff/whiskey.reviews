using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using HttpClient client = new HttpClient();
        var response = await client.GetAsync("http://localhost:8000/v1/distilleries");
        string responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
        Console.WriteLine("test");
    }
}

