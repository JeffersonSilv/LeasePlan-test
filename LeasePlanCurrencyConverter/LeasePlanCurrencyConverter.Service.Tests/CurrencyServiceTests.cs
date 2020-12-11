using System;
using LeasePlanCurrencyConverter.Adapter;
using LeasePlanCurrencyConverter.Data;
using LeasePlanCurrencyConverter.Domain;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeasePlanCurrencyConverter.Service.Tests
{
    public class CurrencyServiceTests
    {
        private ICurrencyService _sut;
        private Mock<ICurrencyRepository> _repository;
        private Mock<IExchangeRateAdapter> _adapter;

        private const double EuroToDolarExchangeRate =  1.21;
        private const double DolarToEuroExchangeRate = 0.83;
        private const string EuroCode = "EUR";
        private const string DolarCode = "USD";

        List<Currency> _currencies = new List<Currency>
        {
            new Currency() {Code = EuroCode, Description = "Euro"},
            new Currency() {Code = DolarCode, Description = "Dolar"}
        };


        [SetUp]
        public void Setup()
        {
            _repository = SetUpCurrencyRepository();
            _adapter = SetUpExchangeRateAdapter();

            _sut = new CurrencyService(_repository.Object,
                _adapter.Object);
        }

        private Mock<ICurrencyRepository> SetUpCurrencyRepository()
        {
            var mock = new Mock<ICurrencyRepository>();
            mock
                .Setup(s => s.GetCurrenciesAsync())
                .Returns(Task.FromResult<IEnumerable<Currency>>(_currencies));

            return mock;
        }

        private Mock<IExchangeRateAdapter> SetUpExchangeRateAdapter()
        {
            var mock = new Mock<IExchangeRateAdapter>();
            mock
                .Setup(s => s.GetExchangeRateAsync(
                    It.Is<string>(s1 => s1.Equals(EuroCode)),
                    It.Is<string>(s1 => s1.Equals(DolarCode))))
                .Returns(Task.FromResult(EuroToDolarExchangeRate));

            mock
                .Setup(s => s.GetExchangeRateAsync(
                    It.Is<string>(s1 => s1.Equals(DolarCode)), 
                    It.Is<string>(s1 => s1.Equals(EuroCode))))
                .Returns(Task.FromResult(DolarToEuroExchangeRate));

            return mock;
        }

        [Test]
        public async Task ShouldReturnListContainingCurrencies()
        {
            var result = await _sut.GetCurrenciesAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.GreaterThan(0));
            Assert.That(result, Is.EquivalentTo(_currencies));

            Assert.Pass();
        }


        [TestCase(EuroCode, DolarCode, EuroToDolarExchangeRate)]
        [TestCase(DolarCode, EuroCode, DolarToEuroExchangeRate)]

        public void ShouldCalculateExchangeRate(string currencyCodeFrom, string currencyCodeTo, double exchangeRate)
        {
            var r = new Random();
            var amount = r.NextDouble();
            var result = _sut.GetConvertedValueAsync(currencyCodeFrom, currencyCodeTo, amount).Result;

            var expectedResult = amount * exchangeRate;

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.GreaterThan(0));
            Assert.That(result, Is.EqualTo(expectedResult));

            Assert.Pass();
        }
    }
}