using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MagicConversion : BattleAction
{
    public override void Command()
    {
        target.currentMP += sender.magicPower;
    }
}
