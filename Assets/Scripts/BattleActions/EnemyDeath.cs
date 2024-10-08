using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : BattleAction
{
    EnemyDeath()
    {
        animationName = "deathAnim";
        SPCost = 0;
    }
    public override void Command()
    {
        Destroy(sender.gameObject);
    }
}
