using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBehavior : EnemyActor
{

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        initStats();
    }

    private void Update()
    {
        BattleActorUpdate();

        if(currentSP > 2)
        {
            BattleAction newAction = Instantiate(actions[0]);
            newAction.target = gameManager.playerActors[0];
            newAction.sender = this;
            gameManager.actionQueue.Add(newAction);
            currentSP -= 1;
        }

    }

    public override void Death()
    {
        EnemyDeath deathAction = new EnemyDeath();
        deathAction.sender = this;
        gameManager.actionQueue.Insert(1, deathAction);
    }


}
