using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Transform _tr;
    private Animation _anim;

    [Header("Attributes")]
    public float velocity;
    public float rotationSpeed;

    private float lastFireTime = -1.0f;          // 마지막 발사 시각
    private float fireCooldown = 0.5f;        // 발사 쿨타임 (초 단위)

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
        StartPlayerAnim(v, h,r);
    }

    private void StartPlayerAnim(float v, float h, float r)
    {
        // if (Input.GetMouseButton(0))
        // {
        //     // if (Time.time - lastFireTime >= fireCooldown)
        //     // {
        //     //     // 발사 가능
        //     //     lastFireTime = Time.time;
        //     //
        //     //     if (h != 0 || v != 0)
        //     //         _anim.Play("RunFireSMG");
        //     //     else
        //     //         _anim.Play("IdleFireSMG");
        //     //
        //     //     // 여기에 총알 발사 코드도 추가하면 됩니다.
        //     //     Debug.Log("발사!");
        //     // }
        //     // else
        //     // {
        //     //     Debug.Log("쿨타임 중입니다...");
        //     // }
        // }
        // else
        // {
            if (v > 0) _anim.CrossFade("RunF", 0.25f);
            else if (v < 0) _anim.CrossFade("RunB", 0.25f);
            else if (h > 0||r<0) _anim.CrossFade("RunL", 0.25f);
            else if (h < 0||r>0) _anim.CrossFade("RunR", 0.25f);
            else _anim.CrossFade("Idle", 0.25f);
        // }
    }
}