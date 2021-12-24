using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TodoService.Dtos;

namespace TodoServuce.SyncDataServices.Http
{

    public class HttpProjectsDataClient : IHttpProjectsDataClient
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        private readonly string baseUrl;

        public HttpProjectsDataClient(IConfiguration configuration, HttpClient httpClient)
        {
            this.configuration = configuration;
            this.httpClient = httpClient;
            baseUrl = configuration.GetValue<string>("httpProjectsServiceUrl");
            Console.WriteLine($"baseUrl HttpProjectsDataClient: {baseUrl}");
        }

        public async Task SendItemToProject(TodoItemRead data)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{baseUrl}/api/p/todo-items", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("SendItemToProject OK");
            }
            else
            {
                Console.WriteLine("SendItemToProject NOT OK");
                throw new Exception("SendItemToProject NOT OK");
            }
        }
    }
}