using System.Linq;
using GroceryStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GroceryStore.Web.Filters
{
    public class NotFoundUser : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var db = (GroceryStoreDbContext)context.HttpContext.RequestServices.GetService(typeof(GroceryStoreDbContext));
            var id = context.RouteData.Values.Values.Last().ToString();

            var userExist = db.Users.Find(id);
            if (userExist == null)
            {
                //{
                //    var routeValues = context.RouteData.Values;

                //    var areaName=routeValues["Area"];
                //    var controllerName= routeValues["Controller"];

                context.Result = new NotFoundResult();
            }
        }
    }
}
