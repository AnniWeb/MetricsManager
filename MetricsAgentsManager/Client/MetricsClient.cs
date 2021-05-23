using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MetricsAgentsManager.Client.Interfaces;
using MetricsAgentsManager.Client.Request;
using Microsoft.Extensions.Logging;

namespace MetricsAgentsManager.Client
{
    public class MetricsClient : IMetricsClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public MetricsClient(HttpClient client/*, ILogger logger*/)
        {
            _client = client;
            // _logger = logger;
        }

        public async Task<IList<TRe>> GetMetricsByPeriod<TRl, TRe>(GetMetricsByPeriodRequest request) where TRl : IListMetricsResponse<TRe> where TRe : class
        {
            // _logger.LogInformation("Запрос данных клиентом", request);
            var _ = $"{request.ApiHost}/api/{request.ApiMethod}/from/{request.FromTime}/to/{request.ToTime}";
            var uri = new Uri($"{request.ApiHost}/api/{request.ApiMethod}/from/{request.FromTime:u}/to/{request.ToTime:u}");
            var response = await _client.GetFromJsonAsync<TRl>(uri);
            // _logger.LogInformation("Ответ клиента", response);

            return response.Metrics;
        }
    }
}