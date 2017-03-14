using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Calendar.Models;
using Microsoft.Extensions.Options;

namespace Calendar.Controllers
{
    [Produces("application/json")]
    [Route("api/AppSettings")]
    public class AppSettingsController : Controller
    {
        private readonly AppSettings _appsettings;
        /* make use of the dependency injection provided by asp.net core */
        public AppSettingsController(IOptions<AppSettings> appsettings)
        {
            _appsettings = appsettings.Value;
        }

        public string CM_Url { get { return _appsettings.cm_url; } }
        public string IA_Url { get { return _appsettings.ia_url; } }

        /*
        public IActionResult Index()
        {
            var option1 = _appsettings.cm_url;
            var option2 = _appsettings.ia_url;
            return Content($"option1 = {option1}, option2 = {option2}");
        }
        */
    }
}