using UnityEngine;

public class Character2DAnimatorController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private HorizontalOrientation _defaultAnimationOrientation;
    private HorizontalOrientation _animationOrientation;
    bool _isFlipped;
    private void Start()
    {

    }

    public void Init()
    {
        _animator = GetComponent<Animator>();
        _animationOrientation = _defaultAnimationOrientation;
        _isFlipped = false;
    }

    private void Update()
    {
        if (_animator == null)
            return;

        if(_animationOrientation != _defaultAnimationOrientation && !_isFlipped)
        {
            _animator.transform.Rotate(0, 180, 0);
            _isFlipped = true;
        }
    }
    public void SetShouldRun( bool shouldRun )
    {
        _animator.SetBool("ShouldRun", shouldRun);
    }

    public void SetAnimationOrientation(HorizontalOrientation animationOrientation)
    {
        _animationOrientation = animationOrientation;
    }
}
