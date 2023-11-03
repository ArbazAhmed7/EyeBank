
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using TransportManagementCore.Models;

namespace TransportManagementCore.Utilities
{
    public class ViewRightsAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public string FormId { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isValidRequest = false;
            AutherizedFormRights FormRights = Utilities.General.GetFormRights(context.HttpContext.Session.GetString("LoginId"), FormId);

            if (FormRights.CanView == true)
            {
                isValidRequest = true;
            }

            if (!isValidRequest)
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
            }

        }
    }
}

