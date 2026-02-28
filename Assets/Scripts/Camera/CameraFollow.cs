using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothTime = 0.2f;
    [SerializeField] private Transform _target;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _ground = new Vector3(0, -1, 0);    

    void LateUpdate()
    {
        if (_target == null)
        {
            Debug.LogError("No target for the camera");
            return;
        }
        Vector3 targetPosition = _target.position + _offset + _ground;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}