using Core.Singleton;

namespace Constrollers
{
    public abstract class BaseController<T> : SingletonMonoBahaviour<T> where T : BaseController<T>
    {
    }
}
