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
            if (!_pricingRules.ContainsKey(item))
            {
                throw new ArgumentException($"Invalid SKU: {item} is not recognized.");
            }
            _cart[item] = _cart.GetValueOrDefault(item, 0) + 1;
        }

        public int GetTotalPrice()
        {
            int total = 0;

            foreach (var (item, count) in _cart)
            {
                if (!_pricingRules.TryGetValue(item, out var rule))
                {
                    throw new InvalidOperationException($"Pricing rule for SKU: {item} is missing.");
                }

                if (rule.UnitPrice < 0 || (rule.SpecialCount.HasValue && (rule.SpecialCount <= 0 || rule.SpecialPrice <= 0)))
                {
                    throw new InvalidOperationException($"Invalid pricing rule fr SKU: {item}.");
                }

                rule = _pricingRules[item];
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
