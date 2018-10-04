using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeApp.Models;
using MongoDB.Bson;

namespace EmployeeApp.Repositories
{
    public interface IEmployeeRepository
    {
        List<EmployeeInfo> GetAllEmployees();
        EmployeeInfo GetEmployee(ObjectId id);
        Task Create(EmployeeInfo employee);
        Task<bool> Update(EmployeeInfo employee);
        Task<bool> Delete(ObjectId id);
    }
}
