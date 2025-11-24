using Microsoft.AspNetCore.Mvc;
using Minio;

namespace ASPNET_CourseProject.Services
{
    public interface IStorageService
    {
        public Task UploadArt(string fileName, Stream stream);
        public Task UploadAvatar(string fileName, Stream stream);
        //public Task GetFilePath([FromServices] IMinioClient minioClient);
    }
}
