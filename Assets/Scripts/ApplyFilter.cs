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
    private FilterValueType valueType;
    [SerializeField]
    private FilterType filter;
    private TMP_InputField input;
    private TMP_Dropdown dropdown;

    private void Awake()
    {
        if (valueType == FilterValueType.InputField)
            input = GetComponent<TMP_InputField>();
        else
            dropdown = GetComponent<TMP_Dropdown>();
    }

    public void AddFilter()
    {
        string value;
        if (valueType == FilterValueType.InputField)
            value = input.text;
        else
            value = dropdown.options[dropdown.value].text;
        switch (filter)
        {
            case FilterType.Day:
                Filter.Instance.dayFilter = int.Parse(value);
                break;
            case FilterType.Month:
                Filter.Instance.monthFilter = int.Parse(value);
                break;
            case FilterType.Year:
                Filter.Instance.yearFilter = int.Parse(value);
                break;
            case FilterType.Greater:
                Filter.Instance.greaterFilter = int.Parse(value);
                break;
            case FilterType.Less:
                Filter.Instance.lessFilter = int.Parse(value);
                break;
            case FilterType.Equal:
                Filter.Instance.equalFilter = int.Parse(value);
                break;
            case FilterType.Account:
                Filter.Instance.categoryFilter = value;
                break;
            case FilterType.Type:
                Filter.Instance.typeFilter = value;
                break;
        }
        TransactionManager.Instance.AddFilter();
    }
}
