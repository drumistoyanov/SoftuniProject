using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroceryStore.Web.Areas.Identity.Pages
{
    [AllowAnonymous]
#pragma warning disable SA1649 // File name should match first type name
    public class ErrorModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
#pragma warning restore MVC1001 // Filters cannot be applied to page handler methods.
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
