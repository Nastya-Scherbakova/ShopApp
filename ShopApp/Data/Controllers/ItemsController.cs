using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Models;
using ShopApp.Services;

namespace ShopApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ShopAppContext _context;

        public ItemsController(ShopAppContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<List<Item>> Index()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.Include(el=>el.PriceHistory).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            item.PriceHistory.ForEach(el=>el.Item = null);
            

            return Ok(item);
        }

        // GET: Items/Parse
        public async Task<IEnumerable<Item>> Parse()
        {
            var context = _context;

            HtmlParser parser = new HtmlParser();
            await parser.Parse(context);

            var changedItems = await context.Items.ToListAsync();

            return changedItems;
        }

        
    }
}
