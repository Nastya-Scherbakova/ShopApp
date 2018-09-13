using System.Collections.Generic;


namespace ShopApp.Models
{
    public class Item
    {
        public int Id {get;set;}
        public string Title {get; set;}
        public string About {get;set;}
        public string Image {get;set;}
        public decimal CurrentPrice{get;set;}
        public virtual IEnumerable<PriceInfo> PriceHistory {get;set;}

        public Item()
        {
            PriceHistory = new List<PriceInfo>();
        }
    }

}
