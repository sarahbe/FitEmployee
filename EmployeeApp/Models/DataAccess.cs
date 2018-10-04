using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;


namespace EmployeeApp.Models
{
    public class DataAccess
    {
            MongoClient _client;
            MongoServer _server;
            MongoDatabase _db;
            public DataAccess()
            {
                _client = new MongoClient("mongodb://localhost:27017");
                _server = _client.GetDatabase("BookstoreDb");
            }
            public IEnumerable<EmployeeInfo> GetBooks()
            {
                return _db.GetCollection<EmployeeInfo>("Books").FindAll();
            }

            public EmployeeInfo GetBook(ObjectId id)
            {
                var res = Query<EmployeeInfo>.EQ(p => p.Id, id);
                return _db.GetCollection<EmployeeInfo>("Books").FindOne(res);
            }
            public EmployeeInfo Create(EmployeeInfo p)
            {
                _db.GetCollection<EmployeeInfo>("Books").Save(p);
                return p;
            }
            public void Update(ObjectId id, EmployeeInfo p)
            {
                p.Id = id;
                var res = Query<EmployeeInfo>.EQ(pd => pd.Id, id);
                var operation = Update<EmployeeInfo>.Replace(p);
                _db.GetCollection<EmployeeInfo>("Books").Update(res, operation);
            }
            public void Remove(ObjectId id)
            {
                var res = Query<EmployeeInfo>.EQ(e => e.Id, id);
                var operation = _db.GetCollection<EmployeeInfo>("Books").Remove(res);
            }

    }
}
