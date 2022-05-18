using PlatformService.Dtos;
using System.Threading.Tasks;

namespace PlatformService.SyncDataServices.Http 
{
    public interface ICommandDataClient
    {
        Task SendPlatformToComand(PlatformReadDto plat);
    }
}