using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableHealth : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    public void UpdateHealthBar(int currentHealth)
    {
        slider.value = currentHealth;
    }
    void Update()
    {
        
    }
}
