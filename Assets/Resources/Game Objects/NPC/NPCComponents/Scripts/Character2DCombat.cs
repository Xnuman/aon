using System.Collections;
using UnityEngine;

public class Character2DCombat : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private GameObject _attackPoint;
    [SerializeField] private float _radius;

    [SerializeField] private int _attackRange; // TODO: Move to CharacterConfig
    [SerializeField] AudioSource _attackSound; // TODO: Think of better approach
    public int GetAttackRange => _attackRange;

    //[SerializeField] private LayerMask enemyLayerMask;

    public void Attack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(_attackPoint.transform.position.x, _attackPoint.transform.position.y), _radius);

        foreach(Collider2D collider in hitColliders)
        {
            Character2DHealth enemyHealthComponent = null;

            if (!collider.gameObject.TryGetComponent<Character2DHealth>(out enemyHealthComponent))
                continue;

            if (CompareTag(enemyHealthComponent.gameObject.tag))
                continue;

            if (_attackSound != null)
                _attackSound.Play();

            Attack(enemyHealthComponent);
        }
    }

    public void Attack(Character2DHealth enemyHealth)
    {
        enemyHealth.SetHealth(Mathf.Max(enemyHealth.GetHealth() - _damage, 0.0f));
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_attackPoint.transform.position, _radius);
    }
}