using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCtrl : MonoBehaviour
{
    [Header("References")]
    private Transform _tr;
    private Animation _anim;
     
    [Header("Attributes")]
    [SerializeField]private float velocity;
    [SerializeField]private float rotationSpeed;
    public float currHp;
    private readonly float InitHp = 100f;

    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;
    void Start()
    {
        _tr = GetComponent<Transform>();
        _anim = GetComponent<Animation>();
        _anim.Play("Idle");
        currHp = InitHp;
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

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ishit");
        if (other.CompareTag("Punch"))
        {
            currHp -= 10f;
            Debug.Log(currHp);
        }

        if (currHp <= 0)
        {
            Debug.Log(currHp);

            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            monster.SendMessage("OnPlayerDie",SendMessageOptions.DontRequireReceiver);
        }
        // OnPlayerDie();

    }
    private void StartPlayerAnim(float v, float h, float r)
    {
        if (v > 0) _anim.CrossFade("RunF", 0.25f);
        else if (v < 0) _anim.CrossFade("RunB", 0.25f);
        else if (h > 0||r<0) _anim.CrossFade("RunL", 0.25f);
        else if (h < 0||r>0) _anim.CrossFade("RunR", 0.25f);
        else _anim.CrossFade("Idle", 0.25f);
    }
    void OnGUI()
    {
        GUI.Box(new Rect(10,10,100,25),"HP: "+currHp);
        float size = 200f;
        float posX = (Screen.width - size) / 2+100;
        float posy = (Screen.height - size) / 2-150;
        GUI.Label(new Rect(posX,posy,size,size),"+");
    }
    // void PlayerDie()
    // {
    //     OnPlayerDie();
    // }
}