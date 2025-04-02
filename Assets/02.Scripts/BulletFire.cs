using System.Collections;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    private Animation _anim;
    public GameObject muzzleFlash;
    public AudioClip fireSfx;           // 총 발사 소리
    
    
    private AudioSource audioSource;    // AudioSource 컴포넌트
    private float lastFireTime = -1.0f;          // 마지막 발사 시각
    [SerializeField]private float fireCooldown = 0.2f;        // 발사 쿨타임 (초 단위)
    
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false; // 자동 재생 방지
        audioSource.spatialBlend = 0.0f; // 2D 사운드
        muzzleFlash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) // 0번은 좌클릭
        {
            if (Time.time - lastFireTime >= fireCooldown)
            {
                // 발사 가능
                lastFireTime = Time.time;

                FireBullet();
                // Debug.Log("발사!");
            }
            else
            {
                // Debug.Log("쿨타임 중입니다...");
            }
        }
    }
    public void FireBullet()
    {
        // bulletPrefab을 bulletSpawn 위치에 생성하고 forward 방향으로 힘을 가함
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        // 발사 사운드 재생
        if (fireSfx != null)
            audioSource.PlayOneShot(fireSfx);
        StartCoroutine("ShowMuzzleFlash");
    }
    IEnumerator ShowMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        muzzleFlash.SetActive(false);

    }
}
