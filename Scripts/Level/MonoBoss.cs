using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonoBoss : MonoBehaviour
{
    public Image image;
    public Text healthText;
    public Slider healthSlider;
    Boss.Data currentBossData;
    public void SetBoss(Boss.Data bossData, int bossHP)
    {
        currentBossData = bossData;
        int health = bossHP;
        image.sprite = bossData.sprite;
        healthSlider.maxValue = bossData.maxHP;
        UpdateHealth(health);

    }

    public void UpdateHealth(int health)
    {
        health = Mathf.Clamp(health, 0, int.MaxValue);
        healthText.text = $"{Mathf.Clamp(health, 0, int.MaxValue)}/{currentBossData.maxHP}";
        healthSlider.value = health;
    }
}
