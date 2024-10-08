using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Troll : EnemyControl
{
    private void Start()
    {
        EnemyStart();
    }
    // Update is called once per frame
    void Update()
    {
        
        EnemyUpdate();

        if(battleActor.currentSP > 1)
        {
            Attack();
        }

        
    }

    void Attack()
    {
        battleActor.currentSP -= 1;
    }
}
