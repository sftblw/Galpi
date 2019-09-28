using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace Galpi.Areas.Wiki
{
    public static class WikiRouting
    {
        public static void ConfigureWikiRouting(this RazorPagesOptions options)
        {
            options.Conventions.AddAreaPageRoute("Wiki", "/Create", "/Wiki/{title}/Create");
        }
    }
}
