using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Game/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Àû ¼³Á¤")]
    public EnemyType type;
    public int Attackdamage;
    public int maxHP;
    public float attackSpeed = 0f;
    public GameObject enemyPrefab;

    public enum EnemyType
    { 
        bicycle,
        drone,
        bigCar,
        dangerousCar,
        tank
    }

}
