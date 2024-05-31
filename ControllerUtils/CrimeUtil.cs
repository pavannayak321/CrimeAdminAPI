using Azure.Storage.Blobs;

namespace CrimeAdminAPI.ControllerUtils
{
    public class CrimeUtil
    {
        private readonly BlobServiceClient _blobServiceClient;
        public CrimeUtil(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        private async Task<string> UploadFileToBlobStorage(IFormFile file)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient("");
            var blobClient = blobContainerClient.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }
            return blobClient.Uri.ToString();
        }
    }
}
