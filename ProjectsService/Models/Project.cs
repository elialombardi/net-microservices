using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProjectsService.Dtos;

namespace ProjectsService.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ExternalId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}