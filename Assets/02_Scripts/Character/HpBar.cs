using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HpBar : MonoBehaviour
{
    TMP_Text hpTMP;

    Character character;

    private void Start()
    {
        character = GetComponentInParent<Character>();
        hpTMP = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        HpUpdate();
    }

    void HpUpdate()
    {
        if (hpTMP != null)
        {
            hpTMP.text = character.Hp.ToString();
        }
    }
}
