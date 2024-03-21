using Android.Content;

namespace DocumentReaderSample.Platforms.Android
{
    public class PhotoPickerService : IPhotoPickerService
    {
        public Task<Stream> GetImageStreamAsync()
        {
            return Task.Run<Stream>(() => new MemoryStream());
        }
    }
}