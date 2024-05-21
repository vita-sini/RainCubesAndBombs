using UnityEngine;
using UnityEngine.Pool;

public class SpawnerBomb : Spawner<Bomb>
{
    [SerializeField] private Bomb _bomb;
    [SerializeField] private SpawnerCube _spawnerCube;

    private void Start()
    {
        _spawnerCube.CubeDeactivated += Activation;
    }

    private void Activation(Vector3 position)
    {
        Bomb newBomb = Pool.Get(); 
        newBomb.transform.position = position; 
        ReceiveObject(newBomb); 
    }

    public override void ReturnItem(Bomb bomb)
    {
        base.ReturnItem(bomb);
    }

    public override void ReceiveObject(Bomb bomb)
    {
        bomb.gameObject.SetActive(true);
        bomb.Initialize(this, Vector3.zero);
        bomb.Setup();
        bomb.SetStartColor();
    }
}
