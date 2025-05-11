using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class MonsterCtrl : MonoBehaviour
{

    [Header("References")]
    private Transform _playerTr;
    private NavMeshAgent _agent;
    private Animator _anim;
    private Collider _collider;
    
    [Header("Attributes")]
    [SerializeField] private float traceDist;
    [SerializeField] private float attackDist;
    private int _hp;
    private bool _isVictory;
    private float _distance;
    
    void Awake()
    {
        _playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        _hp = 100;
        _isVictory = false;
    }

    void Update()
    {
        // Debug.Log(_hp);
        _distance = Vector3.Distance(_playerTr.position, transform.position);
        
        if (_distance < traceDist && !_agent.isStopped && _hp >= 0)
        {
            _agent.SetDestination(_playerTr.position);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet") && _anim.GetBool("isDead") == false)//Bullet과 충돌 시 Monster 체력--
        {
            _agent.isStopped = true;
            _anim.SetTrigger("Hit");
            _hp-=10;
        }
        if (_hp <= 0&& _anim.GetBool("isDead") == false)
        {
            GameManager.Instance.DisplayScore(50);//Monster 사망시 _totScore 50++
            MonsterDie();
        }
    }

    void MonsterDie()
    {
        StopAllCoroutines();
        _agent.isStopped = true;
        // _agent.baseOffset = -0.5f;
        _anim.SetBool("isTrace", false);
        _anim.SetBool("isAttack", false);
        _anim.SetBool("isDead", true);
        _anim.SetTrigger("Die");
        _agent.isStopped = true;
        _collider.enabled = false;
        StartCoroutine(MonsterDestroy());
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

            
            yield return new WaitForSeconds(0.3f);
        }

        yield break;
    }

    IEnumerator MonsterDestroy()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        _hp = 100;
        _collider.enabled = true;
        _isVictory = false;
        _anim.Rebind();
        _anim.Update(0f);
        StartCoroutine(MonsterAnim());  // 한 번만 실행
        if (_agent != null) _agent.isStopped = false;
        if (_anim != null)
        {
            _anim.SetBool("isDead", false);
            _anim.SetBool("isAttack", false);
            _anim.SetBool("isTrace", false);
        }

        PlayerCtrl.OnPlayerDie += this.OnPlayerDie;
    }

    void OnDisable()
    {
        PlayerCtrl.OnPlayerDie -= this.OnPlayerDie;
    }

    void OnPlayerDie()
    {
        
        if (_isVictory) return;//플레이어 사망 최초 1회 실행
        _isVictory = true;
        float victorySpeed = Random.Range(0.8f, 1.5f);
        _anim.SetFloat("Speed",victorySpeed);
        _anim.SetTrigger("PlayerDie");
        StopAllCoroutines();
        _agent.isStopped = true;
        
        //플레이어 사망 시 몬스터가 플레이어의 위치를 바라보게 설정
        Vector3 lookPos = _playerTr.position - transform.position;
        lookPos.y = 0; // 수직 방향은 고정
        if (lookPos != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(lookPos);

    }
}
