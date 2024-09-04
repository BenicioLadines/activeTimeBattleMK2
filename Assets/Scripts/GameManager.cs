using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<PlayerActor> playerActors = new List<PlayerActor>();
    public List<EnemyActor> enemyActors = new List<EnemyActor>();
    public List<BattleAction> actionQueue = new List<BattleAction>();
    bool actionPlaying = false;
    public enum gameState { inBattle, walking }
    [SerializeField]gameState _currentGameState;
    public gameState currentGameState
    {
        get { return _currentGameState; }
        set
        {
            switch (_currentGameState)
            {
                case gameState.inBattle:
                    actionQueue.Clear();
                    foreach (PlayerActor actor in playerActors)
                    {
                        actor.inBattle = false;
                        actor.TogglePlayerUI();
                    }
                    break;
                case gameState.walking:
                    break;
                default:
                    break;
            }

            switch (value)
            {
                case gameState.inBattle:
                    foreach(PlayerActor actor in playerActors)
                    {
                        
                        actor.inBattle = true;
                        actor.TogglePlayerUI();
                        actor.initStats();
                    }
                    foreach(EnemyActor actor in enemyActors)
                    {
                        actor.initStats();
                    }
                    break;
                case gameState.walking:
                    foreach(PlayerActor actor in playerActors)
                    {
                        actor.animator.Play("walkAnim");
                        
                    }
                    break;
                default:
                    break;
            }
            _currentGameState = value;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerActors.AddRange(FindObjectsOfType<PlayerActor>());
        enemyActors.AddRange(FindObjectsOfType<EnemyActor>());
        if(enemyActors.Count > 0)
        {
            currentGameState = gameState.inBattle;
        }
        else
        {
            currentGameState = gameState.walking;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        switch(currentGameState)
        {
            case gameState.inBattle:
                if(enemyActors.Count == 0)
                {
                    Debug.Log("no more enemies!");
                    currentGameState = gameState.walking;
                }

                if (actionQueue.Count > 0 && !actionPlaying)
                {
                    NextAction();
                }
                break;
            default: break;
        }
    }

    public void OnActionSelected()
    {
        foreach(PlayerActor playerChar in playerActors)
        {
            playerChar.GetComponentInChildren<PlayerUI>().ToggleButtons();
            playerChar.toggleReticle(true);
        }

        foreach(EnemyActor enemy in enemyActors)
        {
            enemy.toggleReticle(true );
        }
    }

    public void OnTargetSelected()
    {
        foreach (PlayerActor playerActor in playerActors)
        {
            playerActor.GetComponentInChildren<PlayerUI>().ToggleButtons();
            playerActor.toggleReticle(false);
        }

        foreach (EnemyActor enemy in enemyActors)
        {
            enemy.toggleReticle(false);
        }
    }

    void NextAction()
    {
        if (actionQueue[0].sender != null)
        {
            actionQueue[0].sender.animator.Play(actionQueue[0].animationName);
            actionPlaying = true;
        }
        else
        {
            Debug.Log("action has no sender!");
        }
        
    }

    public void OnActionEnd()
    {
        if(actionQueue.Count > 0)
        {
            actionQueue[0].Command();
            actionQueue.RemoveAt(0);
            actionPlaying = false;
        }

    }

}
