using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private List<Cube> _startCubes;

    [SerializeField] private Cube _prefab;
    [SerializeField] private uint _minCubesSpawn;
    [SerializeField] private uint _maxCubesSpawn;
    [SerializeField] private float _spawnInaccuray;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _spawnedCubeScaleDivider;
    [SerializeField] private float _spawnedCubeSpawnChanceDivider;

    public void OnEnable()
    {
        foreach(Cube cube in _startCubes)
        {
            cube.Explode += OnCubeExploded;
        }
    }

    private void OnCubeExploded(Cube cube)
    {
        cube.Explode -= OnCubeExploded;

        if(cube.SpawnChance > Random.Range(0, 100))
        {
            SpawnCubes(cube);
        }
    }

    private void SpawnCubes(Cube cube)
    {
        Cube spawnedCube;
        int spawnedCubesCount = (int)Random.Range(_minCubesSpawn, _maxCubesSpawn + 1);

        for(int i = 0; i <= spawnedCubesCount; i++)
        {
            spawnedCube = Instantiate(_prefab, new Vector3(cube.Transform.position.x + Random.Range(-_spawnInaccuray, _spawnInaccuray), 
                cube.Transform.position.y + Random.Range(-_spawnInaccuray, _spawnInaccuray), cube.Transform.position.z + Random.Range(-_spawnInaccuray, _spawnInaccuray)), cube.Transform.rotation);

            spawnedCube.Explode += OnCubeExploded;
            spawnedCube.Init(cube.Scale / _spawnedCubeScaleDivider, cube.SpawnChance / _spawnedCubeSpawnChanceDivider);
            ApplyExplosionForce(cube, spawnedCube);
        }
    }

    private void ApplyExplosionForce(Cube oldCube, Cube spawnedCube)
    {
        spawnedCube.Rigidbody.AddForce((oldCube.Transform.position - spawnedCube.Transform.position).normalized * _explosionForce);
    }
}
