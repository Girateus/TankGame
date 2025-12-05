using System;
using UnityEngine;

public class DestroyableBox : MonoBehaviour
{
    [SerializeField] private float _capForce;
    [SerializeField] private float _boxForce;

    [SerializeField] private Rigidbody _capRb;
    [SerializeField] private Rigidbody _boxRb;
    
    public Action<DestroyableBox> OnBoxDestroyed;

   // private bool _shouldExplode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        if (_shouldExplode)
        {
            
            _shouldExplode = false;
        }
    }*/

    private Vector3 _projectilePosition;

    public void SetProjectilePosition(Vector3 projectilePosition)
    {
        _projectilePosition = projectilePosition;
    }
    
    /*private void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Projectile") || other.CompareTag("Tank"))
        {
            Debug.Log("box touched");
            Explode(other);
        }
        _projectile = other;
        
    }*/

    public void Explode()
    {
        Debug.Log("Explode");
        _capRb.AddForce(Vector3.up * _capForce);
        _boxRb.AddForce((_projectilePosition - transform.position) * _boxForce);
        Collider myCollider = GetComponent<Collider>();
        Destroy(myCollider);
            
        OnBoxDestroyed.Invoke(this);
    }
}
