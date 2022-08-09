using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Globalization;

public class AddTransaction : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dayField;
    [SerializeField]
    private MonthDropdownList monthList;
    [SerializeField]
    private TMP_Dropdown monthField;
    [SerializeField]
    private TMP_InputField yearField;
    [SerializeField]
    private TMP_InputField nameField;
    [SerializeField]
    private TMP_InputField amountField;
    [SerializeField]
    private TMP_Dropdown categoryField;
    [SerializeField]
    private TMP_Dropdown typeField;

    public void AddTheTransaction()
    {
        DateTime date;
        int year = 0;
        if (dayField.value == 0 || monthList.GetChosenMonth() == 0 || !int.TryParse(yearField.text, out year))
        {
            return;
        }
        date = new DateTime(year, monthList.GetChosenMonth(), dayField.value);
        DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
        dtfi.ShortDatePattern = @"dd/MM/yyyy";
        double amount;
        if (!double.TryParse(amountField.text, out amount))
        {
            Debug.LogError("Invalid amount");
        }
        if (nameField.text.Equals(""))
            Debug.LogError("Enter a valid name");
        Transaction newTransaction = new Transaction(date.ToString("d", dtfi), nameField.text, amount,
            TransactionManager.Instance.GetCategory(categoryField.options[categoryField.value].text), 
            typeField.options[typeField.value].text);
        TransactionManager.Instance.UpdateTransactions(newTransaction);
        ClearFields();
    }

    private void ClearFields()
    {
        dayField.value = 0;
        monthField.value = 0;
        yearField.text = "";
        nameField.text = "";
        amountField.text = "";
        categoryField.value = 0;
        typeField.value = 0;
    }
}
