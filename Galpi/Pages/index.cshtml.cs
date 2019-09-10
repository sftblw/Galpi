using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Galpi.Pages
{
    public class indexModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("Index", new {area = "Wiki"});
        }
    }
}