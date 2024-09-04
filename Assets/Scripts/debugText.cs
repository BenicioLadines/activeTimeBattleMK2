using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class debugText : MonoBehaviour
{
    BattleActor actor;
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        actor = GetComponentInParent<BattleActor>();
        text = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        text.text = string.Format("HP: {0} \n MP: {1} \n SP: {2} \n Stamina: {3}",actor.currentHP,actor.currentMP,actor.currentSP,
            actor.staminaBarProgress);
    }
}
