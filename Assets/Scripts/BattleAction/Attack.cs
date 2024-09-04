using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Attack : BattleAction
{
    [SerializeField] int damage;


    public override void Command()
    {
        target.currentHP -= damage;
        SpendPointCost();
    }

}
