using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0번은 좌클릭
        {
            FireBullet();
        }
    }
    private void FireBullet()
    {
        // bulletPrefab을 bulletSpawn 위치에 생성하고 forward 방향으로 힘을 가함
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        // Rigidbody rb = bullet.GetComponent<Rigidbody>();
    }
}
