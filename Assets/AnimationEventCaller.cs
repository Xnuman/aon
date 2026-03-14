using UnityEngine;

public class AnimationEventCaller : MonoBehaviour
{
    public void DoAttack()
    {
        Character2DCombat combatComponent = GetComponentInParent<Character2DCombat>();

        if (combatComponent == null)
        {
            return;
        }
        combatComponent.Attack();
    }
}
