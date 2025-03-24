using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //References
    private Transform _tr;
    private Animation _anim;

    [Header("Attributes")]
    public float velocity;
    public float rotationSpeed;

    
    // public float bulletSpeed = 20f;

    void Start()
    {
        _tr = GetComponent<Transform>();
        _anim = GetComponent<Animation>();
        _anim.Play("Idle");
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        // 이동
        if (h != 0 || v != 0)
        {
            Vector3 moveDirection = new Vector3(h, 0, v);
            _tr.Translate(moveDirection * Time.deltaTime * velocity, Space.Self);
        }

        // 회전
        if (r != 0)
        {
            _tr.Rotate(Vector3.up * r * Time.deltaTime * rotationSpeed);
        }

        // 애니메이션 처리
        StartPlayerAnim(v, h);

        // 마우스 좌클릭 -> 총알 발사
        
    }

    private void StartPlayerAnim(float v, float h)
    {
        if (v > 0) _anim.CrossFade("RunF", 0.25f);
        else if (v < 0) _anim.CrossFade("RunB", 0.25f);
        else if (h > 0) _anim.CrossFade("RunL", 0.25f);
        else if (h < 0) _anim.CrossFade("RunR", 0.25f);
        else _anim.CrossFade("Idle", 0.25f);
    }

    
}