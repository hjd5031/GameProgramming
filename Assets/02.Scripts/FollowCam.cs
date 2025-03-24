using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Range(2.0f, 100.0f)]
    public float distance = 20.0f;
    [Range(2.0f, 20.0f)]
    public float height = 20.0f;

    private Transform _camTr;
    
    public GameObject player;
    private Transform _playerTr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camTr = GetComponent<Transform>();
        _playerTr = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _camTr.position = _playerTr.position + new Vector3(0, height, -distance);
    }
}
