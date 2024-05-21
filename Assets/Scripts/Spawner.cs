using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IInitializable, IColorable
{
    [SerializeField] protected T Object;

    protected ObjectPool<T> Pool;

    private void Awake()
    {
        Pool = new ObjectPool<T>(
        createFunc: Create,
        actionOnGet: ReceiveObject,
        actionOnRelease: (gameObject) => gameObject.gameObject.SetActive(false));
    }

    public virtual void ReceiveObject(T obj)
    {
        obj.gameObject.SetActive(true);
        obj.SetStartColor();
    }

    public virtual T Create()
    {
        T obj = Instantiate(Object);
        obj.Initialize(this, Vector3.zero);
        return obj;
    }

    public virtual void ReturnItem(T obj)
    {
        Pool.Release(obj);
    }
}
