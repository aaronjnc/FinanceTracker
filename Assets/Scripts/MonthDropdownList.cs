using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonthDropdownList : MonoBehaviour
{
    public static readonly List<string> Months = new List<string>() 
    { "January", "February", "March", "April", "May", "June", "July", "August", 
        "September", "October", "November", "December" };
    private void Start()
    {
        TMP_Dropdown monthDropdown = GetComponent<TMP_Dropdown>();
        monthDropdown.ClearOptions();
        monthDropdown.AddOptions(new List<string>() { "" });
        monthDropdown.AddOptions(Months);
    }

    public string GetMonthString(int i)
    {
        return Months[i - 1];
    }

    public int GetMonthNumber(string Month)
    {
        return Months.IndexOf(Month);
    }
}
