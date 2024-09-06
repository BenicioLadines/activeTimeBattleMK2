using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyActor : BattleActor
{

    protected virtual void OnDeath()
    {
        gameManager.enemyActors.Remove(this);

    }

}
