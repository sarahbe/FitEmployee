using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeApp.Models
{
    public class EmployeeInfo
    {
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("PhoneNumber")]
        public int PhoneNumber { get; set; }
        [BsonElement("EmployeeImage")]
        public byte[] EmployeeImage { get; set; }
        [BsonElement("ImageUrl")]
        public string ImageUrl { get; set; }
    }
}
