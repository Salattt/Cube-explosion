using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
    private float _scale = 1f;
    private float _spawnChance = 100f;

    public event Action<Cube> CubeExplode;

    public Transform Transform { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public float Scale { get { return _scale; } }
    public float SpawnChance { get { return _spawnChance; } }

    public void Awake()
    {
        Transform = transform;
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Construct(float scale, float spawnChance)
    {
        Transform.localScale = new Vector3(scale, scale, scale);
        _scale = scale;
        _spawnChance = spawnChance;
        GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0,1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    private void OnMouseDown()
    {
        CubeExplode.Invoke(this);
    }
}
