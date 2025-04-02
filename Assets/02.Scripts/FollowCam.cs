using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // [Range(12.0f, 42.0f)]
    public float distance = 5.0f;

    // [Range(9.0f, 39.0f)]
    public float height = 3.0f;

    [SerializeField] private float scrollSpeed = 10f;
    public float smoothTime = 0.01f;

    private Transform _camTr;
    private Transform _playerTr;

    public GameObject player;

    private Vector3 currentVelocity = Vector3.zero;

    void Start()
    {
        _camTr = GetComponent<Transform>();
        _playerTr = player.transform;
    }

    void LateUpdate()
    {
        // 1. 목표 위치 계산 (플레이어의 뒷쪽 위)
        Vector3 desiredPosition = _playerTr.position + _playerTr.up * height - _playerTr.forward * distance;

        // 2. 현재 위치에서 목표 위치로 부드럽게 이동
        _camTr.position = Vector3.SmoothDamp(_camTr.position, desiredPosition, ref currentVelocity, smoothTime);

        // 3. 카메라는 플레이어를 바라봄
        Vector3 lookTarget = _playerTr.position + Vector3.up * 5f;
        _camTr.LookAt(lookTarget);

        // 4. 마우스 스크롤로 거리, 높이 조절
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            distance += -scrollInput * scrollSpeed;
            height += -scrollInput * scrollSpeed;
            distance = Mathf.Clamp(distance, 12.0f, 42.0f);
            height = Mathf.Clamp(height, 9.0f, 39.0f);
        }
    }
}