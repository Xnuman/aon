using UnityEngine;

public class Character2DMovementController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float jumpPower;

    private int _orientationMultiplier = 1;

    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (_rb == null)
        {
            return;
        }

        UpdateSpeed();
    }

    public void Init(Rigidbody2D rb)
    {
        _rb = rb;
        _orientationMultiplier = 0;
        _speed = 0.0f;
    }
    public void SetSpeed( float speed )
    {
        this._speed = speed;
        UpdateSpeed();
    }

    public float GetSpeed()
    {
        return this._speed;
    }

    public void SetOrientation( HorizontalOrientation orientation )
    {
        _orientationMultiplier = orientation switch
        {
            HorizontalOrientation.Left => -1,
            HorizontalOrientation.Right => 1,
            _ => 0,
        };
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        _rb.linearVelocityX = _speed * _orientationMultiplier;
    }

}
