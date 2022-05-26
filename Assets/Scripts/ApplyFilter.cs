using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ApplyFilter : MonoBehaviour
{
    public enum FilterType
    {
        Day,
        Month,
        Year,
        Greater,
        Less,
        Equal,
        Account,
        Type,
    }
    public enum FilterValueType
    {
        Dropdown,
        InputField,
    }
    [SerializeField]
    private FilterValueType m_ValueType;
    [SerializeField]
    private FilterType filter;
    private TMP_InputField input;
    private TMP_Dropdown dropdown;

    private void Awake()
    {
        if (m_ValueType == FilterValueType.InputField)
            input = GetComponent<TMP_InputField>();
        else
            dropdown = GetComponent<TMP_Dropdown>();
    }

    public void AddFilter()
    {
        string value;
        if (m_ValueType == FilterValueType.InputField)
            value = input.text;
        else
            value = dropdown.options[dropdown.value].text;
        switch (filter)
        {
            case FilterType.Day:
                Filter.Instance.DayFilter = int.Parse(value);
                break;
            case FilterType.Month:
                Filter.Instance.MonthFilter = int.Parse(value);
                break;
            case FilterType.Year:
                Filter.Instance.YearFilter = int.Parse(value);
                break;
            case FilterType.Greater:
                Filter.Instance.GreaterFilter = int.Parse(value);
                break;
            case FilterType.Less:
                Filter.Instance.LessFilter = int.Parse(value);
                break;
            case FilterType.Equal:
                Filter.Instance.EqualFilter = int.Parse(value);
                break;
            case FilterType.Account:
                Filter.Instance.AccountFilter = value;
                break;
            case FilterType.Type:
                Filter.Instance.TypeFilter = value;
                break;
        }
        TransactionManager.Instance.AddFilter();
    }
}
