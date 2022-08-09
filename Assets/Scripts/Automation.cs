using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static AccountOption;

public class Automation
{
    private List<string> category = new List<string>();
    private List<AutomationType> automationType = new List<AutomationType>();
    private List<double> amounts = new List<double>();

    public void AddRow(string c, AutomationType percentage, double amount)
    {
        category.Add(c);
        automationType.Add(percentage);
        amounts.Add(amount/100);
    }

    public void SpawnRows(Transform contentParent, GameObject automationRow)
    {
        for (int i = 0; i < category.Count && i < automationType.Count && i < amounts.Count; i++)
        {
            GameObject newRow = GameObject.Instantiate(automationRow, contentParent);
            newRow.GetComponent<AccountOption>().BeginSetValues(category[i], automationType[i], amounts[i]);
        }
    }

    public List<Transaction> CreateTransactions(Transaction original)
    {
        List<Transaction> transactions = new List<Transaction>();
        double leftoverMoney = original.GetAmount();
        for (int i = 0; i < category.Count; i++)
        {
            double value = 0;
            if (automationType[i] == AutomationType.Percentage)
            {
                value = Mathf.Clamp((float)(original.GetAmount() * amounts[i]), 0, (float)leftoverMoney);
            }
            else if (automationType[i] == AutomationType.Amount)
            {
                value = leftoverMoney > amounts[i] ? leftoverMoney : amounts[i];
            }
            else
            {
                value = leftoverMoney;
            }
            leftoverMoney -= value;
            Transaction t = new Transaction(original.GetDate(), "Automatic Transfer", value, 
                TransactionManager.Instance.GetCategory(category[i]), "Automated");
            transactions.Add(t);
            if (leftoverMoney == 0)
                break;
        }
        return transactions;
    }

    public override string ToString()
    {
        StringBuilder stringRep = new StringBuilder();
        for (int i = 0; i < category.Count; i++)
        {
            stringRep.Append(category[i] + "|" + automationType[i].ToString() + "|" + amounts[i]);
        }
        return stringRep.ToString();
    }
}
