using Checkout.Core.Base.Implementation;

namespace Checkout.Test
{
    public class TestCheckout
    {
        [Fact]
        public void TestCheckoutTotalPrice()
        {
            var pricingRules = new Dictionary<string, PricingRule>
            {
                ["A"] = new PricingRule(50, 3, 130),
                ["B"] = new PricingRule(30, 2, 45),
                ["C"] = new PricingRule(20),
                ["D"] = new PricingRule(15)
            };

            var checkout = new Core.Base.Implementation.Checkout(pricingRules);
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("B");

            Assert.Equal(95, checkout.GetTotalPrice());
        }

    }
}