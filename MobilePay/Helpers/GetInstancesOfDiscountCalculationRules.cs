using MobilePay.Controllers.DiscountCalculationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MobilePay.Helpers
{
    public static class GetInstancesOfDiscountCalculationRules
    {
        /// <summary>
        /// Gets list of discount calculation classes instances
        /// </summary>
        /// <returns>list of discount calculation classes instances</returns>
        public static IEnumerable<IDiscountCalculationRule> GetRulesInstances()
        {
            return  Assembly.GetExecutingAssembly()
                    .GetTypes()  
                    .Where(type => typeof(IDiscountCalculationRule).IsAssignableFrom(type))
                    .Where(type =>
                        !type.IsAbstract &&
                        !type.IsGenericType &&
                        type.GetConstructor(new Type[0]) != null) 
                    .Select(type => (IDiscountCalculationRule)Activator.CreateInstance(type)) 
                    .ToList();
        }
    }
}
