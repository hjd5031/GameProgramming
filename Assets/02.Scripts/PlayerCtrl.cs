using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float velocity;
    public float rotationSpeed;
    
    private Transform tr;
    private Animation anim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();
        anim.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        // WASD 키 입력값 가져오기
        float h = Input.GetAxis("Horizontal"); // A, D 키 (좌우)
        float v = Input.GetAxis("Vertical");   // W, S 키 (상하)
        
        // 마우스 X 축 이동값 가져오기 (좌우)
        float r = Input.GetAxis("Mouse X");
        // Translate(tr, Space.Self);
        // WASD 키 입력에 따라 이동 (Z축 방향으로)
        if (h != 0 || v != 0)
        {
            // 로컬 좌표계 기준으로 이동 (Z축 방향으로)
            Vector3 moveDirection = new Vector3(h, 0, v);
            tr.Translate(moveDirection * Time.deltaTime * velocity, Space.Self);
        }
        // 마우스 이동값에 따라 회전 (Y축 회전)
        if (r != 0)
        {
            tr.Rotate(Vector3.up * r * Time.deltaTime * rotationSpeed);
        }
        PlayerAnimaiton(v,h);
    }

    private void PlayerAnimaiton(float v, float h)
    {
        if(v>0)anim.CrossFade("RunF",0.25f);
        
        if(v<0)anim.CrossFade("RunB",0.25f);
        
        if(h>0)anim.CrossFade("RunL",0.25f);
        
        if(h<0)anim.CrossFade("RunR",0.25f);
        if(h == 0 && v == 0)anim.CrossFade("Idle",0.25f);
    }
}