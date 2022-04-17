using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsInfo : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] string unitName;
    [SerializeField] int strenght;
    [SerializeField] int intelligent;
    [SerializeField] int agility;

    [SerializeField] STATUS status;
    [SerializeField] ABNORMAL_STATUS abnormalStatus;
    public enum STATUS{
        HEALTHY,
        INJURED,
        ABNORMAL,
        DEATH
    }
    public enum ABNORMAL_STATUS{
        NONE,
        PARALYSE,
        POISONED,
        BURN,
        FROSTBITE,
        BREEDING,
        CONFUSED,
        BLIND
    }
    public int Level
    {
        set { level = value; }
        get { return level; }
    }
    public string UnitName
    {
        set { unitName = value; }
        get { return unitName; }
    }
    public int Strength
    {
        set { strenght = value; }
        get { return strenght; }
    }
    public int Intelligent
    {
        set { intelligent = value; }
        get { return intelligent; }
    }
    public int Agility
    {
        set { agility = value; }
        get { return agility; }
    }
}
