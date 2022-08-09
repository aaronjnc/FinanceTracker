using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryTotals : MonoBehaviour
{
    private static CategoryTotals _instance;
    public static CategoryTotals Instance
    {
        get
        {
            return _instance;
        }
    }
    [SerializeField]
    private GameObject categoryLinePrefab;
    [SerializeField]
    private Transform contentParent;
    private Dictionary<string, CategoryLine> categoryLines = new Dictionary<string, CategoryLine>();
    private void Awake()
    {
        _instance = this;
    }

    public void AddCategory(Category category)
    {
        string categoryName = category.GetCategoryName();
        if (categoryLines.ContainsKey(categoryName) || categoryName.Equals("Automated"))
            return;
        GameObject categoryLine = Instantiate(categoryLinePrefab, contentParent);
        categoryLines.Add(categoryName, categoryLine.GetComponent<CategoryLine>());
        categoryLines[categoryName].SetCategory(category);
    }

    public void RemoveCategory(string categoryName)
    {
        if (!categoryLines.ContainsKey(categoryName))
            return;
        Destroy(categoryLines[categoryName].gameObject);
        categoryLines.Remove(categoryName);
    }
}
