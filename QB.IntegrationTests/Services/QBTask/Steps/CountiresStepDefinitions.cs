using FluentAssertions;
using QB.IntegrationTests.Abstractions.HttpRequests;
using QB.IntegrationTests.Constants;
using QB.IntegrationTests.Contracts.Counties.Responses;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace QB.IntegrationTests.Services.QBTask.Steps
{
    [Binding]
    public sealed class CountiresStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private readonly HttpClient _httpClient;
        private readonly QbRestClient _qbRestClient;
        private readonly IRestClient _restClient = new RestClient();
        private readonly IRestRequest _restRequest = new RestRequest();
        private CancellationToken cancellationToken = default;

        public CountiresStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            _scenarioContext = scenarioContext;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CountryResponse>> GetAllCountriesAsync()
        {
            _restClient.BaseUrl = new Uri($"http://localhost:5000/{Endpoints.GetCountries}");
            _restRequest.Method = Method.GET;

            var response = await _restClient.GetAsync<IEnumerable<CountryResponse>>(_restRequest, cancellationToken);

            return response;
        }

        [Given("I have country with id (.*) and name (.*)")]
        public void  GivenTheFirstNumberIs(int id, string name)
        {
            var countryEntity = new CountryResponse
            {
                CountryId = id,
                CountryName = name
            };

            _scenarioContext.Add("CountrUsa", countryEntity);
        }

        [When("I get all countries")]
        public async Task WhenIGetAllCountriesAsync()
        {
            var countries = await GetAllCountriesAsync();
            _scenarioContext.Add("Countries", countries.ToList());

            //using var response = await _httpClient.GetAsync($"{Endpoints.GetCountries}", HttpCompletionOption.ResponseHeadersRead);
            //var countries = await test.Content.ReadAsStringAsync();

            //var countries = _qbRestClient.GetAllCountriesAsync();
        }

        [Then("the result list should be greater than (.*)")]
        public void ThenTheResultShouldBeGreaterThan(int result)
        {
           var countryList = (List<CountryResponse>)_scenarioContext["Countries"];
           var countryUsa = (CountryResponse)_scenarioContext["CountrUsa"];

            countryList.Should().NotBeEmpty()
                .And.HaveCount(x => x > result)
                .And.OnlyHaveUniqueItems()
                .And.Contain(x=>x.CountryId == countryUsa.CountryId);
        }
    }
}
