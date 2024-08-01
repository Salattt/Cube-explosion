using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(SphereCollider))]
public class Explosion : MonoBehaviour
{
    private SphereCollider _sphereCollider;
    private Transform _transform;
    private float _force;
    private float _radius;

    public void Awake()
    {
        _transform = transform;
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.enabled = false;
    }

    public void Explode(float radius, float force)
    {
        _sphereCollider.radius = radius;
        _force = force;
        _radius = radius;
        _sphereCollider.enabled=true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Cube>(out Cube cube))
        {
            Vector3 direction = cube.Transform.position - _transform.position;

            cube.Rigidbody.AddForce(direction.normalized * Mathf.Clamp01(1 - (_radius - direction.magnitude) / _radius) * _force, ForceMode.VelocityChange);
        }
    }
}
