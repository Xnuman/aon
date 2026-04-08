using UnityEngine;
using UnityEngine.InputSystem;

public class FightLevelCameraController : MonoBehaviour
{
    private Vector2 _moveInput;
    private Vector3 _velocity;

    private float _zoomValue = 0.0f;
    private float _standardCameraSize = 6.75f;

    [SerializeField] private float _speed = 5.0f;

    public void OnMoving(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnZooming(InputAction.CallbackContext context)
    {
        _zoomValue = context.ReadValue<float>();
    }

    private void Update()
    {
        Vector3 targetMove = (Vector3)_moveInput * _speed;
        _velocity = Vector3.Lerp(_velocity, targetMove, 10f * Time.deltaTime);
        transform.position += _velocity * Time.deltaTime;

        var aboba = GetComponent<Camera>();

        aboba.orthographicSize = Mathf.Clamp(aboba.orthographicSize + _zoomValue, _standardCameraSize / 2.0f, _standardCameraSize);
    }
}
