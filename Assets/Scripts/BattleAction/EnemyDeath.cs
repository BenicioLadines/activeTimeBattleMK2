using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyDeath : BattleAction
{

    public EnemyDeath()
    {
        animationName = "deathAnim";
    }

    public override void Command()
    {
        Debug.Log("deid");
        Destroy(sender.gameObject);
    }
}
