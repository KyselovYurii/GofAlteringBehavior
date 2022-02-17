using System;

namespace Calculator.Task1
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
}
