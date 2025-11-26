using Unity.VisualScripting;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    //[SerializeField] private GameObject chest;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Tank"))
        {
            Debug.Log("I'm collided with " + other.gameObject.name);
            Destroy(gameObject);
        }
       
    }
}
