using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EmployeeApp.Models
{
    public class DataContext: IDataContext
    {
        private readonly IMongoDatabase _db;

        public DataContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<EmployeeInfo> EmployeeInfos => _db.GetCollection<EmployeeInfo>("EmployeeInfos");
    }
    }

