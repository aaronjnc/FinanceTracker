using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    private Button previousButton;
    private GameObject previousGroup;
    public void ChangeButton(Button newBackground)
    {
        if (previousButton != null)
            previousButton.interactable = true;
        newBackground.interactable = false;
        previousButton = newBackground;
    }

    public void ChangePage(GameObject newGroup)
    {
        if (previousGroup != null)
            previousGroup.SetActive(false);
        newGroup.SetActive(true);
        previousGroup = newGroup;
    }
}
