using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 1000f; // per sec in degre

    private CharacterController _cc;
    private Vector2 _input;
    private Vector3 _direction;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _input = ctx.ReadValue<Vector2>();
    }

    void Update()
    {
        _direction = new Vector3(_input.x, 0, _input.y);

        if (_direction.sqrMagnitude > 0.01f)
        {
            // rotation
            Quaternion targetRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // movements
            _cc.Move(_direction * speed * Time.deltaTime);
        }
    }
}