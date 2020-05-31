using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace FitnessClub.Areas.CustomerPanel.Pages
{
    public static class CustomerPanelNavPages
    {
        public static string Index => "Index";
        public static string Calendar => "Calendar";
        public static string CoachRating => "CoachRating";
        public static string Membership => "Membership";
        public static string AccountSettings => "AccountSettings";
        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);
        public static string CalendarNavClass(ViewContext viewContext) => PageNavClass(viewContext, Calendar);
        public static string CoachRatingNavClass(ViewContext viewContext) => PageNavClass(viewContext, CoachRating);
        public static string MembershipNavClass(ViewContext viewContext) => PageNavClass(viewContext, Membership);
        public static string AccountSettingsNavClass(ViewContext viewContext) => PageNavClass(viewContext, AccountSettings);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
