using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {  
      if (collision.collider.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
