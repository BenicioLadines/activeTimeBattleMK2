using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class GameManager : MonoBehaviour
{

    public enum GameState { inBattle, walkin }
    GameState _currentGameState;
    public GameState currentGameState
    {
        get => _currentGameState;
        set => ChangeGameState(value);
    }
    public List<EnemyControl> enemies;
    public List<PlayerCharControl> playerChars;
    public List<BattleAction> actionQueue;
    [SerializeField]bool actionPlaying = false;
    PlayerCharControl pickingTarget;

    // Start is called before the first frame update
    void Start()
    {
        enemies.AddRange(FindObjectsOfType<EnemyControl>());
        playerChars.AddRange(FindObjectsOfType<PlayerCharControl>());
        if (enemies.Count == 0)
        {
            currentGameState = GameState.walkin;
        }
        else
        {
            currentGameState = GameState.inBattle;
        }

        actionPlaying = false;

    }

    // Update is called once per frame
    void Update()
    {
        switch(currentGameState)
        {
            case GameState.inBattle:
                InBattle();
                break;
            default: break;
        }
    }

    void InBattle()
    {
        if (enemies.Count == 0)
        {
            Debug.Log("no more enemies!");
            currentGameState = GameState.walkin;
        }

        if (actionQueue.Count > 0 && !actionPlaying)
        {
            NextAction();
        }
    }

    void NextAction()
    {
        BattleAction nextAction = actionQueue[0];
        if (nextAction.sender != null)
        {
            if (nextAction.sender.TryGetComponent<PlayerCharControl>(out PlayerCharControl player))
            {
                player.jumpTo.transform.position = (Vector2)nextAction.target.transform.position + (Vector2.right * 2);
            }
            nextAction.sender.animator.Play(nextAction.animationName);
            actionPlaying = true;
        }
        else
        {
            Debug.Log("action has no sender!");
        }
    }

    public void OnActionSelected(PlayerCharControl character)
    {
        pickingTarget = character;
        foreach (PlayerCharControl playerChar in playerChars)
        {
            playerChar.playerUI.EnableActions(false);
            playerChar.battleActor.toggleReticle(true);
        }

        foreach (EnemyControl enemy in enemies)
        {
            enemy.battleActor.toggleReticle(true);
        }
    }

    public void OnTargetSelected(GameObject target)
    {
        pickingTarget.OnTargetSelected(target);
        pickingTarget = null;
        foreach (PlayerCharControl playerChar in playerChars)
        {
            playerChar.battleActor.toggleReticle(false);
            if (playerChar.canAct)
            {
                playerChar.playerUI.EnableActions(true);
            }
            
        }

        foreach (EnemyControl enemy in enemies)
        {
            enemy.battleActor.toggleReticle(false);
        }
    }

    public void OnActionApply()
    {
        if(actionQueue.Count > 0)
        {
            actionQueue[0].Command();
        }

        if (actionQueue[0] is EnemyDeath)
        {
            enemies.Remove(actionQueue[0].sender.GetComponent<EnemyControl>());
        }
    }

    public void OnActionEnd()
    {
        if (actionQueue.Count > 0)
        {
            if (actionQueue[0].lastCommand)
            {
                if(actionQueue[0].sender.TryGetComponent<PlayerCharControl>(out PlayerCharControl player))
                {
                    player.currentState = PlayerCharControl.playerState.waitingForOrders;
                }
            }
            actionQueue.RemoveAt(0);
            actionPlaying = false;
        }
    }

    void ChangeGameState(GameState newState) 
    {
        ExitState(_currentGameState);
        EnterState(newState);
        _currentGameState = newState;
    }

    void EnterState(GameState state)
    {
        switch(state)
        {
            case GameState.walkin:

                foreach(PlayerCharControl playerChar in playerChars)
                {
                    playerChar.battleActor.animator.Play("walk");
                }
                break;
            default: break;
        }
    }

    void ExitState(GameState state)
    {

    }
}
