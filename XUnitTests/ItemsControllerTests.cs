using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApp.Controllers;
using ShopApp.Data;
using ShopApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace XUnitTests
{
    public class ItemsControllerTests
    {
        private ItemsController controller;
        private ShopAppContext context;


        [Fact]
        public async void ItemsController_GetEmptyDataAsync()
        {
            context = GetInMemoryContext();
            controller = new ItemsController(context);
            var items = await controller.Index();
            Assert.Empty(items);
        }

        [Fact]
        public async void ItemsController_GetNotEmptyDataAsync()
        {
            context = GetInMemoryContext();
            controller = new ItemsController(context);
            Item newItem = new Item()
            {
                Id = 1,
                Title = "First",
                About = "About",
                CurrentPrice = 222.22m,
                Image = "image/href"
            };
            await context.AddAsync(newItem);
            await context.SaveChangesAsync();
            var items = await controller.Index();
            Assert.NotEmpty(items);
        }

        [Fact]
        public async void ItemsController_GetSelectedNotNullableItemDataAsync()
        {
            context = GetInMemoryContext();
            controller = new ItemsController(context);
            Item newItem = new Item()
            {
                Id = 1,
                Title = "First",
                About = "About",
                CurrentPrice = 222.22m,
                Image = "image/href"
            };
            await context.AddAsync(newItem);
            await context.SaveChangesAsync();
            
            OkObjectResult itemRes = await controller.Details(newItem.Id) as OkObjectResult;
            Assert.NotNull(itemRes);
            Item item = itemRes.Value as Item;
            Assert.NotNull(item);
           

        }

        [Fact]
        public async void ItemsController_ThrowNotFoundExceptionItemDataAsync()
        {
            context = GetInMemoryContext();
            controller = new ItemsController(context);
            Item newItem = new Item()
            {
                Id = 1,
                Title = "First",
                About = "About",
                CurrentPrice = 222.22m,
                Image = "image/href"
            };
            await context.AddAsync(newItem);
            await context.SaveChangesAsync();
            
            NotFoundResult itemRes = await controller.Details(2) as NotFoundResult;
            Assert.NotNull(itemRes);
           

        }

        [Fact]
        public async void ItemsController_ThrowNotFoundExceptionItemNullIdAsync()
        {
            context = GetInMemoryContext();
            controller = new ItemsController(context);
            Item newItem = new Item()
            {
                Id = 1,
                Title = "First",
                About = "About",
                CurrentPrice = 222.22m,
                Image = "image/href"
            };
            await context.AddAsync(newItem);
            await context.SaveChangesAsync();
            
            NotFoundResult itemRes = await controller.Details(null) as NotFoundResult;
            Assert.NotNull(itemRes);
           

        }

        [Fact]
        public async void ItemsController_ThrowNotFoundExceptionItemsEmptyAsync()
        {
            context = GetInMemoryContext();
            controller = new ItemsController(context);
           
            NotFoundResult itemRes = await controller.Details(1) as NotFoundResult;
            Assert.NotNull(itemRes);
           

        }

        [Fact]
        public async void ItemsController_ParseDataNotEmptyAsync()
        {
            context = GetInMemoryContext();
            controller = new ItemsController(context);
           
            var items = await controller.Parse();
            Assert.NotNull(items);
           

        }

        [Fact]
        public async void ItemsController_ParseDataLength_BiggerThan20_Async()
        {
            context = GetInMemoryContext();
            controller = new ItemsController(context);
           
            var items = await controller.Parse() as List<Item>;
            bool isBigger = items.Count > 20;
            Assert.True(isBigger);
           

        }

        [Fact]
        public async void ItemsController_GetSelectedItemDataAsync()
        {
            context = GetInMemoryContext();
            controller = new ItemsController(context);
            Item newItem = new Item()
            {
                Id = 1,
                Title = "First",
                About = "About",
                CurrentPrice = 222.22m,
                Image = "image/href"
            };
            await context.AddAsync(newItem);
            await context.SaveChangesAsync();
            
            OkObjectResult itemRes = await controller.Details(newItem.Id) as OkObjectResult;
            Assert.NotNull(itemRes);
            Item item = itemRes.Value as Item;
            Assert.Equal(newItem.Title, item.Title);
            Assert.Equal(newItem.Image, item.Image);
            Assert.Equal(newItem.About, item.About);
            Assert.Equal(newItem.CurrentPrice, item.CurrentPrice);
            Assert.Equal(newItem.PriceHistory.Count, item.PriceHistory.Count);

        }


        private ShopAppContext GetInMemoryContext()
        {
            DbContextOptions<ShopAppContext> options;
            var builder = new DbContextOptionsBuilder<ShopAppContext>();
            builder.UseInMemoryDatabase("ShopApp");
            options = builder.Options;
            ShopAppContext shopDataContext = new ShopAppContext(options);
            shopDataContext.Database.EnsureDeleted();
            shopDataContext.Database.EnsureCreated();
            return shopDataContext;
        }
    }
}
