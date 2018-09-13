using System;

namespace ShopApp.Models
{
    public class PriceInfo
    {
        public int Id {get;set;}
        public decimal Price {get;set;}
        public DateTime Date {get;set;}
        public virtual Item Item {get;set;}
    }
}
