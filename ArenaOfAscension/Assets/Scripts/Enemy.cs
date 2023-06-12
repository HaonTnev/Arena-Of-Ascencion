using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies")]
public class Enemy: ScriptableObject 
{



    public int enemyHealth;
    public int enemyATK;
    public string enemyName;

    public Sprite artwork;



}
