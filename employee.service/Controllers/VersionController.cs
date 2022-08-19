using employee.service.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace employee.service.Controllers
{
    [Route("service")]
    public class VersionController : Controller
    {
        [Route("version")]
        [HttpGet]
        public IActionResult GetVersion()
        {
            var projectionsAssembly = Assembly.GetEntryAssembly();
            var version = new VersionModel
            {
                Version = projectionsAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion,
                Documents = new System.Collections.Generic.List<VersionDocument>
                {
                    new VersionDocument
                    {
                       Status = VersionStatus.Live.ToString()
                    }
                }
            };
            return Ok(version);
        }
    }
}
