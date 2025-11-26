using UnityEngine;

public class Tree : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("boom " + other.gameObject.name);
            Destroy(gameObject);
        }
        
    }
}
