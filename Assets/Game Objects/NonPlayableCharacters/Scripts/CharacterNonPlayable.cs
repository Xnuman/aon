using UnityEngine;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{

    [SerializeField] private Character2DMovementController _movementController;
    [SerializeField] private Character2DAnimatorController _animatorController;
    [SerializeField] private Character2DCombat combat;
    [SerializeField] private Character2DHealth health;
    [SerializeField] private Rigidbody2D rb;

    public int myIndexInUnitsPositions;

    public enum CharacterState
    {
        Idle,
        Moving,
        Fighting
    };

    public void SetCharacterState(CharacterState state) => _characterState = state;
    private CharacterState _characterState;
    private HorizontalOrientation _characterMovementOrientation;

    public void InitCharacter()
    {
        if (CompareTag("Ally"))
        {
            _characterMovementOrientation = HorizontalOrientation.Right;
        }
        else if (CompareTag("Enemy"))
        {
            _characterMovementOrientation = HorizontalOrientation.Left;
        }

        _animatorController.Init();
        _movementController.Init(rb);

        _movementController.SetOrientation(_characterMovementOrientation);
        _animatorController.SetAnimationOrientation(_characterMovementOrientation);
        _characterState = CharacterState.Moving;
    }
    public void UpdateCharacter()
    {
        switch (_characterState)
        {
            case CharacterState.Idle:
                _animatorController.SetShouldRun(false);
                _animatorController.SetShouldFight(false);
                _movementController.SetSpeed(0.0f);
                break;
            case CharacterState.Moving:
                _animatorController.SetShouldRun(true);
                _animatorController.SetShouldFight(false);
                _movementController.SetSpeed(1.0f);
                break;
            case CharacterState.Fighting:
                _animatorController.SetShouldRun(false);
                _animatorController.SetShouldFight(true);
                _movementController.SetSpeed(0.0f);
                break;
        };
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