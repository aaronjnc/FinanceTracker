using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CategoryLine : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI categoryTotal;
    [SerializeField]
    private TextMeshProUGUI categoryNameField;
    private Category category;
    public void SetCategory(Category cat)
    {
        category = cat;
        UpdateName(category.GetCategoryName());
        category.OnCategoryValueChange += UpdateValue;
        UpdateValue(category.GetCategoryValue());
    }
    public void UpdateName(string categoryName)
    {
        categoryNameField.text = categoryName;
    }
    public void UpdateValue(double amount)
    {
        categoryTotal.text = amount.ToString("C2");
    }
}
