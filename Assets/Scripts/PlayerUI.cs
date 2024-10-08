using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI HPText;
    [SerializeField]TextMeshProUGUI MPText;
    [SerializeField]TextMeshProUGUI SPText;
    [SerializeField]Slider HPbar;
    [SerializeField]Slider MPBar;
    [SerializeField]Slider StaminaBar;
    [SerializeField] GameObject actions;
    [SerializeField] GameObject actionSlots;
    public List<Button> actionButtons;
    List<Image> actionSlotImages = new List<Image>();
    [SerializeField] Color actionSlotColor;
    public Button confirmButton;
    public Button cancelButton;

    private void Start()
    {
        actionSlotImages.AddRange(actionSlots.GetComponentsInChildren<Image>());
        actionButtons.AddRange(actions.GetComponentsInChildren<Button>());
    }

    public void EnableActions(bool enabled)
    {
        foreach (Button button in actionButtons)
        {
            button.interactable = enabled;
        }

    }

    public void EnableConfirm(bool enabled) => confirmButton.interactable = enabled;

    public void EnableCancel(bool enabled) => cancelButton.interactable = enabled;

    public void SetMaxHP(int maxHP)
    {
        HPbar.maxValue = maxHP;
    }

    public void ChangeHP(int newHP)
    {
        HPbar.value = newHP;
    }

    public void SetMaxMP(int maxMP)
    {
        MPBar.maxValue = maxMP;
    }

    public void ChangeMP(int newMP)
    {
        MPBar.value = newMP;
    }

    public void ChangeSP(int newSP)
    {
        SPText.text = newSP.ToString();
    }

    public void ChangeStamina(float newStamina)
    {
        StaminaBar.value = newStamina;
    }

    public void ChangeCurrentActionSlots(int newValue)
    {
        for(int i = 0; i < actionSlotImages.Count; i++)
        {
            if (i >= newValue)
            {
                actionSlotImages[i].color = Color.black;
            }
            else
            {
                actionSlotImages[i].color = actionSlotColor;
            }
        }
    }
}
