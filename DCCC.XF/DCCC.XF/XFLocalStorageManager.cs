using DCCC.BaseImplementations;
using PCLStorage;
using System.Threading.Tasks;

namespace DCCC.XF
{
    public class XFLocalStorageManager : BaseLocalStorageManager
    {
        private readonly IFolder _storageFolder;

        public XFLocalStorageManager()
        {
            _storageFolder = FileSystem.Current.LocalStorage;
        }

        protected override async Task DeleteFile(string fileName)
        {
            var file = await GetFile(fileName);
            await file?.DeleteAsync();
        }

        protected override async Task<string> ReadFile(string fileName)
        {
            var file = await GetFile(fileName);
            return await file?.ReadAllTextAsync();
        }

        protected override async Task WriteToFile(string fileName, string content)
        {
            var file = await GetFile(fileName, true);
            await file.WriteAllTextAsync(content);
        }

        private async Task<IFile> GetFile(string fileName, bool create = false)
        {
            var fileExists = await _storageFolder.CheckExistsAsync(fileName);

            if (fileExists == ExistenceCheckResult.FileExists)
                return await _storageFolder.GetFileAsync(fileName);

            if (create)
                return await _storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            return null;
        }
    }
}
