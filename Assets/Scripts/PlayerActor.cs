using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActor : BattleActor
{
    int currentActionNum = 0;
    PlayerUI playerUI;
    public bool inBattle;


    private void Awake()
    {
        playerUI = GetComponentInChildren<PlayerUI>();

    }
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        ClearCommands();
        initStats();
        playerUI.setMaxHP(maxHP);
        playerUI.setMaxMP(maxMP);
    }

    private void Update()
    {
        if (inBattle)
        {
            BattleActorUpdate();
        }
        
    }

    public void OnActionSelected(int actionIndex)
    {
        AddCommand(actions[actionIndex]);
        playerUI.ToggleButtons();
    }

    public void OnTargetSelected(BattleActor target)
    {
        commandsEntered[commandsEntered.Count-1].target = target;

        if(currentActionNum > actionLimit - 1)
        {
            playerUI.ToggleButtons();
        }
    }

    

 

    public void TogglePlayerUI()
    {
        if(playerUI.gameObject.activeSelf)
        {
            playerUI.gameObject.SetActive(false);
        }
        else
        {
            playerUI.gameObject.SetActive(true);
        }
    }

    public override void Death()
    {
        Debug.Log("player died!!!!");
    }
}
