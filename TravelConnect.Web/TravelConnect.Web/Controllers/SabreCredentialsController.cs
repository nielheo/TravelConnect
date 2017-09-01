using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelConnect.Models;

namespace TravelConnect.Web.Controllers
{
    public class SabreCredentialsController : Controller
    {
        private readonly TCContext _context;

        public SabreCredentialsController(TCContext context)
        {
            _context = context;
        }

        // GET: SabreCredentials
        public async Task<IActionResult> Index()
        {
            return View(await _context.SabreCredentials.ToListAsync());
        }

        // GET: SabreCredentials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sabreCredential = await _context.SabreCredentials
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sabreCredential == null)
            {
                return NotFound();
            }

            return View(sabreCredential);
        }

        // GET: SabreCredentials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SabreCredentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,ClientSecret,AccessToken,ExpiryTime,IsActive")] SabreCredential sabreCredential)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sabreCredential);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sabreCredential);
        }

        // GET: SabreCredentials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sabreCredential = await _context.SabreCredentials.SingleOrDefaultAsync(m => m.Id == id);
            if (sabreCredential == null)
            {
                return NotFound();
            }
            return View(sabreCredential);
        }

        // POST: SabreCredentials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,ClientSecret,AccessToken,ExpiryTime,IsActive")] SabreCredential sabreCredential)
        {
            if (id != sabreCredential.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sabreCredential);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SabreCredentialExists(sabreCredential.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sabreCredential);
        }

        // GET: SabreCredentials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sabreCredential = await _context.SabreCredentials
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sabreCredential == null)
            {
                return NotFound();
            }

            return View(sabreCredential);
        }

        // POST: SabreCredentials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sabreCredential = await _context.SabreCredentials.SingleOrDefaultAsync(m => m.Id == id);
            _context.SabreCredentials.Remove(sabreCredential);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SabreCredentialExists(int id)
        {
            return _context.SabreCredentials.Any(e => e.Id == id);
        }
    }
}
