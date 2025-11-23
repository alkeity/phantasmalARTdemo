using Microsoft.AspNetCore.Mvc;
using Minio;

namespace ASPNET_CourseProject.Services
{
    public interface IStorageService
    {
        public Task UploadArt(string fileName, string filePath);
        public Task UploadAvatar(string fileName, string filePath);
        //public Task GetFilePath([FromServices] IMinioClient minioClient);
    }
}
