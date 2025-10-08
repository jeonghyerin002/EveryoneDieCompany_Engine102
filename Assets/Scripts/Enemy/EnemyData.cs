using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab;
    public Sprite enemyIcon;

    [Header("Ω∫≈»")]
    public int health = 30;
    public int damage = 10;
    public float moveSpeed = 2f;

    [Header("∫∏ªÛ")]
    public int expReward = 10;
    public int goldReward = 5;
}
