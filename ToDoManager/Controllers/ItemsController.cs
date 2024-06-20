using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoManager.Models;
using ToDoManager.Data;

namespace ToDoManager.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemContext _context;

        public ItemsController(ItemContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET : Employees/Create
        public IActionResult AddNew()
        {
            return View();
        }
        //POST : Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNew([Bind("ItemId", "ItemName", "ItemDescription", "ItemDate")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items
        public async Task<IActionResult> ViewAll()
        {
            return View(await _context.Items.ToListAsync());
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemDescription,ItemDate")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ViewAll));
            }
            return View(item);
        }

        //GET ITEMS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        //GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = _context.Items.FirstOrDefault(i => i.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        //POST : Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ViewAll));
        }
    
        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
