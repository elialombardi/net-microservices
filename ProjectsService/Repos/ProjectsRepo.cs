using System.Collections.Generic;
using MongoDB.Driver;
using ProjectsService.Models;

namespace ProjectsService.Repos
{
    public interface IProjectsRepo
    {
        Project Create(Project project);
        List<Project> Get();
        Project Get(string id);
        void Remove(Project projectIn);
        void Remove(string id);
        void Update(string id, Project projectIn);
    }

    public class ProjectsRepo : IProjectsRepo
    {
        private readonly IMongoCollection<Project> projects;
        public ProjectsRepo(IProjectsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            projects = database.GetCollection<Project>(settings.ProjectsCollectionName);
        }

        public List<Project> Get() =>
            projects.Find(project => true).ToList();

        public Project Get(string id) =>
            projects.Find<Project>(project => project.Id == id).FirstOrDefault();

        public Project Create(Project project)
        {
            projects.InsertOne(project);
            return project;
        }

        public void Update(string id, Project projectIn) =>
            projects.ReplaceOne(project => project.Id == id, projectIn);

        public void Remove(Project projectIn) =>
            projects.DeleteOne(project => project.Id == projectIn.Id);

        public void Remove(string id) =>
            projects.DeleteOne(project => project.Id == id);
    }
}