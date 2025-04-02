using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BarrelCtrl : MonoBehaviour
{
    [Header("References")]
    public GameObject[] explosionEffect;
    public Texture[] textures;
    private MeshRenderer _renderer;
    private GameObject _explosion;
    private GameObject _flame;
    private GameObject _smoke1;
    private GameObject _smoke2;
    private Rigidbody _rb;
    
    [Header("Attributes")]
    [SerializeField]private float radius;
    private int _hp;
    
    
    void Start()
    {
        //배럴에 랜덤 Texture 적용
        _renderer = GetComponentInChildren<MeshRenderer>();
        int idx = Random.Range(0, textures.Length);
        _renderer.material.mainTexture = textures[idx];
        _hp = 3;//배럴 목숨 3개
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))//Bullet과 충돌 시 Barrel 체력--
        {
            _hp--;
            CheckHp(collision);
        }
    }
    void CheckHp(Collision collision)//Barrel이 Bullet에 맞으면 적절한 효과로 대응
    {
        Transform parent = transform;
        ContactPoint cp = collision.GetContact(0);
        Quaternion rot = Quaternion.LookRotation(-cp.normal);
        switch (_hp)
        {
            case 2:_smoke1 = Instantiate(explosionEffect[0], cp.point, rot, parent); break;//1회 연기
            case 1:_smoke2 = Instantiate(explosionEffect[0], cp.point, rot, parent); break;//2회 연기
            case 0:                                                                        //3회 화염 후 3초뒤 자동폭발
            {
                _flame = Instantiate(explosionEffect[1], cp.point, rot, parent);
                StartCoroutine(StartExplosion());
            }
                break;
            case -1:                                                                        //4회 3초이내로 격발 시 즉시 폭발
            {
                StopCoroutine(StartExplosion());
                BarrelFinalExplosion();
            }
                break;
            
        }
    }

    private void BarrelFinalExplosion()//Barrel 최종 폭발시 효과 제거 후 폭발 효과 추가
    {
        if(_flame != null)Destroy(_flame);
        if(_smoke1 != null)Destroy(_smoke1);
        if(_smoke2 != null)Destroy(_smoke2);
        
        IndirectDamage(gameObject.transform.position);
        _explosion = Instantiate(explosionEffect[2], transform.position, Quaternion.identity);
        Destroy(_explosion,3f);
        Destroy(gameObject,3f);
    }

    void IndirectDamage(Vector3 pos)//주변 Barrel에 폭발 영향 주기
    {
        Collider[] colls = Physics.OverlapSphere(pos, radius, 1<<3);
        foreach (var coll in colls)
        {
            _rb = coll.GetComponent<Rigidbody>();
            _rb.mass = 1.0f;
            _rb.constraints = RigidbodyConstraints.None;
            _rb.AddExplosionForce(1500.0f, new Vector3(pos.x+1, pos.y, pos.z), radius, 1200.0f);
            StartCoroutine(RecoverMass(_rb));//영향 받은 Barrel들의 mass 복구
            StartCoroutine(FreezeRotation(_rb));//              rotation 고정
        }
    }
    //--------------------------------------------------------------------------------------------for coroutine
    IEnumerator StartExplosion()
    {
        yield return new WaitForSeconds(3f);
        BarrelFinalExplosion();
    }

    IEnumerator RecoverMass(Rigidbody rb)
    { 
        while (rb.mass <= 20000f)
        {
            rb.mass += Time.deltaTime * 5000f;
            yield return null;
        }
    }
    IEnumerator FreezeRotation(Rigidbody rb)
    {
        Debug.Log("BeingRecovered");
        yield return new WaitForSeconds(3f);
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        Debug.Log("Recovered");
    }
}