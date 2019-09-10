using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Galpi.Areas.Wiki
{
    public static class TitleDecodingHelper
    {
        public static string TitleDecoded(this RouteValueDictionary dic)
        {
            return dic.ContainsKey("title") ? WebUtility.UrlDecode(dic["title"] as string) : null;
        }
    }
}
