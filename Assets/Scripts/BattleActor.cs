using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleActor : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]public int maxHP;
    int _currentHP;
    public int currentHP 
    {  
        get => _currentHP;
        set => SetHP(value);
    }
    [SerializeField] public int maxMP;
    int _currentMP;
    public int currentMP
    {
        get => _currentMP;
        set => SetMP(value);
    }
    [SerializeField]public int maxSP;
    int _currentSP;
    public int currentSP
    {
        get => _currentSP;
        set => SetSP(value);
    }
    float _currentStaminaPercent;
    public float currentStaminaPercent
    {
        get => _currentStaminaPercent;
        set => SetStamina(value);
    }
    public UnityEvent Death;
    public UnityEvent applyAction;
    public UnityEvent animationEnded;
    public bool regainingStamina;
    public int attackPower;
    public int magicPower;
    public IntEvent hpChanged;
    public IntEvent mpChanged;
    public IntEvent spChanged;
    public FloatEvent staminaChanged;
    public List<BattleAction> actions;
    public List<BattleAction> enteredCommands;
    public Animator animator;
    public GameObject selectionReticle;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        currentHP = maxHP;
        currentMP = maxMP;
        currentSP = 0;
        currentStaminaPercent = 0f;
    }

    public void regainStamina(float change)
    {
        currentStaminaPercent += change;
    }

    void SetHP(int newHP)
    {
        if (newHP <= 0)
        {
            _currentHP = 0;
            hpChanged.Invoke(_currentHP);
            Death.Invoke();
            return;
        }
        
        if (newHP > maxHP)
        {
            _currentHP = maxHP;
            hpChanged.Invoke(_currentHP);
            return;
        }

        _currentHP = newHP;
        hpChanged.Invoke(_currentHP);
    }

    void SetMP(int newMP)
    {
        if (newMP <= 0)
        {
            _currentMP = 0;
            mpChanged.Invoke(_currentMP);
            return;
        }

        if(newMP > maxMP)
        {
            _currentMP = maxMP;
            mpChanged.Invoke(_currentMP);
            return;
        }

        _currentMP = newMP;
        mpChanged.Invoke(_currentMP);
    }

    void SetSP(int newSP)
    {
        if(newSP <= -maxSP)
        {
            _currentSP = -maxSP;
            spChanged.Invoke(_currentSP);
            return;
        }

        if(newSP > maxSP)
        {
            _currentSP = maxSP;
            spChanged.Invoke(_currentSP);
            return;
        }

        _currentSP = newSP;
        spChanged.Invoke(_currentSP);
    }

    void SetStamina(float newStamina)
    {
        if(newStamina >= 100f)
        {
            _currentStaminaPercent = 0;
            currentSP++;
            staminaChanged.Invoke(_currentStaminaPercent);
            if(currentSP == maxSP)
            {
                regainingStamina = false;
            }
            return;
        }

        _currentStaminaPercent = newStamina;
        staminaChanged.Invoke(_currentStaminaPercent);

    }

    public void toggleReticle(bool enabled)
    {
        if (enabled)
        {
            selectionReticle.SetActive(true);
        }
        else
        {
            selectionReticle.SetActive(false);
        }
    }

    public void AddCommand(BattleAction action)
    {
        BattleAction newAction = Instantiate(action);
        newAction.sender = this;
        enteredCommands.Add(newAction);
        if (enteredCommands.Count < 2)
        {
            enteredCommands[0].lastCommand = true;
        }
        else
        {
            enteredCommands[enteredCommands.Count - 2].lastCommand = false;
            enteredCommands[enteredCommands.Count - 1].lastCommand = true;
        }
    }

    public bool SubmitActions()
    {
        if (enteredCommands.Count == 0)
        {
            Debug.Log("nothing to submit!");
            return false;
        }

        int totalSPcost = 0;
        foreach (BattleAction action in enteredCommands)
        {
            totalSPcost += action.SPCost;
        }

        if (currentSP - totalSPcost < -maxSP || currentSP < 0)
        {
            Debug.Log("cant submit!");
            ClearCommands();
            return false;
        }
        currentSP -= totalSPcost;
        gameManager.actionQueue.AddRange(enteredCommands);
        ClearCommands();
        return true;
    }

    public void SubmitWithPriority()
    {
        gameManager.actionQueue.InsertRange(1, enteredCommands);
    }

    public void ClearCommands()
    {
        enteredCommands.Clear();
    }

    void ApplyAction()
    {
        applyAction.Invoke();
    }

}
