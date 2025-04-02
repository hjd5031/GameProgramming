using UnityEngine;
public class RemoveBullet : MonoBehaviour
{ 
    [Header("References")]
    [SerializeField]private GameObject sparkEffect;
    private GameObject _spark;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            ContactPoint cp = collision.GetContact(0);
            Quaternion rot = Quaternion.LookRotation(-cp.normal);
            _spark = Instantiate(sparkEffect, cp.point, rot);
            Destroy(_spark, 0.5f);
            Destroy(collision.gameObject);
        }
    }
}
