using MongoDB.Driver;

namespace EmployeeApp.Models
{
    public interface IDataContext
    {
        IMongoCollection<EmployeeInfo> EmployeeInfos { get; }
    }
}