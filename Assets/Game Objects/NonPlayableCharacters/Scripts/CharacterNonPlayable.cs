using UnityEngine;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{

    [SerializeField] private Character2DMovementController _movementController;
    [SerializeField] private Character2DAnimatorController _animatorController;
    [SerializeField] private Character2DCombat _combat;
    [SerializeField] private Character2DHealth _health;
    [SerializeField] private Rigidbody2D rb;

    public int myIndexInUnitsPositions;

    public Character2DCombat GetCombatComponent => _combat;

    public bool IsDead()
    {
        return _health.GetHealth() <= 0.0f;
    }

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
        _health.Init();

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
}