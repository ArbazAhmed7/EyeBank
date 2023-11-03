using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace TransportManagementCore.Controllers
{
    [Area("Session")]
    [Route("Session/SessionStore")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        
        private readonly IConfiguration Configuration;
        public SessionController(IConfiguration config)
        {
            this.Configuration = config;

        }
        [Route("api/Get/{UserId}")]
        [HttpGet]
        public ObjectResult Get(string UserId)
        {
            string id = Utilities.Encryption.GetDecrypt(UserId);
            
            var MainApplication = this.Configuration.GetSection("MainApplicationURL").GetSection("URL").Value;
            HttpContext.Session.SetString("LoginId", id);
            Global.MainApplication = MainApplication;
            HttpContext.Session.SetString("MainApplication", MainApplication);
            //// Logic to fetch and return data
            return Ok("API response");
        }

        [Route("Getuser")]
        [HttpGet]
        public string Getuser()
        {
            string User=HttpContext.Session.GetString("LoginId");

            //// Logic to fetch and return data
            return User;
        }
        [Route("api/ClearSession")]
        [HttpGet]
        public IActionResult ClearSession()
        {
            HttpContext.Session.Clear();

            return Ok("Session Cleared");
        }
        
    }

}
