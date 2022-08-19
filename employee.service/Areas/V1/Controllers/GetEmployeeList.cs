using employee.service.Areas.V1.Models.Responses;
using employee.service.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace employee.service.Areas.V1.Controllers
{  
    public partial class EmployeeController : Controller
    {
        [Route("")]
        [HttpGet]
        public IActionResult GetEmployeeList()
        {
            var response =new GetEmployeesListResponse();

            var result=  _dbRepository.GetEmployeesList();            
            if (result != null && result.Any())
            {
                response.Result = result;
                response.Success = true;
            }
            else
            {
                response.ErrorResponse = Helpers.Helper.ConvertToErrorResponse("No employee record found..",ErrorsType.NoRecordFound.ToString() , ErrorMessageType.Validation.ToString());               
            }

            return Ok(response);
                
        } 
    }
 
}
