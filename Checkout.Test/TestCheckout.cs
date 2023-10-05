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
                ["A"] = new(50, 3, 130),
                ["B"] = new(30, 2, 45),
                ["C"] = new(20),
                ["D"] = new(15)
            };

            var checkout = new Core.Base.Implementation.Checkout(pricingRules);
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("B");

            Assert.Equal(95, checkout.GetTotalPrice());
        }
        [Fact]
        public void TestInvalidPricingRule()
        {
            var pricingRules = new Dictionary<string, PricingRule>
            {
                ["A"] = new(-50),
            };

            var checkout = new Core.Base.Implementation.Checkout(pricingRules);
            checkout.Scan("A");

            Assert.Throws<InvalidOperationException>(() => checkout.GetTotalPrice());
        }

        [Fact]
        public void TestInvalidSKU()
        {
            var pricingRules = new Dictionary<string, PricingRule>
            {
                ["A"] = new(50, 3, 130),
            };

            var checkout = new Core.Base.Implementation.Checkout(pricingRules);

            Assert.Throws<ArgumentException>(() => checkout.Scan("Z"));
        }
    }
}