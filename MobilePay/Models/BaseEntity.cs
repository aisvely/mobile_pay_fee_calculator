using System;

namespace MobilePay.Models
{
    public abstract class BaseEntity
    {
        public string merchantName { get; set; }

        public DateTime date { get; set; }
    }
}
