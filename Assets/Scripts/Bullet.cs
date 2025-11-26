using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _forceIntensity = 50f;   
    private Rigidbody _rigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I'm collided with " + other.gameObject.name);
        Destroy(gameObject);
    }
}
