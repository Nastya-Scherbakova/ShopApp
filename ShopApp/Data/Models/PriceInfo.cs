using System;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class PriceInfo
    {
        public int Id {get;set;}
        public decimal Price {get;set;}
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date {get;set;}
        public virtual Item Item {get;set;}
    }
}
