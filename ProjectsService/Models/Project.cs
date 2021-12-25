using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectsService.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [BsonDefaultValue(BsonDateTime.Create())]
        public BsonDateTime CreatedOn { get; set; }
        public BsonDateTime ModifiedOn { get; set; }
        public BsonDateTime DeletedOn { get; set; }
    }
}