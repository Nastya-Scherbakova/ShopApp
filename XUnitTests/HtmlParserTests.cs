using System;
using Xunit;
using ShopApp.Services;
using ShopApp.Data;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;

namespace XUnitTests
{
    public class HtmlParserTests
    {
        private readonly HtmlParser parser;
        private ShopAppContext context;

        public HtmlParserTests()
        {
            parser = new HtmlParser();
            context = GetInMemoryContext();
        }

        [Fact]
        public async void HtmlParser_GetNotEmptyItemsDataAsync()
        {
           
            await parser.Parse(context);  
            Assert.NotEmpty(context.Items.AsNoTracking());
        }

        [Fact]
        public async void HtmlParser_GetNotEmptyPriceDataAsync()
        {
           
            await parser.Parse(context);  
            Assert.NotEmpty(context.PriceInfos.AsNoTracking());
        }

       
        [Fact]
        public async void HtmlParser_UpdatesDataAsync()
        {
           
            await parser.Parse(context);  
            var length = context.Items.Local.Count;
            await parser.Parse(context);  
            Assert.Equal(length, context.Items.Local.Count);
        }

        private ShopAppContext GetInMemoryContext()
        {
            DbContextOptions<ShopAppContext> options;
            var builder = new DbContextOptionsBuilder<ShopAppContext>();
            builder.UseInMemoryDatabase("ShopAppParser");
            options = builder.Options;
            ShopAppContext shopDataContext = new ShopAppContext(options);
            shopDataContext.Database.EnsureDeleted();
            shopDataContext.Database.EnsureCreated();
            return shopDataContext;
        }
    }
}
