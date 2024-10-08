using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleActor))]
public class PlayerCharControl : MonoBehaviour
{
    public BattleActor battleActor;
    [SerializeField]float staminaRegainRate;
    public PlayerUI playerUI;
    public enum playerState { waitingForOrders, waitingToAct, dead }
    playerState _currentState;
    public playerState currentState
    {
        get => _currentState;
        set => ChangeState(value);
    }
    [SerializeField]GameObject sprite;
    [SerializeField]public GameObject jumpTo;
    [HideInInspector]public float lerpPos;
    Vector2 startingPos;
    int _currentAction;
    public int currentAction
    {
        get => _currentAction;
        set => CurrentActionChanged(value);
    }
    [SerializeField]int maxActionsToTake;
    public bool canAct;
    public IntEvent currentActionChanged;

    // Start is called before the first frame update
    void Start()
    {
        _currentAction = 0;
        canAct = true;
        startingPos = sprite.transform.localPosition;
        battleActor = GetComponent<BattleActor>();
        playerUI.SetMaxHP(battleActor.maxHP);
        battleActor.currentHP = battleActor.maxHP;
        playerUI.SetMaxMP(battleActor.maxMP);
        currentState = playerState.waitingForOrders;

    }

    // Update is called once per frame
    void Update()
    {
        if (battleActor.regainingStamina)
        {
            battleActor.regainStamina(Time.deltaTime * staminaRegainRate);
        }

        sprite.transform.localPosition = Vector2.Lerp(startingPos, jumpTo.transform.localPosition, lerpPos);

    }

    public void OnActionSelected(int actionIndex)
    {
        battleActor.AddCommand(battleActor.actions[actionIndex]);
        playerUI.EnableConfirm(false);
        playerUI.EnableActions(false);
        playerUI.EnableCancel(false);
    }

    public void OnTargetSelected(GameObject target)
    {
        if(target.TryGetComponent<BattleActor>(out BattleActor actor))
        {
            battleActor.enteredCommands[currentAction++].target = actor;
        }

        if (currentAction > maxActionsToTake - 1)
        {
            Debug.Log("cant take anymore actions");
            canAct = false;
        }
        else
        {
            Debug.Log("can act again");
            canAct = true;
        }
        playerUI.EnableConfirm(true);
        playerUI.EnableCancel(true);
    }

    public void SubmitActions()
    {
        if (battleActor.SubmitActions())
        {
            currentState = playerState.waitingToAct;
        }
        currentAction = 0;
        
    }

    public void ClearActions()
    {
        battleActor.ClearCommands();
        currentAction = 0;
    }

    public void OnDeath()
    {
        currentState = playerState.dead;
        
    }

    void ChangeState(playerState newState)
    {
        ExitState(_currentState);
        EnterState(newState);
        _currentState = newState;
    }

    void CurrentActionChanged(int newValue)
    { 
        currentActionChanged.Invoke(newValue);
        _currentAction = newValue;
    }

    void EnterState(playerState state)
    {
        switch (state)
        {
            case playerState.waitingForOrders:
                playerUI.EnableActions(true);
                battleActor.regainingStamina = true;
                break;
            case playerState.waitingToAct:
                playerUI.EnableActions(false);
                battleActor.regainingStamina = false;
                break;
            default: break;
        }
    }

    void ExitState(playerState state)
    {
        switch (state)
        {
            case playerState.waitingForOrders:
                break;
            case playerState.waitingToAct:
                break;
            default: break;
        }
    }
}
