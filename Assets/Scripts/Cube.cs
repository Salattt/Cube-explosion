using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Explosion _explosion;
    [SerializeField] private float _maxExplosionRadius;
    [SerializeField] private float _maxExplosionForce;
    [SerializeField] private float _biggestExplosionScale;

    public event Action<Cube> Explode;

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

    public void Init(float scale, float spawnChance)
    {
        Transform.localScale = new Vector3(scale, scale, scale);
        Scale = scale;
        SpawnChance = spawnChance;
        GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0,1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    public void CreateExplosion()
    {
        float scaleFactor = Mathf.Clamp01(_biggestExplosionScale / Scale);
        _explosion.Explode(scaleFactor * _maxExplosionRadius, scaleFactor * _maxExplosionForce);
    }

    private void OnMouseDown()
    {
        Explode.Invoke(this);

        Destroy(gameObject, Time.fixedDeltaTime);
    }
}
