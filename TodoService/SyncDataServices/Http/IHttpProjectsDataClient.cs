using System.Threading.Tasks;
using TodoService.Dtos;

namespace TodoServuce.SyncDataServices.Http
{
    public interface IHttpProjectsDataClient
    {
        Task SendItemToProject(TodoItemRead data);
    }
}