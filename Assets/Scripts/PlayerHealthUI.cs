using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    TextMeshProUGUI text;
    Slider healthSlider;
    MyEventManager eventManager;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        healthSlider = GetComponentInChildren<Slider>();
        eventManager = FindObjectOfType<MyEventManager>();
        eventManager.AddEventListener<PlayerHealthChanged>(OnPlayerHealthChanged);
    }

    void OnPlayerHealthChanged(PlayerHealthChanged nextEvent)
    {
        float sliderValue = nextEvent.Health / nextEvent.MaxHealth;
        int percentage = (int)(sliderValue * 100);
        text.text = percentage + "%";
        healthSlider.value = sliderValue;
    }

    void OnDestroy()
    {
        eventManager.RemoveEventListener<PlayerHealthChanged>(OnPlayerHealthChanged);
    }
}
