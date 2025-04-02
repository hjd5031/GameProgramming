using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletCtrl : MonoBehaviour
{
    [Header("References")]
    
    private Rigidbody _rb;
    
    
    [Header("Attributes")]
    [SerializeField]private float damage = 20.0f;
    [SerializeField]private float force = 1500.0f;
    
    void Start()
    {
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;//콜라이더 무시 현상 방지(뚫고 지나가기)
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * force);
        StartCoroutine(RemoveBullet());
    }

    IEnumerator RemoveBullet()//총알이 아무것도 맞지 않았을 때 쓰레기처리
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
