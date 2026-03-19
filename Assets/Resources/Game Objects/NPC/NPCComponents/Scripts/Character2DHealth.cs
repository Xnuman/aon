using UnityEngine;
using UnityEngine.UI;

public class Character2DHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Gradient healthBarGradient;

    public float GetHealth() => _currentHealth;
    public void SetHealth( float newHealth ) => _currentHealth = newHealth;

    public void Init()
    {
        _currentHealth = _maxHealth;
        healthBarSlider.maxValue = _maxHealth;
        healthBarSlider.value = _maxHealth;
    }

    public void ManualUpdate()
    {

    }
    void Update()
    {
        healthBarSlider.value = _currentHealth;
        healthBarFill.color = healthBarGradient.Evaluate(healthBarSlider.normalizedValue);
    }
}