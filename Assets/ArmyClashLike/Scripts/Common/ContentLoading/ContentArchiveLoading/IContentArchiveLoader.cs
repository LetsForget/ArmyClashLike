using Cysharp.Threading.Tasks;

namespace ContentLoading
{
    public interface IContentArchiveLoader
    {
        public UniTask Load(string contentKey);
    }
}