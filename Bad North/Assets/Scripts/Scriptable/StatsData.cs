using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Stats")]
public class StatsData : ScriptableObject
{
    [Header("Stats")]
    [Space(10)]
    public float health;
    public float dmg;
    public float cooldown;
}
