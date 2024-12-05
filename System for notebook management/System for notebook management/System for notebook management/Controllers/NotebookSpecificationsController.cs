using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectR.Models;
using System_for_notebook_management.Data;

namespace System_for_notebook_management.Controllers
{
    public class NotebookSpecificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotebookSpecificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NotebookSpecifications
        public async Task<IActionResult> Index()
        {
            return View(await _context.NotebookSpecifications.ToListAsync());
        }

        // GET: NotebookSpecifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notebookSpecifications = await _context.NotebookSpecifications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notebookSpecifications == null)
            {
                return NotFound();
            }

            return View(notebookSpecifications);
        }

        // GET: NotebookSpecifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NotebookSpecifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Weight,IsRecyclable,HasRuler,CreatedDate,Material,CoverColor")] NotebookSpecifications notebookSpecifications)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notebookSpecifications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notebookSpecifications);
        }

        // GET: NotebookSpecifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notebookSpecifications = await _context.NotebookSpecifications.FindAsync(id);
            if (notebookSpecifications == null)
            {
                return NotFound();
            }
            return View(notebookSpecifications);
        }

        // POST: NotebookSpecifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Weight,IsRecyclable,HasRuler,CreatedDate,Material,CoverColor")] NotebookSpecifications notebookSpecifications)
        {
            if (id != notebookSpecifications.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notebookSpecifications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotebookSpecificationsExists(notebookSpecifications.Id))
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
            return View(notebookSpecifications);
        }

        // GET: NotebookSpecifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notebookSpecifications = await _context.NotebookSpecifications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notebookSpecifications == null)
            {
                return NotFound();
            }

            return View(notebookSpecifications);
        }

        // POST: NotebookSpecifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notebookSpecifications = await _context.NotebookSpecifications.FindAsync(id);
            if (notebookSpecifications != null)
            {
                _context.NotebookSpecifications.Remove(notebookSpecifications);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotebookSpecificationsExists(int id)
        {
            return _context.NotebookSpecifications.Any(e => e.Id == id);
        }
    }
}
