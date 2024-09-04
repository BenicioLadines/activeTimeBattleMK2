using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BattleActor : MonoBehaviour
{

    public List<BattleAction> commandsEntered = new List<BattleAction>();
    protected GameManager gameManager;
    public Animator animator;
    public FloatEvent staminaChanged;
    public IntEvent SPchanged;
    public IntEvent HPchanged;
    public IntEvent MPchanged;
    public UnityEvent actionEnded;
    [SerializeField] int _maxHP;
    public int maxHP {  
        get { return _maxHP; }
        set { _maxHP = value; }
    }
    int _currentHP;
    public int currentHP
    {
        get { return _currentHP; }
        set
        {
            if (value <= 0)
            {

                _currentHP = 0;
                Death();
            }
            else if(value > maxHP)
            {
                _currentHP = maxHP;
            }
            else
            {
                _currentHP = value;
            }
            HPchanged.Invoke(_currentHP);
        }
    }
    [SerializeField] int _maxMP;
    public int maxMP { 
        get { return _maxMP; }
        set { _maxMP = value; }
    }
    int _currentMP;
    public int currentMP
    {
        get { return _currentMP; }
        set
        {
            if(value < 0)
            {
                _currentMP = 0;
            }
            else if (value > maxMP)
            {
                _currentMP = maxMP;
            }
            else
            {
                _currentMP= value;
            }
            MPchanged.Invoke(_currentMP);
        }
    }
    [SerializeField] int _maxSP;
    public int maxSP
    {
        get { return _maxSP; }
        set { _maxSP = value; }
    }
    [SerializeField] protected int actionLimit;
    int _currentSP;
    public int currentSP
    {
        get { return _currentSP; }
        set
        {
            if(value < -actionLimit)
            {
                _currentSP = -actionLimit;
            }
            else if(value >= maxSP)
            {
                _currentSP = maxSP;
                regainingStamina = false;
            }
            else
            {
                _currentSP = value;
                regainingStamina = true;
            }
            SPchanged.Invoke(_currentSP);
        }
    }
    float _staminaBarProgress;
    public float staminaBarProgress
    {
        get { return _staminaBarProgress; }
        set
        {
            if(value >= 100)
            {
                currentSP++;
                _staminaBarProgress = 0;
                /*if(currentSP == maxSP)
                {
                    _staminaBarProgress = 100;
                }*/
            }
            else if(value < 0)
            {
                _staminaBarProgress = 0;
            }
            else
            {
                _staminaBarProgress = value;
            }
            staminaChanged.Invoke(_staminaBarProgress);
        }
    }
    [SerializeField] protected float staminaRefill;
    bool regainingStamina = true;
    [SerializeField]protected BattleAction[] actions;
    [SerializeField]GameObject selectionReticle;

    public void initStats()
    {
        currentHP = _maxHP;
        currentMP = _maxMP;
        currentSP = 0;
        staminaBarProgress = 0;
    }

    protected void BattleActorUpdate()
    {
        if(regainingStamina)
        {
            staminaBarProgress += staminaRefill * Time.deltaTime;
        }
    }

    public void toggleReticle(bool on)
    {
        if(on)
        {
            selectionReticle.SetActive(true);
        }
        else
        {
            selectionReticle.SetActive(false);
        }
    }

    public abstract void Death();

}
