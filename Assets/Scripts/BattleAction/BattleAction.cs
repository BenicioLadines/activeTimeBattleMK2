using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BattleAction : ScriptableObject
{
    public BattleActor target;
    public BattleActor sender;
    [SerializeField]public int HPcost;
    [SerializeField]public int MPcost;
    [SerializeField]public int SPcost;
    [SerializeField]string displayName;
    [SerializeField] public string animationName;
    public bool lastCommand;

    public abstract void Command();

    public void SpendPointCost()
    {
        sender.currentHP -= HPcost;
        sender.currentMP -= MPcost;
    }

}
