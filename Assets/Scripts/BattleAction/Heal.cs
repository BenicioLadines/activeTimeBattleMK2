using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Heal : BattleAction
{
    [SerializeField] int healed;
    
    public override void Command()
    {
        target.currentHP += healed;
        SpendPointCost();
    }
}
