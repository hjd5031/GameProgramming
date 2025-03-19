using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //References
    private Transform _tr;
    private Animation _anim;
    
    [Header("Attributes")]
    public float velocity;
    public float rotationSpeed;
    
    
    void Start()
    {
        _tr = GetComponent<Transform>();
        _anim = GetComponent<Animation>();
        _anim.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        // WASD 키 입력값 가져오기
        float h = Input.GetAxis("Horizontal"); // A, D 키 (좌우)
        float v = Input.GetAxis("Vertical");   // W, S 키 (상하)
        
        // 마우스 X 축 이동값 가져오기 (좌우)
        float r = Input.GetAxis("Mouse X");
        // WASD 키 입력에 따라 이동 (Z축 방향으로)
        if (h != 0 || v != 0)
        {
            Debug.Log("h = "+h+"v = "+v);
            // 로컬 좌표계 기준으로 이동 (Z축 방향으로)
            Vector3 moveDirection = new Vector3(h, 0, v);
            _tr.Translate(moveDirection * Time.deltaTime * velocity, Space.Self);
        }
        // 마우스 이동값에 따라 회전 (Y축 회전)
        if (r != 0)
        {
            Debug.Log("r = " + r);
            _tr.Rotate(Vector3.up * r * Time.deltaTime * rotationSpeed);
        }
        StartPlayerAnim(v,h);
    }

    private void StartPlayerAnim(float v, float h)
    {
        if(v>0)_anim.CrossFade("RunF",0.25f);
        
        if(v<0)_anim.CrossFade("RunB",0.25f);
        
        if(h>0)_anim.CrossFade("RunL",0.25f);
        
        if(h<0)_anim.CrossFade("RunR",0.25f);
        if(h == 0 && v == 0)_anim.CrossFade("Idle",0.25f);
    }
}