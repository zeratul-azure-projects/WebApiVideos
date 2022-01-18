using WebApiVideos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using System.IO;

namespace WebApiVideos.Services
{
    public class FileManager : IFileManager
    {
        private readonly BlobServiceClient _blobServiceClient;
        public FileManager(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        public async Task Delete(string imageName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("dir2");
            var blobClient = blobContainer.GetBlobClient(imageName);
            await blobClient.DeleteAsync();
        }

        public async Task<byte[]> Get(string imageName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("dir2");
            var blobClient = blobContainer.GetBlobClient(imageName);
            var blobContent = await blobClient.DownloadAsync();

            using(MemoryStream ms = new MemoryStream())
            {
                await blobContent.Value.Content.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        public async Task Upload(FileModel model)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("dir2");
            var blobClient = blobContainer.GetBlobClient(model.ImageFile.FileName);
            await blobClient.UploadAsync(model.ImageFile.OpenReadStream());
        }

    
    }
}
