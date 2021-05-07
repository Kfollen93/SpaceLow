using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _bulletDuration, _bulletSpeed;
    private float _lifeTimer;
    public GameObject collisionExplosion;

    private void Start()
    {
        _lifeTimer = _bulletDuration;
    }

    private void Update() 
    { 
        transform.position += transform.forward * _bulletSpeed * Time.deltaTime;

        _lifeTimer -= Time.deltaTime; 

        if (_lifeTimer <= 0f)
        { 
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground"))
        {
            GameObject explosion = Instantiate(collisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 2f);
        }
    }
}
