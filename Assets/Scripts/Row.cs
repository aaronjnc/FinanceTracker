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
    private Text Category;
    [SerializeField]
    private Text TransactionType;
    [SerializeField]
    private Image backgroundHighlight;
    private int ListId;
    
    public void Display(Transaction transaction, int id)
    {
        ListId = id;
        Date.text = transaction.GetDate();
        Desc.text = transaction.GetDescription();
        Amount.text = transaction.GetAmount().ToString("C2");
        Category.text = transaction.GetCategoryName();
        TransactionType.text = transaction.GetTransactionType();
        Enable();
    }

    public void SelectRow()
    {
        Color c = backgroundHighlight.color;
        c.a = 255;
        backgroundHighlight.color = c;
        GetComponentInParent<ChooseRow>().ChangeRow(this);
    }

    public void DeselectRow()
    {
        Color c = backgroundHighlight.color;
        c.a = 0;
        backgroundHighlight.color = c;
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
