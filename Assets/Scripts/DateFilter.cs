using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class DateFilter : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown day1;
    [SerializeField]
    private TMP_Dropdown month1;
    [SerializeField]
    private TMP_InputField year1;
    [SerializeField]
    private TMP_Dropdown comparator;
    [SerializeField]
    private TMP_Dropdown day2;
    [SerializeField]
    private TMP_Dropdown month2;
    [SerializeField]
    private TMP_InputField year2;
    [SerializeField]
    private TMP_Dropdown include;
    [SerializeField]
    private Filter filter;
    [SerializeField]
    private GameObject dateRow;
    public void ChangeComparator()
    {
        if (comparator.options[comparator.value].text.Equals("Till"))
        {
            day2.gameObject.SetActive(true);
            month2.gameObject.SetActive(true);
            year2.gameObject.SetActive(true);
        }
        else
        {
            day2.gameObject.SetActive(false);
            month2.gameObject.SetActive(false);
            year2.gameObject.SetActive(false);
        }
    }
    
    public void AddNewRow()
    {
        GameObject spawnedObj = filter.AddRow(dateRow);
        string dateRowOne = GetDateRow(day1, month1, year1);
        if (dateRowOne.Equals(""))
        {
            Destroy(spawnedObj);
            return;
        }
        if (comparator.options[comparator.value].text.Equals("Till"))
        {
            string dateRowTwo = GetDateRow(day2, month2, year2);
            spawnedObj.GetComponent<DateRow>().Dates(dateRowOne, dateRowTwo, include.value == 0);
            comparator.value = 0;
            include.value = 0;
            return;
        }
        DateRow.DateComparator dateComparator = DateRow.DateComparator.On;
        if (comparator.options[comparator.value].text.Equals("Before"))
        {
            dateComparator = DateRow.DateComparator.Before;
        }
        else if (comparator.options[comparator.value].text.Equals("After"))
        {
            dateComparator = DateRow.DateComparator.After;
        }
        comparator.value = 0;
        include.value = 0;
        spawnedObj.GetComponent<DateRow>().SingularDate(dateRowOne, dateComparator, include.value == 0);
    }

    private string GetDateRow(TMP_Dropdown day, TMP_Dropdown month, TMP_InputField year)
    {
        StringBuilder dateString = new StringBuilder();
        if (month.value != 0)
        {

            dateString.Append(month.options[month.value].text);
        }
        if (day.value != 0)
        {
            if (!dateString.Equals(""))
                dateString.Append('/');
            dateString.Append(day.options[day.value].text);
        }
        month.value = 0;
        day.value = 0;
        if (int.TryParse(year.text, out int yearVal))
        {
            if (!dateString.Equals(""))
                dateString.Append('/');
            dateString.Append(year.text);
        }
        year.text = "";
        return dateString.ToString();
    }
}
