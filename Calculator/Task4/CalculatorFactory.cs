using System;

namespace Calculator.Task4
{
    public class CalculatorFactory : ICalculatorFactory
    {
        private ICurrencyService currencyService;
        private ITripRepository tripRepository;
        private ILogger logger;

        public CalculatorFactory(
            ICurrencyService currencyService,
            ITripRepository tripRepository,
            ILogger logger)
        {
            this.currencyService = currencyService;
            this.tripRepository = tripRepository;
            this.logger = logger;
        }

        public ICalculator CreateCalculator()
        {
            return new InsurancePaymentCalculator(currencyService, tripRepository);
        }

        public ICalculator CreateCalculator(bool withLogging, bool withCaching, bool withRounding)
        {
            ICalculator calculator = CreateCalculator();

            if (withLogging)
            {
                calculator = new LoggingCalculatorDecorator(logger, calculator);
            }

            if (withCaching)
            {
                calculator = new CachedPaymentDecorator(calculator);
            }

            if (withRounding)
            {
                calculator = new RoundingCalculatorDecorator(calculator);
            }

            return calculator;
        }
    }
}
