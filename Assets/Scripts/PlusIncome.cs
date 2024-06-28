using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlusIncome : MonoBehaviour
{
    public TextMeshProUGUI tmpro;

    public void EditIncomeText(int income)
    {

        tmpro.text = "+" + income;
    }
}
