using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmountsFilter : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField amountOne;
    [SerializeField]
    private TMP_Dropdown comparator;
    [SerializeField]
    private TMP_InputField amountTwo;
    [SerializeField]
    private TMP_Dropdown include;
    [SerializeField]
    private Filter filter;
    [SerializeField]
    private GameObject amountsRow;

    public void ChangeComparator()
    {
        if (comparator.options[comparator.value].text.Equals("To"))
        {
            amountTwo.gameObject.SetActive(true);
        }
        else
        {
            amountTwo.gameObject.SetActive(false);
        }
    }

    public void AddNewRow()
    {
        float amount1;
        bool included = include.value == 0;
        if (!float.TryParse(amountOne.text, out amount1))
        {
            return;
        }
        if (float.TryParse(amountTwo.text, out float amount2))
        {
            return;
        }
        AmountsRow.AmountComparator comp = AmountsRow.AmountComparator.Equal;
        if (comparator.value == 1)
        {
            comp = AmountsRow.AmountComparator.Less_Than;
        }
        else if (comparator.value == 2)
        {
            comp = AmountsRow.AmountComparator.Greater_Than;
        }

    }

    private void ClearValues()
    {
        amountOne.text = "";
        amountTwo.text = "";
        comparator.value = 0;
        include.value = 0;
    }
}
