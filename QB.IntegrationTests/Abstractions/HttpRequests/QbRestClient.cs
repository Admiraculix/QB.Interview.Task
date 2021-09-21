using QB.IntegrationTests.Contracts.Counties.Responses;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QB.IntegrationTests.Abstractions.HttpRequests
{
    public class QbRestClient
    {
        private readonly IRestClient _restClient;
        private readonly IRestRequest _restRequest;
        private CancellationToken cancellationToken = default;

        public QbRestClient(
            IRestClient restClient,
            IRestRequest restRequest)
        {
            _restClient = restClient;
            _restRequest = restRequest;
        }

        public async Task<IEnumerable<CountryResponse>> GetAllCountriesAsync()
        {
            _restClient.BaseUrl = new Uri("https://api.twitter.com/1.1");
            _restRequest.Method = Method.GET;

            var response = await _restClient.GetAsync<IEnumerable<CountryResponse>>(_restRequest, cancellationToken);

            return response;
        }
    }
}
