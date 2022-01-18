using WebApiVideos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiVideos.Services
{
    public interface IFileManager
    {
        Task<byte[]> Get(string imageName);
        Task Upload(FileModel model);
        Task Delete(string imageName);
    }
}
