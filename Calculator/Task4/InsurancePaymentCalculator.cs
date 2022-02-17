using System.Collections.Generic;

namespace Calculator.Task4
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
            var tripDetails = tripRepository.LoadTrip(touristName);
            return Constants.A * rate * tripDetails.FlyCost
                 + Constants.B * rate * tripDetails.AccomodationCost
                 + Constants.C * rate * tripDetails.ExcursionCost;
        }
    }

    public class RoundingCalculatorDecorator : ICalculator
    {
        private readonly ICalculator _calculator;

        public RoundingCalculatorDecorator(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public decimal CalculatePayment(string touristName)
        {
            return decimal.Round(_calculator.CalculatePayment(touristName));
        }
    }

    public class LoggingCalculatorDecorator : ICalculator
    {
        private readonly ILogger _logger;
        private readonly ICalculator _calculator;

        public LoggingCalculatorDecorator(ILogger logger, ICalculator calculator)
        {
            _logger = logger;
            _calculator = calculator;
        }

        public decimal CalculatePayment(string touristName)
        {
            _logger.Log("Start");
            var payment = _calculator.CalculatePayment(touristName);
            _logger.Log("End");
            return payment;
        }
    }

    public class CachedPaymentDecorator : ICalculator
    {
        private readonly ICalculator _calculator;
        private readonly Dictionary<string, decimal> _cache = new Dictionary<string, decimal>();

        public CachedPaymentDecorator(ICalculator calculator)
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