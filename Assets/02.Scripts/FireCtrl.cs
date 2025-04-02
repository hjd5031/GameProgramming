using System.Collections;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    [Header("References")]
    [SerializeField]private GameObject bulletPrefab;
    [SerializeField]private Transform bulletSpawn;
    [SerializeField]private GameObject muzzleFlash;
    [SerializeField]private AudioClip fireSfx;           // 총 발사 소리
    private Animation _anim;
    private AudioSource _audioSource;    // AudioSource 컴포넌트
    
    [Header("Attributes")]
    [SerializeField]private float fireCooldown = 0.2f;        // 발사 쿨타임 (초 단위)
    private float _lastFireTime = -1.0f;          // 마지막 발사 시각
    
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();

        _audioSource.playOnAwake = false; // 자동 재생 방지
        _audioSource.spatialBlend = 0.0f; // 2D 사운드
        muzzleFlash.SetActive(false);//시작히 반짝임 방지
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) // 0번은 좌클릭
        {
            if (Time.time - _lastFireTime >= fireCooldown)
            {
                // 발사 가능
                _lastFireTime = Time.time;

                FireBullet();
                Debug.Log("발사!");
            }
            else
            {
                Debug.Log("쿨타임 중입니다...");
            }
        }
    }
    void FireBullet()
    {
        // bulletPrefab을 bulletSpawn 위치에 생성하고 forward 방향으로 힘을 가함
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        // 발사 사운드 재생
        if (fireSfx != null)
            _audioSource.PlayOneShot(fireSfx);
        StartCoroutine(ShowMuzzleFlash());
    }
    IEnumerator ShowMuzzleFlash()
    {
        Vector3 originTransform = muzzleFlash.transform.position;
        float ranInt = Random.Range(-0.05f, 0.05f);//자연스러움 위한 랜덤 위치 설정
        muzzleFlash.transform.position = new Vector3(originTransform.x, originTransform.y+ranInt, originTransform.z+ranInt);
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
        muzzleFlash.transform.position = bulletSpawn.position;

    }
}
