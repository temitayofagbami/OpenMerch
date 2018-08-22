using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Pic")]
    public class PicController : Controller
    {

    // property is webenvironment where wwwroot folder is
    private readonly IHostingEnvironment _env;

    public PicController(IHostingEnvironment env)
     {
            //initialize it in constructor
              _env = env;
     }
        //return a pic
        [HttpGet] //client is requesting info from server
        [Route("{id}")] //allows you to call api/Pic/1, Route defines how the method will be called using id parameter 
        // parameter name in the route has to match the name in the method, it will not be able to do the mapping if they dont match 
        public IActionResult GetImage(int id)
        {
            // go to wwwroot folderin webenvironment
            var webRoot = _env.WebRootPath;
            var path = Path.Combine(webRoot + "/pics/", "shoes-" + id + ".png");

            var buffer = System.IO.File.ReadAllBytes(path);

            return File(buffer, "image/png");
        }
    }
}