using MobilePay.Controllers.FeesCalculationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MobilePay.Helpers
{
    public static class GetInstancesOfFeesCalculationRules
    {
        /// <summary>
        /// Gets list of fees calculation classes instances
        /// </summary>
        /// <returns>list of fees calculation classes instances</returns>
        public static IEnumerable<IFeesCalculationRule> GetRulesInstances()
        {
            return  Assembly.GetExecutingAssembly()
                    .GetTypes()  
                    .Where(type => typeof(IFeesCalculationRule).IsAssignableFrom(type))
                    .Where(type =>
                        !type.IsAbstract &&
                        !type.IsGenericType &&
                        type.GetConstructor(new Type[0]) != null)
                    .Select(type => (IFeesCalculationRule)Activator.CreateInstance(type))
                    .ToList();
        }
    }
}
