using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        bool SaveChangtes();

        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);
        void CreatePlatform(Platform plat);
    }
}