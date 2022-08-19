using employee.service.Entities;

namespace employee.service.Areas.V1.Models.Responses
{
    public class AddEmployeeResponse
    {
        public bool Success { get; set; }
        public bool Result { get; set; } 
        public ResponseMessage ErrorResponse { get; set; }
    }
}
