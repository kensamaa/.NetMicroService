using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpclient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpclient,IConfiguration config)
        {
            _httpclient = httpclient;
            _configuration = config;
        }

        public async Task SendPlatformToComand(PlatformReadDto plat)
        {
            var httpcontent = new StringContent(
                JsonSerializer.Serialize(plat),
                Encoding.UTF8,
                "application/json"
                );
            var response = await _httpclient.PostAsync($"{_configuration["CommandService"]}", httpcontent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--->sync post to coommande was ok");
            }
            else
            {
                Console.WriteLine("--->sync post to coommande was shit");
            }
        }
    }
}