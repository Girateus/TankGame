using System;
using UnityEngine;

public class DestroyableBox : MonoBehaviour
{
    [SerializeField] private float _capForce;
    [SerializeField] private float _boxForce;

    [SerializeField] private Rigidbody _capRb;
    [SerializeField] private Rigidbody _boxRb;
    
    public Action<DestroyableBox> OnBoxDestroyed;

    private bool _shouldExplode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldExplode)
        {
            
            _shouldExplode = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") || other.CompareTag("Tank"))
        {
            Debug.Log("box touched");
            _capRb.AddForce(Vector3.up * _capForce);
            _boxRb.AddForce(other.transform.position - transform.position * _boxForce);
            Collider myCollider =GetComponent<Collider>();
            Destroy(myCollider);
            
            OnBoxDestroyed.Invoke(this);
            
        }
        
    }
    
}
