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
using System.Text;

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
            HttpResponseMessage result = await hc.GetAsync("https://hotline.ua/mobile/umnye-chasy-smartwatch/");

            Stream stream = await result.Content.ReadAsStreamAsync();

            HtmlDocument doc = new HtmlDocument();

            doc.Load(stream);

          

            var titles = doc.DocumentNode.SelectNodes("//div[@class='item-info']/p/a").ToList();
            var abouts = doc.DocumentNode.SelectNodes("//div[@class='text']").ToList();
            var images = doc.DocumentNode.SelectNodes("//div[@class='item-img']/a/img").ToList();
            var prices = doc.DocumentNode.SelectNodes("//div[@class='price-md']/span").ToList();



            await HandleParsedDataAsync(context, titles, abouts, images, prices);
            

        }


        /// <summary>
        /// Adding parsed items in context or update them with new price
        /// </summary>
        /// <param name="context"> context from ItemsController </param>
        /// <param name="titles"> all items titles </param>
        /// <param name="abouts"> all items about infos </param>
        /// <param name="images"> all items image hrefs </param>
        /// <param name="prices"> all item prices </param>
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
                var imageHref = images[i].Attributes["src"].Value;
                Item newItem = new Item()
                {
                    Title = titles[i].InnerText,
                    About = abouts[i].InnerText,
                    Image = $"https://hotline.ua{imageHref}"


                };
                decimal price;
                string replacedPrice = prices[i].InnerText.Replace("&nbsp;", "");
                decimal.TryParse(replacedPrice, out price);
                newItem.CurrentPrice = price;
                Item oldItem = null;
                if (items.Any())
                {
                    oldItem = items.FirstOrDefault(item => item.Title == newItem.Title);
                }

                if (!(oldItem is null))
                {
                    if (oldItem.CurrentPrice != price)
                    {
                        var priceInHistory = new PriceInfo()
                        {
                            Price = price,
                            Date = DateTime.Now,
                            Item = oldItem
                        };
                        oldItem.PriceHistory.Add(priceInHistory);
                        oldItem.Image = newItem.Image;
                        oldItem.About = newItem.About;
                        oldItem.CurrentPrice = price;
                       
                        context.Items.Update(oldItem);
                        await context.SaveChangesAsync();
                    }

                }
                else
                {
                    var priceInHistory = new PriceInfo()
                        {
                            Price = price,
                            Date = DateTime.Now,
                            Item = newItem
                        };
                    newItem.PriceHistory.Add(priceInHistory);
                    
                    context.Items.Add(newItem);
                   await context.SaveChangesAsync();
           
                }



            }

            await context.SaveChangesAsync();

        }


    }
}
