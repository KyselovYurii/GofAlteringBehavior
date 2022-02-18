using System;

namespace TemplateMethod.Task1
{
    public class MasalaCooker
    {
        private ICooker cooker;

        public MasalaCooker(ICooker cooker)
        {
            this.cooker = cooker;
        }

        public void CookMasala(Country country)
        {
            CookRice(country);
            CookChicken(country);
            CookTea(country);
        }

        private void CookRice(Country country)
        {
            if (country == Country.India)
            {
                cooker.FryRice(200, Level.Strong);
                cooker.SaltRice(Level.Strong);
                cooker.PepperRice(Level.Strong);
            }
            else if (country == Country.Ukraine)
            {
                cooker.FryRice(500, Level.Strong);
                cooker.SaltRice(Level.Strong);
                cooker.PepperRice(Level.Low);
            }
        }

        private void CookChicken(Country country)
        {
            if (country == Country.India)
            {
                cooker.FryChicken(100, Level.Strong);
                cooker.SaltChicken(Level.Strong);
                cooker.PepperChicken(Level.Strong);
            }
            else if (country == Country.Ukraine)
            {
                cooker.FryChicken(300, Level.Medium);
                cooker.SaltChicken(Level.Medium);
                cooker.PepperChicken(Level.Low);
            }
        }

        private void CookTea(Country country)
        {
            if (country == Country.India)
            {
                cooker.PrepareTea(15, TeaKind.Green, 12);
            }
            else if (country == Country.Ukraine)
            {
                cooker.PrepareTea(10, TeaKind.Black, 10);
            }
        }
    }
}
