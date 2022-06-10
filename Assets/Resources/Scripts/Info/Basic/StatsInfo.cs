using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsInfo : MonoBehaviour
{
    [SerializeField] int tier;
    [SerializeField] string unitName;
    [SerializeField] int strenght;
    [SerializeField] int intelligent;
    [SerializeField] int agility;
    [SerializeField] int mentallity;
    [SerializeField] int perception;
    [SerializeField] int experience;
    [SerializeField] MOVE_TYPE moveType;
    [SerializeField] STATUS status;
    [SerializeField] HUNTER_RANK hunterRank;
    [SerializeField] COMBAT_CLASS hunterCLass;
    [SerializeField] ABNORMAL_STATUS abnormalStatus;
    public enum STATUS{
        HEALTHY,
        INJURED,
        ABNORMAL,
        DEATH
    }
    public enum MOVE_TYPE{
        WALK,
        FLY,
        TELEPORT
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
    public enum HUNTER_RANK{
        APPERENTICE,
        ROCKIEHUNER,
        HUNTER,
        HIGHRANKHUNTER,
        MASTER,
        LEGENDARY
    }
    public enum COMBAT_CLASS {
        INITIATOR,
        STRIKER,
        MARKMAN,
        SUPPORTER
    }
    public int Tier
    {
        set { tier = value; }
        get { return tier; }
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
    public MOVE_TYPE MOVETYPE{
        set{moveType =value;}
        get{return moveType;}
    }

    void resumeFromDelay(IEnumerator delay){
         StartCoroutine(delay);
    }
}
