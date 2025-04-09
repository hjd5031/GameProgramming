using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class MonsterCtrl : MonoBehaviour
{

    private Transform _playerTr;
    private NavMeshAgent _agent;
    private Animator _anim;
    
    [SerializeField] private float traceDist;
    [SerializeField] private float attackDist;
    private int _hp;

    private float _distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _hp = 100;
        StartCoroutine(MonsterAnim());  // 한 번만 실행
    }

    void Update()
    {
        Debug.Log(_hp);
        _distance = Vector3.Distance(_playerTr.position, transform.position);

        if (_distance < traceDist && !_agent.isStopped && _hp != 0)
        {
            _agent.SetDestination(_playerTr.position);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet") && _anim.GetBool("isDead") == false)//Bullet과 충돌 시 Barrel 체력--
        {
            _agent.isStopped = true;
            _anim.SetTrigger("Hit");
            _hp-=10;
        }
    }
    IEnumerator MonsterAnim()
    {
        while (true)
        {
            _distance = Vector3.Distance(_playerTr.position, transform.position);

            if (_distance <= traceDist)
            {
                _agent.isStopped = false;
                _anim.SetBool("isTrace", true);
            }
            else
            {
                _agent.isStopped = true;
                _anim.SetBool("isTrace", false);
            }

            if (_distance <= attackDist)
            {
                _agent.isStopped = true;
                _anim.SetBool("isAttack", true);
            }
            else
            {
                _anim.SetBool("isAttack", false);
            }

            if (_hp <= 0)
            {
                _agent.isStopped = true;
                // _agent.baseOffset = -0.5f;
                _anim.SetBool("isDead", true);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void OnEnable()
    {
        PlayerCtrl.OnPlayerDie += this.OnPlayerDie;
    }

    void OnDisable()
    {
        PlayerCtrl.OnPlayerDie -= this.OnPlayerDie;
    }

    void OnPlayerDie()
    {
        StopAllCoroutines();
        _anim.SetTrigger("PlayerDie");
    }
}
