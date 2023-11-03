using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TransportManagementCore.Middleware
{
    public static class SessionCheckMiddleware
    {
        public static IApplicationBuilder UseSessionCheckMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomMiddleware>();
            return app;
        }

        public class CustomMiddleware
        {
            private readonly RequestDelegate _next;

            public CustomMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                //string loginId = context.Session.GetString("LoginId");

                //if (string.IsNullOrEmpty(loginId))
                //{

                //    context.Response.Redirect($"{context.Request.Scheme}://{context.Request.Host}/login");
                //}
                //else
                //{

                //    Global.CurrentUser = new UserInfo();

                //Global.CurrentUser.UserId = Convert.ToInt32(context.Session.GetString("UserId"));
                //Global.CurrentUser.LoginId = Convert.ToString(context.Session.GetString("LoginId"));
                //Global.CurrentUser.UserName = Convert.ToString(context.Session.GetString("UserName"));
                //Global.CurrentUser.UserType = Convert.ToString(context.Session.GetString("UserType"));
                //Global.CurrentUser.CompanyId = Convert.ToInt32(context.Session.GetString("CompanyId"));
                //Global.CurrentUser.CompanyName = Convert.ToString(context.Session.GetString("CompanyName"));
                //Global.CurrentUser.RegionId = Convert.ToInt32(context.Session.GetString("RegionId"));
                //Global.CurrentUser.RegionName = Convert.ToString(context.Session.GetString("RegionName"));
                //Global.CurrentUser.BranchId = Convert.ToInt32(context.Session.GetString("BranchId"));
                //Global.CurrentUser.BranchName = Convert.ToString(context.Session.GetString("BranchName"));
                //Global.CurrentUser.EntryTerminalIP = Convert.ToString(context.Session.GetString("EntryTerminalIP"));
                //Global.CurrentUser.EntryTerminal = Convert.ToString(context.Session.GetString("EntryTerminal"));

                await _next(context);
                //}
            }
        } 


    }
}
