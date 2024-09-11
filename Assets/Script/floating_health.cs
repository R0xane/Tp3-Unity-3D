using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floating_health : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider slider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void UpdateHealth(float health, float maxHealth)
    {
        slider.value = health/maxHealth;
    }
}
