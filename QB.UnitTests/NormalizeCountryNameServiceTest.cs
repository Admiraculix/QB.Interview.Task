using FluentAssertions;
using Moq;
using QB.Application.Configurations;
using QB.Application.Dtos;
using QB.Application.Interfaces.InfrastuctureServices;
using QB.Application.Interfaces.Repositories;
using QB.Application.Services.Utility;
using QB.Domain.Models;
using QB.UnitTests.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace QB.UnitTests
{
    public class NormalizeCountryNameServiceTest : BaseSetupTest
    {
        private readonly CountryNameVariationConfiguration _countryNameVariationConfiguration = new CountryNameVariationConfiguration();
        private readonly List<string> _countryNameVariationList = new List<string> { "US", "United States of America", "USA" };

        public NormalizeCountryNameServiceTest()
        {
            typeof(CountryNameVariationConfiguration)
            .GetProperty(nameof(_countryNameVariationConfiguration.UsaNames))
            .SetValue(_countryNameVariationConfiguration, _countryNameVariationList);
        }

        [Fact]
        public async Task Successfully_Normalize_Usa_Country_Name()
        {
            //Arrange
            var countryEntity = new Country
            {
                CountryId = 1,
                CountryName = "U.S.A."
            };

            var statResult = new List<Tuple<string, int>>
            {
              Tuple.Create("Lithuania",3329039),
              Tuple.Create("United States of America",309349689)
            };

            var mockRepository = new Mock<ICountryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockStatService = new Mock<IStatExternalService>();

            mockRepository.Setup(r => r.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(countryEntity)
                .Callback(async () => await Task.FromResult(countryEntity));

            mockUnitOfWork.Setup(uow => uow.Countries).Returns(mockRepository.Object);

            mockStatService.Setup(stat => stat.GetCountryPopulationsAsync())
                .ReturnsAsync(statResult)
                .Callback(async () => await Task.FromResult(statResult));

            //Act
            var sut = new NormalizeCountryNameService(
                        mockUnitOfWork.Object,
                        _mapper,
                        mockStatService.Object,
                        _countryNameVariationConfiguration);

            var resultCollection = await sut.NormalizeUsaCountryNameAsync();

            //Assert
            resultCollection.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.OnlyHaveUniqueItems()
                .And.ContainItemsAssignableTo<CountryPopulationDto>()
                .And.ContainSingle(x => x.CountryName == countryEntity.CountryName);
        }
    }
}
