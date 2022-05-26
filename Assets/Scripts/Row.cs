using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Row : MonoBehaviour
{
    [SerializeField]
    private Text Date;
    [SerializeField]
    private Text Desc;
    [SerializeField]
    private Text Amount;
    [SerializeField]
    private Text Account;
    [SerializeField]
    private Text TransactionType;
    private int ListID;
    
    public void Display(Transaction transaction, int id)
    {
        ListID = id;
        Date.text = transaction.GetDate();
        Desc.text = transaction.GetDescription();
        Amount.text = transaction.GetAmount().ToString();
        Account.text = transaction.GetAccount();
        TransactionType.text = transaction.GetTransactionType();
        Enable();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
}
