using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BattleActor))]
public class EnemyControl : MonoBehaviour
{
    public BattleActor battleActor;
    [SerializeField] protected float staminaRegainRate;
    [SerializeField] protected TextMeshProUGUI debugText;

    protected void EnemyStart()
    {
        battleActor = GetComponent<BattleActor>();
    }

    protected void EnemyUpdate()
    {
        if (battleActor.regainingStamina)
        {
            battleActor.regainStamina(Time.deltaTime * staminaRegainRate);
        }

        if (debugText != null)
        {
            debugText.text = string.Format(
                "HP: {0}\nMP: {1}\nSP: {2}\nStam: {3}",
                battleActor.currentHP, battleActor.currentMP,
                battleActor.currentSP, battleActor.currentStaminaPercent);
        }
    }

    public void OnDeath()
    {
        battleActor.ClearCommands();
        BattleAction deathAction = ScriptableObject.CreateInstance<EnemyDeath>();
        deathAction.sender = battleActor;
        battleActor.AddCommand(deathAction);
        battleActor.SubmitWithPriority();
        
    }
}
