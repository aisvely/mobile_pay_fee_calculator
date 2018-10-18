namespace MobilePay.Models
{
    public class FeeModel : BaseEntity
    {
        public decimal feesAmount { get; set; }

        public decimal additionalFeesAmount { get; set; }

        public decimal discountAmount { get; set; }
    }
}
