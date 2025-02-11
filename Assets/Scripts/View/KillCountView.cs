using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCountView : MonoBehaviour
{
    [SerializeField] private string message = "Kill Count:";
    public TextMeshProUGUI killCountText;

    private int curCount = 0;

    public void Start()
    {
        
    }
    public void ResetKillCount()
    {
        curCount = 0;
        UpdateKillCount();
    }

    public void IncrementKillCount()
    {
        curCount++;
        UpdateKillCount();
    }

    void UpdateKillCount()
    {
        killCountText.text = $"{message} {curCount}";
    }

}
