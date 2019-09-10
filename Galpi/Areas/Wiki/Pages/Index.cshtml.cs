using System.Linq;
using Galpi.Data;
using Galpi.Models.Wiki;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Galpi.Areas.Wiki.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GalpiDbContext _db;

        [BindProperty]
        public Article Article { get; private set; }

        public IndexModel(GalpiDbContext db)
        {
            this._db = db;
        }

        public async Task<IActionResult> OnGet(string title)
        {
            if (string.IsNullOrEmpty(title)) { return RedirectToPage(new { title = "home/home" }); }

            var article = await _db.Articles.FirstOrDefaultAsync(item => item.Title == title);
            if (article == null)
            {
                Response.StatusCode = 404;
                return Page();
            }

            Article = article;

            return Page();
        }
    }
}