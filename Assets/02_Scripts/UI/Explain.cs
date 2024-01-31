using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explain : MonoBehaviour
{
    [SerializeField] GameObject explainObj;
    private bool isToggle = false;

    private void Start()
    {
        explainObj = transform.GetChild(0).gameObject;
    }

    public void Explain_UI_Toggle()
    {
        isToggle = !isToggle;
        explainObj.SetActive(isToggle);
    }
}
