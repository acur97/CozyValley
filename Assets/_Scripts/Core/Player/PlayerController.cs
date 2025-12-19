using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public static Action OnDeath;

    [Header("References")]
    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartHalf;
    [SerializeField] private Sprite heartEmpty;

    [Header("UI")]
    [SerializeField] private Image heartImage;

    private int maxHealth = 10;
    private int currentHealth = 10;

    private List<Image> hearts;

    private void Awake()
    {
        instance = this;

        hearts = heartImage.transform.parent.GetComponentsInChildren<Image>().ToList();
    }

    public void UpHealth(int health)
    {
        currentHealth += health;
    }

    public void AddHeart()
    {
        maxHealth += 2;
        currentHealth = maxHealth;

        hearts.Add(Instantiate(heartImage, heartImage.transform.parent));

        UpdateHealth();
    }

    public void DownHealth(int health)
    {
        currentHealth -= health;
        UpdateHealth();

        if (currentHealth < 0)
        {
            OnDeath?.Invoke();
        }
    }

    private void UpdateHealth()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            switch (Mathf.Clamp(currentHealth - (i * 2), 0, 2))
            {
                case 0:
                    hearts[i].sprite = heartEmpty;
                    break;
                case 1:
                    hearts[i].sprite = heartHalf;
                    break;
                case 2:
                    hearts[i].sprite = heartFull;
                    break;
            }
        }
    }
}