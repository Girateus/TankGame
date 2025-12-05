using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _forceIntensity = 50f; 
    [SerializeField] private float _damage = 50;
    
    [SerializeField] private GameObject _impact;
    
    private Rigidbody _rigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody)
            _rigidbody.AddRelativeForce(Vector3.forward * _forceIntensity, ForceMode.VelocityChange);
        //same thing
        //_rigidbody.AddForce(transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("I'm collided with " + collision.gameObject.name);
        OnTouch(collision.gameObject);
        //Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I'm collided with " + other.gameObject.name);
       OnTouch(other.gameObject);
        // Destroy(gameObject);
    }

    private void OnTouch(GameObject touchObject)
    {
        if (touchObject.TryGetComponent(out DestroyableBox box))
        {
            Collider touchcollider =GetComponent<Collider>();
            box.SetProjectilePosition(transform.position);
        }
        if (touchObject.TryGetComponent(out DamageTaker damageTaker))
        {
            damageTaker.TakeDamage(_damage);
        }
        
        Instantiate(_impact, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
    
}
