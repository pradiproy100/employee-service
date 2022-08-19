using employee.service.Configurations;
using employee.service.Filters;
using employee.service.Repositories.Database;
using Microsoft.AspNetCore.Mvc;

namespace employee.service.Areas.V1.Controllers
{
    [Route("/employee/v1")]
    [ServiceFilter(typeof(CustomAuthorize))]
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public partial class EmployeeController : Controller
    {
        public const int REFUND_NOTE_TYPE_ID = 24;
        public const int TAX_EXEMPT_ID = 2;
        private readonly AppSettings _apiSettings;
        private readonly IDatabaseRepository _dbRepository;

        public EmployeeController(AppSettings apiSettings, IDatabaseRepository dbRepository)
        {
            _apiSettings = apiSettings;
            _dbRepository = dbRepository;
        }
    }
}
