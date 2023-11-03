using System;
using System.Web;

namespace TransportManagement
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Get the current HttpContext
            HttpContext context = ((HttpApplication)sender).Context;

            // Access the Response object
            var response = context.Response;
            response.AddHeader("Access-Control-Allow-Origin", "*");

            if (context.Request.HttpMethod == "OPTIONS")
            {
                response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                response.End();
            }

            // Your additional logic or modifications to the response, if needed
            // For example, you can set response headers, cookies, etc.
            // response.Headers.Add("MyCustomHeader", "MyHeaderValue");
        }
        void Session_End(Object sender, EventArgs E)
        {
            // Clean up session resources
        }
    }
}