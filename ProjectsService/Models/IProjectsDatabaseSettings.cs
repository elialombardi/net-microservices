namespace ProjectsService.Models
{
    public interface IProjectsDatabaseSettings
    {
        string ProjectsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}