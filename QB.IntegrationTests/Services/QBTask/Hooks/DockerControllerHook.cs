using BoDi;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Microsoft.Extensions.Configuration;
using QB.IntegrationTests.Constants;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace QB.IntegrationTests.Services.QBTask.Hooks
{
    [Binding]
    public class DockerControllerHook
    {
        private static ICompositeService _compositeService;
        private IObjectContainer _objectContainer;

        public DockerControllerHook(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void DockerComposeUp()
        {
            var config = LoadConfiguration();
            var dockerComposeFileName = config["DockerComposeFileName"];
            var dockerComposePath = GetDockerComposeLocation(dockerComposeFileName);
            var confirmationUrl = config["QB.API:BaseAddress"];
            _compositeService = new Builder()
            .UseContainer()
            .UseCompose()
            .FromFile(dockerComposePath)
            .RemoveOrphans()
            .WaitForHttp("webapi", $"{confirmationUrl}/{Endpoints.GetCountries}",
            continuation: (response, _) => response.Code != HttpStatusCode.OK ? 2000 : 0)
            .Build().Start();
        }

        [AfterTestRun]
        public static void DockerComposeDown()
        {
            _compositeService.Stop();
            _compositeService.Dispose();
        }

        //[BeforeScenario]
        //public void AddHttpClientRestSharp()
        //{
        //    _objectContainer.RegisterTypeAs<RestClient, IRestClient>();
        //    _objectContainer.RegisterTypeAs<RestRequest, IRestRequest>();
        //    //_objectContainer.RegisterTypeAs<QbRestClient, QbRestClient>();
        //}

        [BeforeScenario]
        public void AddHttpClientNative()
        {
            var config = LoadConfiguration();
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(config["QB.API:BaseAddress"])
            };
            _objectContainer.RegisterInstanceAs(httpClient);
        }

        private static IConfiguration LoadConfiguration()
        {
            var configuration = new ConfigurationBuilder()
           .AddJsonFile(Path.Combine("Services", "QBTask", "appsettings.json"))
           .Build();

            return configuration;
        }

        private static string GetDockerComposeLocation(string dockerComposeFileName)
        {
            var directory = Directory.GetCurrentDirectory();
            while (!Directory.EnumerateFiles(directory, "*.yml").Any(s => s.EndsWith(dockerComposeFileName)))
            {
                directory = directory.Substring(0, directory.LastIndexOf(Path.DirectorySeparatorChar));
            }

            return Path.Combine(directory, dockerComposeFileName);
        }
    }
}

