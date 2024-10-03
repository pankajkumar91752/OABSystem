using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OABSystem.Areas.Identity.Data;
using OABSystem.Data;

namespace OABSystem.Filters
{
    public class UserAppoinmentAuthorizationFilter : ActionFilterAttribute, IAsyncPageFilter
    {
        private readonly UserManager<OABSystemUser> userManager;
        private readonly OABSystemContext dbCcontext;

        public UserAppoinmentAuthorizationFilter(UserManager<OABSystemUser> userManager, OABSystemContext dbCcontext)
        {
            this.userManager = userManager;
            this.dbCcontext = dbCcontext;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("id", out object id))
            {
                var userName = userManager.GetUserName(context.HttpContext.User);               
                var apppointment = dbCcontext.Appointment.Where(e => e.AppointmentId == id as int?).FirstOrDefault();
                if (apppointment == null) { context.Result = context.Result = new NotFoundResult(); }
                else
                {
                    if (apppointment.PatientName != userName)
                    {
                        context.Result = new UnauthorizedObjectResult(userName);
                    }
                }
            }
            base.OnActionExecuting(context);

        }





        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            if (context.HandlerArguments.TryGetValue("id", out object id))
            {

                var user = await userManager.GetUserAsync(context.HttpContext.User);

                var apppointment = dbCcontext.Appointment.Where(e => e.AppointmentId == id as int?).FirstOrDefault();
                if (apppointment == null)
                {
                    context.Result = new NotFoundResult();
                }
                else
                {
                    if (!(await userManager.IsInRoleAsync(user, "Admin")) && apppointment.UserName != user.UserName)
                    {
                        context.Result = new UnauthorizedObjectResult($"Unuthorized  access{user}");
                    }
                }
            }
            await next.Invoke();
        }       

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            await Task.CompletedTask;
        }
    }
}
