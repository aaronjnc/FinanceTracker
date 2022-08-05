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
        Category,
    }
    [SerializeField]
    private ChoiceType choiceType;
    private TMP_Dropdown dropdownList;
    private void Start()
    {
        dropdownList = GetComponent<TMP_Dropdown>();
        if (choiceType == ChoiceType.Account)
        {
            TransactionManager.Instance.OnAccountNumberChange += OnAccountChange;
            OnAccountChange(0);
        }
        else if (choiceType == ChoiceType.TransactionType)
        {
            TransactionManager.Instance.OnTypeNumberChange += OnTypeChange;
            OnTypeChange(0);
        }
        else
        {
            TransactionManager.Instance.OnCategoryNumberChange += OnCategoryChange;
            OnCategoryChange(0);
        }
    }

    private void OnAccountChange(int count)
    {
        dropdownList.ClearOptions();
        List<string> accounts = TransactionManager.Instance.GetAccounts();
        dropdownList.AddOptions(new List<string>() { "" });
        dropdownList.AddOptions(accounts);
    }

    private void OnTypeChange(int count)
    {
        dropdownList.ClearOptions();
        List<string> types = TransactionManager.Instance.GetTypes();
        dropdownList.AddOptions(new List<string>() { "" });
        dropdownList.AddOptions(types);
    }

    private void OnCategoryChange(int count)
    {
        dropdownList.ClearOptions();
        List<string> categories = TransactionManager.Instance.GetCategories();
        dropdownList.AddOptions(new List<string>() { "" });
        dropdownList.AddOptions(categories);
    }

    private void OnEnable()
    {
        if (dropdownList != null)
        {
            if (choiceType == ChoiceType.Account)
            {
                OnAccountChange(0);
            }
            else if (choiceType == ChoiceType.TransactionType)
            {
                OnTypeChange(0);
            }
            else
            {
                OnCategoryChange(0);
            }
        }
    }
}
