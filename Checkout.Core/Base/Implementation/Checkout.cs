using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Base.Implementation
{
    public record PricingRule(int UnitPrice, int? SpecialCount = null, int? SpecialPrice = null);
    public class Checkout : ICheckout
    {
        private readonly Dictionary<string, int> _cart = new();
        private readonly Dictionary<string, PricingRule> _pricingRules;

        public Checkout(Dictionary<string, PricingRule> pricingRules)
        {
            _pricingRules = pricingRules;
        }

        public void Scan(string item)
        {
            _cart[item] = _cart.GetValueOrDefault(item, 0) + 1;
        }

        public int GetTotalPrice()
        {
            int total = 0;

            foreach (var (item, count) in _cart)
            {
                var rule = _pricingRules[item];
                total += rule.SpecialCount switch
                {
                    null or 0 => count * rule.UnitPrice,
                    _ when count >= rule.SpecialCount => (count / rule.SpecialCount.Value) * rule.SpecialPrice.Value +
                                                         (count % rule.SpecialCount.Value) * rule.UnitPrice,
                    _ => count * rule.UnitPrice
                };
            }

            return total;
        }
    }
}
