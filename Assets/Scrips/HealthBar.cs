using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }
    void Setup()
    {
        slider.maxValue = 100;
        slider.value = 100;
    }
    // Update is called once per frame
    public void setHealth(int health)
    {
        int applyHealth = health < 0 ? 0 : health;
        slider.value = applyHealth;
    }
    void Update()
    {
        
    }
}
