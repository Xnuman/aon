using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NPC : MonoBehaviour
{

    [SerializeField] private Character2DMovementController _movementController;
    [SerializeField] private Character2DAnimatorController _animatorController;
    [SerializeField] private Character2DCombat combat;
    [SerializeField] private Character2DHealth health;
    [SerializeField] private Rigidbody2D rb;

    private enum CharacterState
    {
        Idle,
        Moving,
        Fighting
    };

    private CharacterState _characterState;
    private HorizontalOrientation _characterMovementOrientation;

    void Start()
    {
        Debug.Log("Start()");

        if(CompareTag("Ally"))
        {
            _characterMovementOrientation = HorizontalOrientation.Right;
        }
        else if(CompareTag("Enemy"))
        {
            _characterMovementOrientation = HorizontalOrientation.Left;
        }

        //_movementController = gameObject.AddComponent<Character2DMovementController>();
        //_animatorController = gameObject.AddComponent<Character2DAnimatorController>();

        _animatorController.Init();
        _movementController.Init(rb);

        _movementController.SetOrientation(_characterMovementOrientation);
        _animatorController.SetAnimationOrientation(_characterMovementOrientation);
        _characterState = CharacterState.Moving;
    }
    void FixedUpdate()
    {
    }

    private void Update()
    {
        switch (_characterState)
        {
            case CharacterState.Idle:
                _animatorController.SetShouldRun(false);
                _movementController.SetSpeed(0.0f);
                break;
            case CharacterState.Moving:
                _animatorController.SetShouldRun(true);
                _movementController.SetSpeed(1.0f);
                break;
            case CharacterState.Fighting:
                _animatorController.SetShouldRun(false);
                _movementController.SetSpeed(0.0f);
                break;
        };
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("3D Version is triggered");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool foundAnotherNPC = collision.gameObject.TryGetComponent<NPC>(out var otherNPC);

        if (!foundAnotherNPC)
        {
            return;
        }

        if ( otherNPC._characterMovementOrientation == _characterMovementOrientation || otherNPC._characterMovementOrientation == HorizontalOrientation.None )
        {
            float otherNPCPositionX = otherNPC.transform.position.x;
            float myPositionX = gameObject.transform.position.x;

            if( ( _characterMovementOrientation == HorizontalOrientation.Right && otherNPCPositionX > myPositionX ) ||
                ( _characterMovementOrientation == HorizontalOrientation.Left && otherNPCPositionX < myPositionX ) )
            {
                _characterState = CharacterState.Idle;
            }
        }
        else if( IsNPCEnemy( otherNPC ) )
        {
            combat.StartAttacking(collision.gameObject.GetComponent<Character2DHealth>());
            _characterState = CharacterState.Fighting;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _characterState = CharacterState.Moving;

        bool foundAnotherNPC = collision.gameObject.TryGetComponent<NPC>(out var otherNPC);

        if (!foundAnotherNPC)
        {
            return;
        }

        if(combat.IsAttacking())
        {
            combat.StopAttacking();
        }
        //movementController.Stop();
    }

    private bool IsNPCEnemy(NPC otherNPC)
    {
        if(CompareTag("Ally"))
        {
            return otherNPC.gameObject.CompareTag("Enemy");
        }
        else if (CompareTag("Enemy"))
        {
            return otherNPC.gameObject.CompareTag("Ally");
        }

        return false;
    }
}
