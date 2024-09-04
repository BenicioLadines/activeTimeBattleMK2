using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider HPbar;
    [SerializeField] TextMeshProUGUI HPtext;
    [SerializeField] Slider MPbar;
    [SerializeField] TextMeshProUGUI MPtext;
    [SerializeField] Slider staminaBar;
    [SerializeField] TextMeshProUGUI SPcounter;
    [SerializeField] Button[] actionButtons = new Button[5];
    [SerializeField] Button submitButton;
    [SerializeField] Button cancelButton;

    public void setMaxHP(int maxHP)
    {
        HPbar.maxValue = maxHP;
        HPbar.value = maxHP;
        HPtext.text = maxHP + "/" + maxHP;
    }

    public void setMaxMP(int maxMP)
    {
        MPbar.maxValue = maxMP;
        MPbar.value = maxMP;
        MPtext.text = maxMP + "/" + maxMP;
    }

    public void changeHP(int HP)
    {
        HPbar.value = HP;
        HPtext.text = HP + "/" + HPbar.maxValue;
    }

    public void changeMP(int MP)
    {
        MPbar.value = MP;
        HPtext.text = MP + "/" + MPbar.maxValue;
    }

    public void changeStamina(float stamina)
    {
        staminaBar.value = stamina/100;
    }

    public void changeSP(int SP)
    {
        SPcounter.text = SP.ToString();
    }

    public void ToggleButtons()
    {
        foreach (Button button in actionButtons)
        {

            if(button != null)
            {
                button.interactable = !button.interactable;
            }
        }

        submitButton.interactable = !submitButton.interactable;
    }

}
