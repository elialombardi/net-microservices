namespace ProjectsService.Models
{
    public class ProjectsDatabaseSettings : IProjectsDatabaseSettings
    {
        public string ProjectsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}