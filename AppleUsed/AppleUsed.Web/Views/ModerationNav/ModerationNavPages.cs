﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Views.ModerationNav
{
    public static class ModerationNavPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string Users => "Пользователи";

        public static string Ads => "Объявления";

        public static string UsersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Users);

        public static string AdsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Ads);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}
