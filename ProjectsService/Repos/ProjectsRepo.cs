using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using ProjectsService.Data;
using ProjectsService.Dtos;
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
        Task<bool> TodoItemExists(string todoItemId);
        Task<bool> ExternalTodoItemExists(int externalTodoItemId);
        TodoItem CreateTodoItem(TodoItem todoItem);
        TodoItem AddTodoItem(TodoItem todoItem);
        Task<List<Project>> CreateMany(List<Project> newProjects);
        Task<long> Count();
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

        public async Task<List<Project>> CreateMany(List<Project> newProjects)
        {
            await projects.InsertManyAsync(newProjects);
            return newProjects;
        }

        public void Update(string id, Project projectIn) =>
            projects.ReplaceOne(project => project.Id == id, projectIn);

        public void Remove(Project projectIn) =>
            projects.DeleteOne(project => project.Id == projectIn.Id);

        public void Remove(string id) =>
            projects.DeleteOne(project => project.Id == id);

        public async Task<bool> TodoItemExists(string todoItemId) =>
        await projects.CountDocumentsAsync(Builders<Project>.Filter.Where(x => x.TodoItems.Any(item => item.Id == todoItemId))) > 0;
        public async Task<bool> ExternalTodoItemExists(int externalTodoItemId) =>
            await projects.CountDocumentsAsync(Builders<Project>.Filter.Where(x => x.TodoItems.Any(item => item.ExternalID == externalTodoItemId))) > 0;
        public TodoItem CreateTodoItem(TodoItem todoItem)
        {
            var project = Get(todoItem.ProjectId);
            project.TodoItems.Add(todoItem);
            Update(project.Id, project);
            return todoItem;
        }

        public TodoItem AddTodoItem(TodoItem todoItem)
        {
            throw new System.NotImplementedException();
        }

        public async Task<long> Count() =>
            await projects.EstimatedDocumentCountAsync();

    }
}