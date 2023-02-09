using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class IncreasePoints : MonoBehaviour
{
    public TMP_Text score;

    public static IncreasePoints instance; 

    private void Start()
    {
        instance = this; 
        score.text = "" + 0 + "/5"; 
    }

    public void UpdateScore(int amount)
    {
        score.text = "" + amount.ToString() + "/5";
    }
}
