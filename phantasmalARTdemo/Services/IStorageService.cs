using Microsoft.AspNetCore.Mvc;
using Minio;

namespace phantasmalARTdemo.Services
{
    public interface IStorageService
    {
        public Task UploadArt(string fileName, Stream stream);
        public Task UploadAvatar(string fileName, Stream stream);
        //public Task GetFilePath([FromServices] IMinioClient minioClient);
    }
}
