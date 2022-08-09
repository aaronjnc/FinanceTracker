using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonthDropdownList : MonoBehaviour
{
    private TMP_Dropdown monthDropdown;
    public static readonly List<string> Months = new List<string>() 
    { "January", "February", "March", "April", "May", "June", "July", "August", 
        "September", "October", "November", "December" };
    private void Start()
    {
        monthDropdown = GetComponent<TMP_Dropdown>();
        monthDropdown.ClearOptions();
        monthDropdown.AddOptions(new List<string>() { "" });
        monthDropdown.AddOptions(Months);
    }

    public int GetChosenMonth()
    {
        if (monthDropdown.value == 0)
            return 0;
        return monthDropdown.value;
    }

    public string GetMonthString(int i)
    {
        return Months[i - 1];
    }

    public int GetMonthNumber(string Month)
    {
        return Months.IndexOf(Month);
    }

    public static string GetMonthAndYear(string yearAndMonth)
    {
        int month = int.Parse(yearAndMonth.Split(' ')[1]);
        string year = yearAndMonth.Split(' ')[0];
        return Months[month - 1] + " " + year;
    }

    public static string GetYearAndMonth(string monthAndYear)
    {
        string month = monthAndYear.Split(' ')[0];
        string year = monthAndYear.Split(' ')[1];
        int monthNum = Months.IndexOf(month) + 1;
        if (monthNum < 10)
            return year + " 0" + monthNum;
        else
            return year + " " + monthNum;
    }
}
