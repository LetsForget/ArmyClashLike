using Cysharp.Threading.Tasks;
using Unity.Entities.Content;
using UnityEngine;

namespace ContentLoading
{
    public class LocalContentArchiveLoader : IContentArchiveLoader
    {
        private readonly string storagePath;
        private readonly string cachePath;
        
        private ContentDeliveryService cdService;
        private ContentDownloadService cdDownloadService;
        
        public LocalContentArchiveLoader(string storagePath, string cachePath)
        {
            this.storagePath = $"{Application.dataPath}/{storagePath}";
            this.cachePath = $"{Application.persistentDataPath}/{cachePath}";
            
            cdDownloadService = new ContentDownloadService("loader", this.cachePath);
            cdService = new ContentDeliveryService();
            cdService.AddDownloadService(cdDownloadService);

        }
        
        public async UniTask Load(string contentKey)
        {
            #if !ENABLE_CONTENT_DELIVERY
            throw new LoadingFailedException("ENABLE_CONTENT_DELIVERY is false");
            #endif

        }
    }
}