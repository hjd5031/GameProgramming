using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class MonsterCtrl : MonoBehaviour
{

    private Transform _playerTr;
    private NavMeshAgent _agent;
    private Animator _anim;
    [SerializeField] private float traceDist = 10.0f;
    [SerializeField] private float attackDist = 2.0f;

    private float _distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_playerTr.position);
        StartCoroutine(MonsterAnim());
    }

    IEnumerator MonsterAnim()
    {
        _distance = Vector3.Distance(_playerTr.position, _agent.destination);
        yield return new WaitForSeconds(0.3f);
        if (_agent.velocity == Vector3.zero)
        {
            _agent.isStopped = true;
            _anim.SetBool("isTrace",false);
        }

        else if (_distance < traceDist)
        {
            _anim.SetBool("isTrace",true);
        }
        else if (_distance < attackDist)
        {
            _anim.SetBool("isAttack",true);
        }
        else if (_distance > attackDist)
        {
            _anim.SetBool("isAttack",false);
        }
    }
}
