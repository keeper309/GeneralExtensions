using Cysharp.Threading.Tasks;

namespace GameCore.GeneralExtensions
{
    public interface IInitializable
    {
        UniTask Initialize(IProgressReceiver progressReceiver);
    }
}
