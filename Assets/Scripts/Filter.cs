using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter : MonoBehaviour
{
    private static Filter _instance;
    public static Filter Instance
    {
        get
        {
            return _instance;
        }
    }
    [HideInInspector]
    public int dayFilter = -1;
    [HideInInspector]
    public int monthFilter = -1;
    [HideInInspector]
    public int yearFilter = -1;
    [HideInInspector]
    public int greaterFilter = -1;
    [HideInInspector]
    public int lessFilter = -1;
    [HideInInspector]
    public int equalFilter = -1;
    [HideInInspector]
    public string categoryFilter = "";
    [HideInInspector]
    public string typeFilter = "";
    [SerializeField]
    private Transform contentObject;
    private void Awake()
    {
        _instance = this;
    }

    public GameObject AddRow(GameObject filterRow)
    {
        return Instantiate(filterRow, contentObject);
    }
}
