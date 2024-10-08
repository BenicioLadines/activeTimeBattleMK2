using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealAction : BattleAction
{
    public override void Command()
    {
        target.currentHP += sender.magicPower;
    }
}
