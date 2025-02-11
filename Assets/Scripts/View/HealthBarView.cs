using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    public Image hpBar;
    float maxHealth, curHealth;

    public void UpdateMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        UpdateFill();
    }

    public void UpdateCurHealth(float newCurHealth)
    {
        curHealth = newCurHealth;
        UpdateFill();
    }

    void UpdateFill()
    {
        hpBar.fillAmount = curHealth / maxHealth;
    }
}
