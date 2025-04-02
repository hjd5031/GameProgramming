using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    private Transform _camTr;
    private Transform _playerTr;
    
    [Header("Attributes")]
    [SerializeField]private float distance;
    [SerializeField]private float height;
    [SerializeField] private float scrollSpeed;
    [SerializeField]private float smoothTime;

    private Vector3 _currentVelocity = Vector3.zero;

    void Start()
    {
        _camTr = GetComponent<Transform>();
        _playerTr = player.transform;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = _playerTr.position + _playerTr.up * height - _playerTr.forward * distance;//SmoothDamp 적용한 카메라의 목적지
        _camTr.position = Vector3.SmoothDamp(_camTr.position, desiredPosition, ref _currentVelocity, smoothTime);

        
        Vector3 lookTarget = _playerTr.position + Vector3.up * 5f;//Offset -> Vector3.up * 5f
        _camTr.LookAt(lookTarget);

        //마우스 휠 이벤트 발생 시 카메라 확대/축소
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            distance += -scrollInput * scrollSpeed;
            height += -scrollInput * scrollSpeed;
            distance = Mathf.Clamp(distance, 13.0f, 43.0f);
            height = Mathf.Clamp(height, 8.0f, 38.0f);
        }
    }
}