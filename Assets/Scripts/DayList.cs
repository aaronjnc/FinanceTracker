using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayList : MonoBehaviour
{
    private TMP_Dropdown DayDropdown;
    List<string> FullMonth = new List<string>();
    List<string> ShortMonth = new List<string>();
    List<string> February = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        DayDropdown = GetComponent<TMP_Dropdown>();
        for (int i = 1; i <= 31; i++)
        {
            string day = i.ToString();
            FullMonth.Add(day);
            if (i <= 30)
            {
                ShortMonth.Add(day);
                if (i <= 28)
                    February.Add(day);
            }
        }
        SetMonthList(FullMonth);
    }

    public void UpdateList(MonthDropdownList monthScript)
    {
        int month = monthScript.GetChosenMonth();
        switch (month)
        {
            case 2:
                SetMonthList(February);
                break;
            case 4:
            case 6:
            case 9: 
            case 11:
                SetMonthList(ShortMonth);
                break;
            default:
                SetMonthList(FullMonth);
                break;
        }
    }

    private void SetMonthList(List<string> monthList)
    {
        DayDropdown.ClearOptions();
        DayDropdown.AddOptions(new List<string> { "" });
        DayDropdown.AddOptions(monthList);
    }
}
