﻿using employee.service.Areas.V1.Models.Requests;
using employee.service.Areas.V1.Models.Responses;
using employee.service.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace employee.service.Areas.V1.Controllers
{
    public partial class EmployeeController : Controller
    {
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
    }

}
