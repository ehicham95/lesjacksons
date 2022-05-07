using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();
        Platform GetPlatformById(int id);
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
    }
}