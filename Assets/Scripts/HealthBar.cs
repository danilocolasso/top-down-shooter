using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private Character character;

    void Start()
    {
        character = GetComponentInParent<Character>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetHealth(int maxHealth, int health)
    {
        slider.maxValue = maxHealth;
        slider.value = health;
    }

    void FixedUpdate()
    {
        
        var targetPos = Camera.main.WorldToScreenPoint(character.transform.position);
        // TODO put the bar above the character head. SHIT!
        transform.position = targetPos;
    }
}
