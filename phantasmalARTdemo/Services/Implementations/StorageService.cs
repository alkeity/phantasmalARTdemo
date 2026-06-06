using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace phantasmalARTdemo.Services.Implementations
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

        public async Task UploadArt(string fileName, Stream stream)
        {
            await UploadFile("art", fileName, stream);
        }

        public async Task UploadAvatar(string fileName, Stream stream)
        {
            await UploadFile("avatars", fileName, stream);
        }

        private async Task UploadFile(string folderName, string fileName, Stream stream)
        {
            if (stream == null || stream.Length == 0)
            {
                throw new ArgumentException("Stream is null");
            }
            string contentType = "image/png"; // TODO check and set actual content type
            fileName = $"{folderName}/{fileName}";
            try
            {
                // upload file
                var putObjArgs = new PutObjectArgs()
                                    .WithBucket(_bucketName)
                                    .WithObject(fileName)
                                    .WithStreamData(stream)
                                    .WithObjectSize(stream.Length)
                                    .WithContentType(contentType);
                await _minioClient.PutObjectAsync(putObjArgs);

                // check if file actually uploaded
                StatObjectArgs statObjArgs = new StatObjectArgs().WithBucket(_bucketName).WithObject(fileName);
                ObjectStat objStat = await _minioClient.StatObjectAsync(statObjArgs);
                Console.WriteLine(objStat.ObjectName);
            }
            catch (MinioException exc)
            {
                // TODO logger
                Console.WriteLine($"File upload error: {exc.Message}");
            }
        }
    }
}
