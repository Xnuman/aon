using UnityEngine;

public class Character2DHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float maxHealth;
    private float currentHealth;

    public float GetHealth() => currentHealth;
    public void SetHealth( float newHealth ) => currentHealth = newHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if( currentHealth <= 0.0f )
        {
            gameObject.GetComponent<Character2DCombat>().StopAttacking();
            Destroy( gameObject );
        }
    }
}
