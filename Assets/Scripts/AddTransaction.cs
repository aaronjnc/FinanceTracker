using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AddTransaction : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField DateField;
    [SerializeField]
    private TMP_InputField NameField;
    [SerializeField]
    private TMP_InputField AmountField;
    [SerializeField]
    private TMP_Dropdown AccountField;
    [SerializeField]
    private TMP_Dropdown TypeField;

    public void AddTheTransaction()
    {
        DateTime date;
        if (!DateTime.TryParse(DateField.text, out date))
        {
            //Debug.LogError("Invalid Date/Time");
        }
        double amount;
        if (!double.TryParse(AmountField.text, out amount))
        {
            Debug.LogError("Invalid amount");
        }
        if (NameField.text.Equals(""))
            Debug.LogError("Enter a valid name");
        Transaction newTransaction = new Transaction(DateField.text, NameField.text, amount,
            AccountField.options[AccountField.value].text, TypeField.options[TypeField.value].text);
        TransactionManager.Instance.AddTransaction(newTransaction);
    }
}
