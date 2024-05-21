using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private Cube _cube;
    [SerializeField] private float _delay;
    [SerializeField] private Transform[] _spawnPoints;

    public event Action<Vector3> CubeDeactivated;

    private void Start()
    {
        StartCoroutine(ExtractingElement());
    }

    public override void ReturnItem(Cube cube)
    {
        CubeDeactivated?.Invoke(cube.transform.position);
        base.ReturnItem(cube);
    }

    public override void ReceiveObject(Cube cube)
    {
        cube.CancelInvoke(nameof(cube.Deactivate));
        cube._isDeactivated = false; 
        int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
        _spawnPoints[spawnPointNumber].transform.position = new Vector3(Random.Range(5.0f, 15.0f), Random.Range(5.0f, 15.0f), Random.Range(5.0f, 15.0f));
        cube.transform.position = _spawnPoints[spawnPointNumber].transform.position;
        cube.gameObject.SetActive(true);
        cube.SetStartColor();
    }

    private IEnumerator ExtractingElement()
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        while (enabled)
        {
            Pool.Get();

            yield return waitForSeconds;
        }
    }
}
