using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BattleAction : ScriptableObject
{
    public BattleActor target;
    public BattleActor sender;
    [SerializeField] public int HPCost;
    [SerializeField] public int MPCost;
    [SerializeField] public int SPCost;
    [SerializeField] string displayName;
    public string animationName;
    public bool lastCommand;
    public abstract void Command();

    public void SpendPointCost()
    {
        sender.currentHP -= HPCost;
        sender.currentMP -= MPCost;
        sender.currentSP -= SPCost;
    }
}
