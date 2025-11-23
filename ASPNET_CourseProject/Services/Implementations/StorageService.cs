using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace ASPNET_CourseProject.Services.Implementations
{
    public class StorageService : IStorageService
    {
        private readonly IMinioClient _minioClient;
        private readonly string _bucketName;

        public StorageService(IMinioClient minioClient, IConfiguration config)
        {
            _minioClient = minioClient;
            _bucketName = config.GetSection("ObjectStorage")
                                .GetValue<string>("BucketName")
                                ?? throw new MissingFieldException("Failed to get bucket name. Please check your appsettings.");
        }

        public async Task UploadArt(string fileName, string filePath)
        {
            await UploadFile("art", fileName, filePath);
        }

        public async Task UploadAvatar(string fileName, string filePath)
        {
            await UploadFile("avatars", fileName, filePath);
        }

        private async Task UploadFile(string folderName, string fileName, string filePath)
        {
            fileName = Path.Combine(folderName, fileName);
            string contentType = $"image/{Path.GetExtension(filePath)}";
            try
            {
                var putObjArgs = new PutObjectArgs()
                                    .WithBucket(_bucketName)
                                    .WithObject(fileName)
                                    .WithFileName(filePath)
                                    .WithContentType(contentType);
                await _minioClient.PutObjectAsync(putObjArgs).ConfigureAwait(false);
            }
            catch (MinioException exc)
            {
                // TODO logger
                Console.WriteLine($"File upload error: {exc.Message}");
            }
        }
    }
}
