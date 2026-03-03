using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Character2D : MonoBehaviour
{
    [SerializeField] private Character2DSpriteController spriteController;
    [SerializeField] private Character2DMovementController movementController;
    [SerializeField] private Animator animator;

    void Start()
    {
        //movementController.FullStop();
    }

    void Update()
    {
        //float horizontalAxisInput = Input.GetAxisRaw("Horizontal");
        //float verticalAxisInput = Input.GetAxisRaw("Vertical");
        //HorizontalOrientation lastOrientation = HorizontalOrientation.None;

        //if ( horizontalAxisInput < 0.0f )
        //{
        //    movementController.MoveLeft();
        //}
        //else if ( horizontalAxisInput > 0.0f )
        //{
        //    movementController.MoveRight();
        //}
        //else
        //{
        //    movementController.Stop();
        //}

        //if ( verticalAxisInput > 0.0f && movementController.IsJumping == false )
        //{
        //    movementController.Jump();
        //}
        //else
        //{
        //    movementController.StopJump();
        //}

        //if( spriteController != null )
        //{
        //    spriteController.SetSpriteOrientation(movementController.GetMovementOrientation());
        //}

        //if( animator != null )
        //{
        //    if( movementController.GetMovementOrientation() == HorizontalOrientation.None || movementController.GetHorizontalSpeed() == 0.0f)
        //    {
        //        animator.Play("Idle");
        //        lastOrientation = HorizontalOrientation.None;
        //    }
        //    else
        //    {
        //        animator.Play("Run");

        //        bool shouldBeFlipped = movementController.GetMovementOrientation() == HorizontalOrientation.Left;

        //        foreach (Transform childTransform in transform)
        //        {
        //            GameObject childGameObject = childTransform.gameObject;

        //            SpriteRenderer childSprite;

        //            if (childGameObject.TryGetComponent<SpriteRenderer>(out childSprite))
        //            {
        //                childSprite.flipX = shouldBeFlipped;
        //            }
        //        }
        //    }
        //}

    }
}
