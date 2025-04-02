using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BarrelCtrl : MonoBehaviour
{
    [FormerlySerializedAs("_hp")] public int hp;
    [FormerlySerializedAs("sparkEffect")] public GameObject[] explosionEffect;
    public Texture[] textures;
    private MeshRenderer _renderer;
    private GameObject _explosion;
    private GameObject _flame;
    private GameObject _smoke1;
    private GameObject _smoke2;
    private Rigidbody _rb;
    private float _radius = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
        int idx = Random.Range(0, textures.Length);
        _renderer.material.mainTexture = textures[idx];
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
        switch (hp)
        {
            case 2:_smoke1 = Instantiate(explosionEffect[0], cp.point, rot); break;
            case 1:_smoke2 = Instantiate(explosionEffect[0], cp.point, rot); break;
            case 0:
            {
                _flame = Instantiate(explosionEffect[1], cp.point, rot,this.transform);
                StartCoroutine("StartExplosion");
            }
                break;
            case -1:
            {
                StopCoroutine("StartExplosion");
                BarrelFinalExplosion();
            }
                break;
            
        }
    }

    private void BarrelFinalExplosion()
    {
        if(_flame != null)Destroy(_flame);
        if(_smoke1 != null)Destroy(_smoke1);
        if(_smoke2 != null)Destroy(_smoke2);
        
        IndirectDamage(gameObject.transform.position);
        _explosion = Instantiate(explosionEffect[2], transform.position, Quaternion.identity);
        Destroy(_explosion,3f);
        Destroy(gameObject,3f);
    }
    IEnumerator StartExplosion()
    {
        yield return new WaitForSeconds(3f);
        BarrelFinalExplosion();
    }

    void IndirectDamage(Vector3 pos)
    {
        Collider[] colls = Physics.OverlapSphere(pos, _radius, 1<<3);
        foreach (var coll in colls)
        {
            _rb = coll.GetComponent<Rigidbody>();
            _rb.mass = 1.0f;
            _rb.constraints = RigidbodyConstraints.None;
            _rb.AddExplosionForce(1500.0f, new Vector3(pos.x+1, pos.y, pos.z), _radius, 1200.0f);
            StartCoroutine("RecoverMass",_rb);
            StartCoroutine("FreezePosition",_rb);
        }
    }

    IEnumerator RecoverMass(Rigidbody rb)
    { 
        while (rb.mass <= 20000f)
        {
            rb.mass += Time.deltaTime * 5000f;
            yield return null;
        }
    }
    IEnumerator FreezePosition(Rigidbody rb)
    {
        Debug.Log("BeingRecovered");
        
        yield return new WaitForSeconds(3f);
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        Debug.Log("Recovered");
    }
}