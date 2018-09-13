using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;
using HtmlAgilityPack;
using System.Net.Http;
using System.IO;
using ShopApp.Data;

namespace ShopApp.Services
{

    public class HtmlParser
    {
        /// <summary>
        /// Parsing html using HtmlAgilityPack and updating given context
        /// </summary>
        /// <param name="context"> context of current data from ItemsController </param>
        /// <returns></returns>
        public async Task Parse(ShopAppContext context)
        {
            
            
            HttpClient hc = new HttpClient();
            HttpResponseMessage result = await hc.GetAsync("https://store.nike.com/us/en_us/pw/womens-best/7ptZpi1?ipp=100");

            Stream stream = await result.Content.ReadAsStreamAsync();

            HtmlDocument doc = new HtmlDocument();

            doc.Load(stream);

            HtmlNodeCollection productNodes = doc.DocumentNode.SelectNodes("//div[@class='exp-product-wall clearfix']/div[@class='grid-item fullSize'][position()<100]");

            var titles = productNodes.Select(c1 => c1.SelectSingleNode("//div[@class='product-name ']/p[last()-1]")).ToList();
            var abouts = productNodes.Select(c1 => c1.SelectSingleNode("//div[@class='product-name ']/p[last()]")).ToList();
            var images = productNodes.Select(c1 => c1.SelectSingleNode("//div[@class='grid-item-image']//a[@href]")).ToList();
            var prices = productNodes.Select(c1 => c1.SelectSingleNode("//div[@class='prices']/span[last()]")).ToList();



            await HandleParsedDataAsync(context, titles, abouts, images, prices);
            

        }


        /// <summary>
        /// Adding parsed items in context or update them with new price
        /// </summary>
        /// <param name="context"> context from ItemsController </param>
        /// <param name="titles"> all items titles </param>
        /// <param name="abouts"> all items about infos </param>
        /// <param name="images"> all items image hrefs </param>
        /// <param name="prices"> all item prices with "$" at the beginning (because of website structure and info) </param>
        /// <returns></returns>
        private async Task HandleParsedDataAsync(ShopAppContext context,
            List<HtmlNode> titles, 
            List<HtmlNode> abouts,
            List<HtmlNode> images,
            List<HtmlNode> prices)
        {
            IEnumerable<Item> items = context.Items;
            List<Item> newItems = new List<Item>();

            for (var i = 0; i < titles.Count(); i++)
            {
                var newItem = new Item()
                {
                    Title = titles[i].InnerText,
                    About = abouts[i].InnerText,
                    Image = images[i].Attributes["href"].Value


                };
                decimal price;
                decimal.TryParse(prices[i].InnerText.TrimStart('$'), out price);
                newItem.CurrentPrice = price;
                Item oldItem = items.First(item => item.Title == newItem.Title);

                if (!(oldItem is null))
                {
                    if (oldItem.CurrentPrice != price)
                    {
                        oldItem.PriceHistory.ToList().Add(new PriceInfo()
                        {
                            Price = price,
                            Date = DateTime.UtcNow
                        });
                        oldItem.CurrentPrice = price;
                        context.Update(oldItem);
                    }

                }
                else
                {
                    newItem.PriceHistory.ToList().Add(new PriceInfo()
                    {
                        Price = price,
                        Date = DateTime.UtcNow
                    });
                    newItems.Add(newItem);
                }



            }

            await context.AddRangeAsync(newItems);

        }


    }
}
