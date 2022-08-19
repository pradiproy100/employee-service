
using employee.service.Entities;
using System.Collections.Generic;

namespace employee.service.Repositories.Database
{
    public interface IDatabaseRepository
    {
        List<Employee> GetEmployeesList();
        EmployeeDetail GetEmployeeDetailById(int employeeId);
        int AddEmployee(string name, string address, int salary, int departmentId);
        int EditEmployee(int id, string name, string address, int salary, int departmentId);
        int RemoveEmployee(int employeeId);
    }
}
