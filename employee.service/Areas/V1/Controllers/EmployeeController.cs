using employee.service.Areas.V1.Models.Requests;
using employee.service.Areas.V1.Models.Responses;
using employee.service.Configurations;
using employee.service.Entities;
using employee.service.Filters;
using employee.service.Repositories.Database;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        /// <summary>
        /// Get Employee List
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IActionResult GetEmployeeList()
        {
            var response = new GetEmployeesListResponse();

            var result = _dbRepository.GetEmployeesList();
            if (result != null && result.Any())
            {
                response.Result = result;
                response.Success = true;
            }
            else
            {
                response.ErrorResponse = Helpers.Helper.ConvertToErrorResponse("No employee record found..", ErrorsType.NoRecordFound.ToString(), ErrorMessageType.Validation.ToString());
            }

            return Ok(response);

        }

        /// <summary>
        /// Get Employee By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public IActionResult AddEmployee([FromBody] AddEmployeeRequest request)
        {
            var response = new AddEmployeeResponse();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(e => e.Value.Errors.Count > 0).Select(ee => ee.Value.Errors.First().ErrorMessage);
                response.ErrorResponse = Helpers.Helper.ConvertToErrorResponse(errors.FirstOrDefault(), ErrorsType.ValidationError.ToString(), ErrorMessageType.Validation.ToString());
                return BadRequest(response);
            }

            var result = _dbRepository.AddEmployee(request.Name, request.Address, request.Salary, request.DepartmentId);
            if (result > 0)
            {
                response.Result = true;
                response.Success = true;
            }
            else
            {
                response.ErrorResponse = Helpers.Helper.ConvertToErrorResponse("Some error occured in adding employee..", ErrorsType.DatabaseError.ToString(), ErrorMessageType.Error.ToString());
            }
            return Ok(response);
        }


        /// <summary>
        /// Edit Employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpPut]
        public IActionResult EditEmployee([Required] int id, [FromBody] AddEmployeeRequest request)
        {
            var response = new AddEmployeeResponse();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(e => e.Value.Errors.Count > 0).Select(ee => ee.Value.Errors.First().ErrorMessage);
                response.ErrorResponse = Helpers.Helper.ConvertToErrorResponse(errors.FirstOrDefault(), ErrorsType.ValidationError.ToString(), ErrorMessageType.Validation.ToString());
                return BadRequest(response);
            }

            var result = _dbRepository.EditEmployee(id, request.Name, request.Address, request.Salary, request.DepartmentId);
            if (result > 0)
            {
                response.Result = true;
                response.Success = true;
            }
            else
            {
                response.ErrorResponse = Helpers.Helper.ConvertToErrorResponse("Some error occured in adding employee..", ErrorsType.DatabaseError.ToString(), ErrorMessageType.Error.ToString());
            }
            return Ok(response);

        }

        /// <summary>
        /// Remove Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult RemoveEmployee(int id)
        {
            var response = new RemoveEmployeeResponse();

            if (id <= 0)
            {
                response.ErrorResponse = Helpers.Helper.ConvertToErrorResponse("Invalid Id..", ErrorsType.ValidationError.ToString(), ErrorMessageType.Validation.ToString());
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(e => e.Value.Errors.Count > 0).Select(ee => ee.Value.Errors.First().ErrorMessage);

                return BadRequest(errors);
            }

            var result = _dbRepository.RemoveEmployee(id);
            if (result > 0)
            {
                response.Result = true;
                response.Success = true;
            }
            else
            {
                response.ErrorResponse = Helpers.Helper.ConvertToErrorResponse("Some error occured in removing employee info..", ErrorsType.DatabaseError.ToString(), ErrorMessageType.Error.ToString());
            }
            return Ok(response);

        }
    }
}
