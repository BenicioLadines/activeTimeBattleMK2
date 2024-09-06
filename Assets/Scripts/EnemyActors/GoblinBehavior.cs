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

            AddCommand(actions[0]);
            commandsEntered[0].target = gameManager.playerActors[0];
            SubmitCommands();

        }

    }

    public override void Death()
    {
        EnemyDeath deathAction = (EnemyDeath) ScriptableObject.CreateInstance("EnemyDeath");
        deathAction.sender = this;
        gameManager.actionQueue.Insert(1, deathAction);
    }

    private void OnDestroy()
    {
        OnDeath();
    }


}
