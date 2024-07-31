using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
    public event Action<Cube> CubeExplode;

    public Transform Transform { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public float Scale { get; private set;  }
    public float SpawnChance { get; private set; } = 100f;

    public void Awake()
    {
        Transform = transform;
        Rigidbody = GetComponent<Rigidbody>();
        Scale = transform.localScale.x;
    }

    public void Construct(float scale, float spawnChance)
    {
        Transform.localScale = new Vector3(scale, scale, scale);
        Scale = scale;
        SpawnChance = spawnChance;
        GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0,1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    private void OnMouseDown()
    {
        CubeExplode.Invoke(this);

        Destroy(gameObject);
    }
}
