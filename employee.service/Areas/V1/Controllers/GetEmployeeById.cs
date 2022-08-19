using employee.service.Areas.V1.Models.Responses;
using employee.service.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace employee.service.Areas.V1.Controllers
{
    public partial class EmployeeController : Controller
    {
        [Route("{id:int}")]
        [HttpGet]
        public IActionResult GetEmployeeById(int id)
        {
            var response = new GetEmployeeResponse();

            if (id <= 0)
            {
                response.ErrorResponse = Helpers.Helper.ConvertToErrorResponse("Invalid Id..", ErrorsType.ValidationError.ToString(), ErrorMessageType.Validation.ToString());
                return BadRequest(response);
            }

            var result = _dbRepository.GetEmployeeDetailById(id);
            if (result != null)
            {
                response.Result = result;
                response.Success = true;
                return Ok(response);
            }
            else
            {
                response.ErrorResponse = Helpers.Helper.ConvertToErrorResponse("No Resource found..", ErrorsType.ResourceNotFoundError.ToString(), ErrorMessageType.Error.ToString());
                return NotFound(response);
            }
        }
    }

}
