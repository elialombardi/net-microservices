using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectsService.Models
{
    public class TodoItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public int ExternalID { get; set; }

        [Required]
        public string Title { get; set; }

        public string ProjectId { get; set; }
        public Project Project { get; set; }
    }
}