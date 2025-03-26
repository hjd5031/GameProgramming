using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BarrelHit : MonoBehaviour
{
    [FormerlySerializedAs("_hp")] public int hp;
    [FormerlySerializedAs("sparkEffect")] public GameObject[] explosionEffect;

    private GameObject _explosion;
    private GameObject _flame;
    private GameObject _smoke1;
    private GameObject _smoke2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = 3;
    }

    // Update is called once per frame
    void Update()
    {
        // CheckHp();
    }

   
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            hp--;
        }
        CheckHp(collision);
    }
    void CheckHp(Collision collision)
    {
        ContactPoint cp = collision.GetContact(0);
        Quaternion rot = Quaternion.LookRotation(-cp.normal);
        if (hp == 2)
        {
            _smoke1 = Instantiate(explosionEffect[0], cp.point, rot);
        }
        if (hp == 1)
        {
            _smoke2 = Instantiate(explosionEffect[0], cp.point, rot);
        }
        if (hp == 0)
        {
            _flame = Instantiate(explosionEffect[1], cp.point, rot);
            StartCoroutine("StartExplosion");
        }
    }

    IEnumerator StartExplosion()
    {
        yield return new WaitForSeconds(3f);
        Destroy(_smoke1);
        Destroy(_smoke2);
        Destroy(_flame);
        _explosion = Instantiate(explosionEffect[2], transform.position, Quaternion.identity);
        Destroy(_explosion,3f);
        Destroy(this.gameObject);

    }
}