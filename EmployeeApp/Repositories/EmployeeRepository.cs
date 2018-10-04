using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EmployeeApp.Repositories
{
    public class EmployeeRepository:IEmployeeRepository
    {
    
              private readonly IDataContext _context;

        public EmployeeRepository(IDataContext context)
        {
            _context = context;
        }

        public List<EmployeeInfo> GetAllEmployees()
        {
            return _context
                            .EmployeeInfos
                            .Find(_ => true)
                            .ToList();
        }

        public EmployeeInfo GetEmployee(ObjectId id)
        {
            FilterDefinition<EmployeeInfo> filter = Builders<EmployeeInfo>.Filter.Eq(m => m.Id, id);

            return _context
                    .EmployeeInfos
                    .Find(filter)
                    .FirstOrDefault();
        }

        public async Task Create(EmployeeInfo employee)
        {
            await _context.EmployeeInfos.InsertOneAsync(employee);
        }

        public async Task<bool> Update(EmployeeInfo employee)
        {
            ReplaceOneResult updateResult =
                await _context
                        .EmployeeInfos
                        .ReplaceOneAsync(
                            filter: g => g.Id == employee.Id,
                            replacement: employee);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(ObjectId id)
        {
            FilterDefinition<EmployeeInfo> filter = Builders<EmployeeInfo>.Filter.Eq(m => m.Id, id);

            DeleteResult deleteResult = await _context
                                                .EmployeeInfos
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
