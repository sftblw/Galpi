using Galpi.Data;
using Galpi.Models.Wiki;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Galpi.Areas.Wiki.Pages
{
    public class CreateModel : PageModel
    {
        private readonly GalpiDbContext _db;

        public CreateModel(GalpiDbContext db)
        {
            this._db = db;
            
        }

        public async Task<IActionResult> OnGet(string title)
        {
            if (await _db.Articles.AnyAsync(item => item.Title == title))
            {
                RedirectToPage("Edit");
            }

            return Page();
        }
    }
}