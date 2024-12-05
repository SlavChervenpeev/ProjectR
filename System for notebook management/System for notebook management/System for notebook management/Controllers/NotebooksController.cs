using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectR.Models;
using System_for_notebook_management.Data;


namespace System_for_notebook_management.Controllers
{
  
    public class NotebooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotebooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Notebooks
        
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? page)
        {
            // Настройка на параметрите за странициране.
            int pageSize = 10; // Примерна стойност за размер на страницата.
            int pageNumber = (page ?? 1);

            // Извличане на всички записи и прилагане на филтри.
            var notebooks = _context.Notebooks
                .Include(n => n.Company)
                .Include(n => n.NotebookSpecifications)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                notebooks = notebooks.Where(n => n.Name.Contains(searchString) || n.Company.Name.Contains(searchString));
            }

            // Сортиране на резултатите.
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["CurrentFilter"] = searchString;

            switch (sortOrder)
            {
                case "name_desc":
                    notebooks = notebooks.OrderByDescending(n => n.Name);
                    break;
                case "Price":
                    notebooks = notebooks.OrderBy(n => n.Price);
                    break;
                case "price_desc":
                    notebooks = notebooks.OrderByDescending(n => n.Price);
                    break;
                default:
                    notebooks = notebooks.OrderBy(n => n.Name);
                    break;
            }

            // Изчисляване на общия брой записи за странициране.
            var totalRecords = await notebooks.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            // Предаване на общия брой записи и текущата страница към изгледа.
            ViewData["TotalRecords"] = totalRecords;
            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = pageNumber;

            // Странициране.
            var pagedNotebooks = await notebooks.Skip((pageNumber - 1) * pageSize)
                                               .Take(pageSize)
                                               .ToListAsync();

            return View(pagedNotebooks);
        }

        // GET: Notebooks/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notebook = await _context.Notebooks
                .Include(n => n.Company)
                .Include(n => n.NotebookSpecifications)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notebook == null)
            {
                return NotFound();
            }

            return View(notebook);
        }





        // GET: Notebooks/Create
        
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name");
            ViewData["NotebookSpecificationsId"] = new SelectList(_context.NotebookSpecifications, "Id", "Material");
            return View();
        }

        // POST: Notebooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("Id,NotebookSpecificationsId,CompanyId,Name,Type,NumberOfPages,Price,CoverImage,IsWithLines,PaperType")] Notebook notebook)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(notebook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", notebook.CompanyId);
            ViewData["NotebookSpecificationsId"] = new SelectList(_context.NotebookSpecifications, "Id", "Id", notebook.NotebookSpecificationsId);
            return View(notebook);
        }

        // GET: Notebooks/Edit/5
       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notebook = await _context.Notebooks.FindAsync(id);
            if (notebook == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", notebook.CompanyId);
            ViewData["NotebookSpecificationsId"] = new SelectList(_context.NotebookSpecifications, "Id", "Id", notebook.NotebookSpecificationsId);
            return View(notebook);
        }

        // POST: Notebooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(int id, [Bind("Id,NotebookSpecificationsId,CompanyId,Name,Type,NumberOfPages,Price,CoverImage,IsWithLines,PaperType")] Notebook notebook)
        {
            if (id != notebook.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(notebook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotebookExists(notebook.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", notebook.CompanyId);
            ViewData["NotebookSpecificationsId"] = new SelectList(_context.NotebookSpecifications, "Id", "Id", notebook.NotebookSpecificationsId);
            return View(notebook);
        }

        // GET: Notebooks/Delete/5
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notebook = await _context.Notebooks
                .Include(n => n.Company)
                .Include(n => n.NotebookSpecifications)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notebook == null)
            {
                return NotFound();
            }

            return View(notebook);
        }

        // POST: Notebooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notebook = await _context.Notebooks.FindAsync(id);
            if (notebook != null)
            {
                _context.Notebooks.Remove(notebook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotebookExists(int id)
        {
            return _context.Notebooks.Any(e => e.Id == id);
        }
    }
}
