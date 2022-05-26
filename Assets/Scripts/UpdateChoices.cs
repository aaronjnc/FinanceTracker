using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateChoices : MonoBehaviour
{
    public enum ChoiceType
    {
        Account,
        TransactionType,
    }
    [SerializeField]
    private ChoiceType choiceType;
    private TMP_Dropdown dropdownList;
    private void Start()
    {
        dropdownList = GetComponent<TMP_Dropdown>();
        if (choiceType == ChoiceType.Account)
            TransactionManager.Instance.OnAccountNumberChange += OnAccountChange;
        else
            TransactionManager.Instance.OnTypeNumberChange += OnTypeChange;
    }

    private void OnAccountChange(int count)
    {
        dropdownList.ClearOptions();
        List<string> accounts = TransactionManager.Instance.GetAccounts();
        accounts.Insert(0, "");
        dropdownList.AddOptions(accounts);
    }

    private void OnTypeChange(int count)
    {
        dropdownList.ClearOptions();
        List<string> types = TransactionManager.Instance.GetTypes();
        dropdownList.AddOptions(types);
    }
}
