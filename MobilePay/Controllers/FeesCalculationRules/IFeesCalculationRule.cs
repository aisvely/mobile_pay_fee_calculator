using MobilePay.Models;

namespace MobilePay.Controllers.FeesCalculationRules
{
    public interface IFeesCalculationRule
    {
        FeeModel CalculateFee(decimal tranctionsAmount);
    }
}
