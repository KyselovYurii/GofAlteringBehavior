using System;
using System.Collections.Generic;

namespace Calculator.Task2
{
    public class InsurancePaymentCalculator : ICalculator
    {
        private ICurrencyService currencyService;
        private ITripRepository tripRepository;

        public InsurancePaymentCalculator(
            ICurrencyService currencyService,
            ITripRepository tripRepository)
        {
            this.currencyService = currencyService;
            this.tripRepository = tripRepository;
        }

        public decimal CalculatePayment(string touristName)
        {
            var rate = currencyService.LoadCurrencyRate();
            var trip = tripRepository.LoadTrip(touristName);
            return Constants.A * rate * trip.FlyCost
                 + Constants.B * rate * trip.AccomodationCost
                 + Constants.C * rate * trip.ExcursionCost;
        }
    }

    public class CachedInsurancePaymentCalculator : ICalculator
    {
        private readonly ICalculator _calculator;
        private readonly Dictionary<string, decimal> _cache = new Dictionary<string, decimal>();

        public CachedInsurancePaymentCalculator(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public decimal CalculatePayment(string touristName)
        {
            if (_cache.TryGetValue(touristName, out var payment))
            {
                return payment;
            }

            payment = _calculator.CalculatePayment(touristName);
            return _cache[touristName] = payment;
        }
    }
}
