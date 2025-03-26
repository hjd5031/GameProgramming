using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Range(12.0f, 42.0f)]
    public float distance = 5.0f;

    [Range(9.0f, 39.0f)]
    public float height = 3.0f;

    [SerializeField] private float scrollSpeed = 10f;
    public float rotationSpeed = 5f;

    private Transform _camTr;
    private Transform _playerTr;

    public GameObject player;

    void Start()
    {
        _camTr = GetComponent<Transform>();
        _playerTr = player.transform;
    }

    void LateUpdate()
    {
        Vector3 offset = _playerTr.up*height - _playerTr.forward*distance;
        _camTr.position = _playerTr.position + offset;

        // 플레이어의 중심을 향하도록 카메라 회전
        Vector3 lookTarget = _playerTr.position + Vector3.up*5f;
        _camTr.LookAt(lookTarget);
        // 4. 마우스 스크롤로 확대/축소
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            distance += -scrollInput * scrollSpeed;
            height += -scrollInput * scrollSpeed;
            distance = Mathf.Clamp(distance, 12.0f, 42.0f); // 거리 제한
            height = Mathf.Clamp(height, 9.0f, 39.0f); // 거리 제한
        }
    }
}