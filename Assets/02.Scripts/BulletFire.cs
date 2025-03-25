using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    private Animation _anim;

    
    private float lastFireTime = -1.0f;          // 마지막 발사 시각
    [SerializeField]private float fireCooldown = 0.2f;        // 발사 쿨타임 (초 단위)
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _anim = GetComponent<Animation>();
        _anim.Play();

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
                Debug.Log("발사!");
            }
            else
            {
                Debug.Log("쿨타임 중입니다...");
            }
        }
    }
    private void FireBullet()
    {
        // bulletPrefab을 bulletSpawn 위치에 생성하고 forward 방향으로 힘을 가함
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        _anim.Play("IdleFireSMG");
        // Rigidbody rb = bullet.GetComponent<Rigidbody>();
    }
}
