using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System_for_notebook_management.Models;
using System_for_notebook_management.Data;
using Microsoft.AspNetCore.Authorization;


namespace System_for_notebook_management.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // Извличане на всички тетрадки с включени компании и спецификации.
            var notebooks = await _context.Notebooks
                                          .Include(n => n.Company)
                                          .Include(n => n.NotebookSpecifications)
                                          .ToListAsync();
            return View(notebooks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
