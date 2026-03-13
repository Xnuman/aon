using System.Collections;
using UnityEngine;

public class Character2DCombat : MonoBehaviour
{

    [SerializeField] private float damage;

    private IEnumerator attackCoroutine;

    private bool isAttacking = false;

    public bool IsAttacking() => isAttacking;
    void Start()
    {
        isAttacking = false;
    }

    public void StartAttacking(Character2DHealth enemyHealth)
    {
        attackCoroutine = Attack(enemyHealth);
        isAttacking = true;
        StartCoroutine(attackCoroutine);
    }

    public void StopAttacking()
    {
        if(attackCoroutine != null)
        {
            isAttacking = false;
            StopCoroutine(attackCoroutine);
        }
    }
    IEnumerator Attack(Character2DHealth enemyHealth)
    {
        while(enemyHealth != null && enemyHealth.GetHealth() > 0.0f)
        {
            enemyHealth.SetHealth(enemyHealth.GetHealth() - damage);
            yield return new WaitForSeconds(1);
        }

        enemyHealth.gameObject.GetComponent<Character2DCombat>().StopAttacking();
    }
    
}