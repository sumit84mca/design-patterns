using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CaptureRAWRequest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaptureRequestController : ControllerBase
    {
        private readonly IWebHostEnvironment hostEnvironment;

        public CaptureRequestController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet("GetCookies")]
        public ActionResult<string> Get()
        {

            var options = new CookieOptions
            {
                Domain = Request.Host.Host,
                Expires = DateTime.Now.AddDays(365),
                HttpOnly = true,
                SameSite = SameSiteMode.None
            };

            if(Request.Scheme == "https")
            {
                options.Secure = true;
            }

            Response.Cookies.Append("test", "somevalue", options);

            var options2 = new CookieOptions
            {
                Domain = "cookie.anotherdomain.com",
                Expires = DateTime.Now.AddDays(365),
                HttpOnly = true,
                SameSite = SameSiteMode.None
            };

            if (Request.Scheme == "https")
            {
                options2.Secure = true;
            }

            Response.Cookies.Append("test", "somevalue", options2);

            return Request.ToString();
        }

        [HttpGet("PostCookies")]       
        public ActionResult<string> Get([FromQuery] string id)
        {
            if(Request.Cookies.ContainsKey("test"))
            {
                return Request.Cookies["test"];
            }
            
            return "no cookies found";
        }

        [HttpGet("GetImage")]
        public ActionResult GetImage([FromQuery] string id)
        {            
            string found = Path.Combine(hostEnvironment.ContentRootPath, hostEnvironment.WebRootPath, "found.png");
            string notfound = Path.Combine(hostEnvironment.ContentRootPath, hostEnvironment.WebRootPath, "notfound.png");
            
            if (Request.Cookies.ContainsKey("test"))
            {               
                return File(System.IO.File.OpenRead(found), "image/png");
            }            
            return File(System.IO.File.OpenRead(notfound), "image/png");
        }

        
    }
}
