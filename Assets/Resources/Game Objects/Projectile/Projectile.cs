using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _startSpeed;

    private float distance = 5;
    private float t;
    private Vector2 startPosition;

    private float time = 5;

    void Start()
    {
        //_rb.linearVelocity = new Vector2(1.0f, 1.0f) * _startSpeed;
        startPosition = transform.position;
    }

    void Update()
    {
        t = (time * t + Time.deltaTime) / time;

        float height = Mathf.Abs(parabola(distance / 2.0f));

        float currentXPosition = Mathf.Lerp((-distance / 2.0f), (distance / 2.0f), t);
        float currentYPosition = parabola(currentXPosition) + height;

        transform.position = startPosition + new Vector2( currentXPosition, currentYPosition );
    }

    float parabola(float x)
    {
        return -Mathf.Pow(x, 2.0f);
    }
}
