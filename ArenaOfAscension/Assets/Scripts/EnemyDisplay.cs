using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EnemyDisplay : MonoBehaviour
{
    public Enemy enemy;
    EnemyBehaviour enemyBehaviour;
    [SerializeField] private Image image;
    public TextMeshProUGUI nameText, sTRText, healthText;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        health = enemy.enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        SetEnemyValues();
    }

    private void SetEnemyValues()
    {
        nameText.text = enemy.enemyName;
        sTRText.text = enemy.enemyATK.ToString();
        healthText.text = enemyBehaviour.currentHealth.ToString();
        image.sprite = enemy.artwork;
    }
}
