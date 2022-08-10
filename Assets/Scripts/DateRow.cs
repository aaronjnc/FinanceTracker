using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DateRow : MonoBehaviour
{
    public enum DateComparator
    {
        On,
        Before,
        After,
        Till,
    }
    [SerializeField]
    private TMP_Text textInfo;
    private DateComparator dateComparator;
    private string date1;
    private string date2;
    private bool inclusion;
    public void SingularDate(string baseDate, DateComparator comparator, bool include)
    {
        date1 = baseDate;
        dateComparator = comparator;
        inclusion = include;
        if (include)
            textInfo.text = "Include ";
        else
            textInfo.text = "Exclude ";
        textInfo.text += comparator.ToString() + " " + date1;
    }

    public void Dates(string startDate, string secondDate, bool include)
    {
        date1 = startDate;
        date2 = secondDate;
        inclusion = include;
        if (include)
            textInfo.text = "Include ";
        else
            textInfo.text = "Exclude ";
        textInfo.text += date1 + " to " + date2;
    }
}
