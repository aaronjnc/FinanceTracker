using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AddTransaction : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField dateField;
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
        if (!DateTime.TryParse(dateField.text, out date))
        {
            Debug.LogError("Invalid Date/Time");
        }
        double amount;
        if (!double.TryParse(amountField.text, out amount))
        {
            Debug.LogError("Invalid amount");
        }
        if (nameField.text.Equals(""))
            Debug.LogError("Enter a valid name");
        Transaction newTransaction = new Transaction(dateField.text, nameField.text, amount,
            TransactionManager.Instance.GetCategory(categoryField.options[categoryField.value].text), 
            typeField.options[typeField.value].text);
        TransactionManager.Instance.UpdateTransactions(newTransaction);
        ClearFields();
    }

    private void ClearFields()
    {
        dateField.text = "";
        nameField.text = "";
        amountField.text = "";
        categoryField.value = 0;
        typeField.value = 0;
    }
}
