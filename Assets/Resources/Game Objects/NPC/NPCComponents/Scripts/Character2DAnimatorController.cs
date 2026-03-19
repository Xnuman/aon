using UnityEngine;
using UnityEngine.U2D.IK;

public class Character2DAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private HorizontalOrientation _defaultAnimationOrientation;
    private HorizontalOrientation _animationOrientation;
    bool _isFlipped;

    bool _shouldRun;
    bool _shouldFight;
    public void Init()
    {
        if(_animator == null)
        {
            Debug.Log("Character2DAnimationController: _animator was null");
        }

        _animationOrientation = _defaultAnimationOrientation;
        _isFlipped = false;
    }

    public void ManualUpdate()
    {

    }

    private void Update()
    {
        if (_animator == null)
            return;

        if (_animationOrientation != _defaultAnimationOrientation && !_isFlipped)
        {
            _animator.transform.Rotate(0, 180, 0);
            _isFlipped = true;
        }
    }
    public void SetShouldRun( bool shouldRun )
    {

        //if (shouldRun != _shouldRun)
        //{
        //    _animator.Rebind();
        //    _shouldRun = shouldRun;
        //}

        _animator.SetBool("ShouldRun", shouldRun);
        //_animator.Rebind();
    }

    public void SetShouldFight(bool shouldFight)
    {
        _animator.SetBool("ShouldFight", shouldFight);
        
        if(shouldFight != _shouldFight)
        {
            //_animator.Rebind();
            //_shouldFight = shouldFight;

            //var IKManager = gameObject.GetComponentInChildren<IKManager2D>();
            //if(IKManager != null)
            //{
            //    IKManager.
            //}
        }
    }

    public void SetAnimationOrientation(HorizontalOrientation animationOrientation)
    {
        _animationOrientation = animationOrientation;
    }
}