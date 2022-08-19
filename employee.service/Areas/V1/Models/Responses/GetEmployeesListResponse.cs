using employee.service.Entities;
using System.Collections.Generic;

namespace employee.service.Areas.V1.Models.Responses
{
    public class GetEmployeesListResponse
    {
        public bool Success { get; set; }
        public List<Employee> Result { get; set; }
        public ResponseMessage ErrorResponse { get; set; }
    }
}
