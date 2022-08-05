using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountTotals : MonoBehaviour
{
    private static AccountTotals _instance;
    public static AccountTotals Instance
    {
        get
        {
            return _instance;
        }
    }
    [SerializeField]
    private GameObject accountLinePrefab;
    [SerializeField]
    private Transform contentParent;
    private Dictionary<string, AccountLine> accountLines = new Dictionary<string, AccountLine>();
    private void Awake()
    {
        _instance = this;
    }

    public void AddAccount(Account account)
    {
        string accountName = account.GetAccountName();
        if (accountLines.ContainsKey(accountName))
            return;
        GameObject accountLine = Instantiate(accountLinePrefab, contentParent);
        accountLines.Add(accountName, accountLine.GetComponent<AccountLine>());
        accountLines[accountName].SetAccount(account);
    }

    public void RemoveAccount(string accountName)
    {
        if (!accountLines.ContainsKey(accountName))
            return;
        Destroy(accountLines[accountName].gameObject);
        accountLines.Remove(accountName);
    }
}
