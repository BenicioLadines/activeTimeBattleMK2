using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackAction : BattleAction
{
    
    public override void Command()
    {
        target.currentHP -= sender.attackPower;
    }
}
