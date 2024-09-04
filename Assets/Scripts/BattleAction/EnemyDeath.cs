using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyDeath : BattleAction
{
    GameManager gameManager;

    public EnemyDeath()
    {
        animationName = "deathAnim";
        gameManager = FindObjectOfType<GameManager>();
    }

    public override void Command()
    {
        gameManager.enemyActors.Remove((EnemyActor)sender);
        Destroy(sender.gameObject);
    }
}
