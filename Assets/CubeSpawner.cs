using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private List<Cube> _startCubes;

    [SerializeField] private Cube _cube;
    [SerializeField] private uint _minCubesSpawn;
    [SerializeField] private uint _maxCubesSpawn;
    [SerializeField] private float _spawnInaccuray;
    [SerializeField] private float _explosionForce;

    public void OnEnable()
    {
        foreach(Cube cube in _startCubes)
        {
            cube.CubeExplode += OnCubeExplode;
        }
    }

    private void OnCubeExplode(Cube cube)
    {
        cube.CubeExplode -= OnCubeExplode;

        if(cube.SpawnChance > Random.Range(0, 100))
        {
            SpawnCube(cube);
        }
    }

    private void SpawnCube(Cube cube)
    {
        Cube spawnedCube;

        for(int i = 0; i <= Random.Range(_minCubesSpawn, _maxCubesSpawn +1); i++)
        {
            spawnedCube = Instantiate(_cube, new Vector3(cube.Transform.position.x + Random.Range(-_spawnInaccuray, _spawnInaccuray), 
                cube.Transform.position.y + Random.Range(-_spawnInaccuray, _spawnInaccuray), cube.Transform.position.z + Random.Range(-_spawnInaccuray, _spawnInaccuray)), cube.Transform.rotation).GetComponent<Cube>();

            spawnedCube.CubeExplode += OnCubeExplode;
            spawnedCube.Construct(cube.Scale / 2, cube.SpawnChance / 2);
            ApplyExplosionForce(cube, spawnedCube);
        }
    }

    private void ApplyExplosionForce(Cube oldCube, Cube spawnedCube)
    {
        spawnedCube.Rigidbody.AddForce((oldCube.Transform.position - spawnedCube.Transform.position).normalized * _explosionForce);
    }
}
