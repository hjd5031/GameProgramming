using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletCtrl : MonoBehaviour
{
    public float damage = 20.0f;

    public float force = 1500.0f;
    private Rigidbody _rb;
    
    public GameObject sparkEffect;

    private GameObject _spark;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * force);
        StartCoroutine("RemoveBullet");
    }

    IEnumerator RemoveBullet()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        
        ContactPoint cp = collision.GetContact(0);
        Quaternion rot = Quaternion.LookRotation(-cp.normal);
        _spark = Instantiate(sparkEffect, cp.point, rot);
        Destroy(_spark, 0.5f);
        Destroy(this.gameObject);
        // private int hp = GetCompo;
        // if (collision.collider.CompareTag("Barrel"))
        // {
        //     collision.gameObject.hp -= damage;
        // }
        
    }
}
