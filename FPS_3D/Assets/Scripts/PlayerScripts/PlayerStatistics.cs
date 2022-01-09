using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : MonoBehaviour
{
    // HEALTH & STAMINA - decrease progress bars in UI

    [SerializeField] private Image _healthStats, _staminaStats; // Image object from UI library

    public void DisplayHealthStats(float healthValue)
    {
        healthValue /= 100f;
        _healthStats.fillAmount = healthValue; // bar fills fractional amount of 100.
                                               // healthvalue is (total starting health) - (total damage)
    }
    public void DisplayStaminaStats(float staminaValue)
    {
        staminaValue /= 100f;
        _staminaStats.fillAmount = staminaValue;
    }

}
