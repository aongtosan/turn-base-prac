using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public class StatsInfo 
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
    [SerializeField] COMBAT_CLASS hunterClass;
    [SerializeField] ABNORMAL_STATUS abnormalStatus;
    [SerializeField] int move;
    [SerializeField] int jump;
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
    public int Move
    {
        set { move = value; }
        get { return move; }
    }
    public int Jump
    {
        set { jump = value; }
        get { return jump; }
        
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
    public int Perception
    {
        set { perception = value; }
        get { return perception; }
    }
    public int Mentallity
    {
        set { mentallity = value; }
        get { return mentallity; }
    }
    public int Experience
    {
        set { experience = value; }
        get { return experience; }
    }
     public HUNTER_RANK HunterRank
    {
        set { hunterRank = value; }
        get { return hunterRank; }
    }
    public COMBAT_CLASS HunterClass
    {
        set { hunterClass = value; }
        get { return hunterClass; }
    }
    public STATUS Status
    {
        set { status = value; }
        get { return status; }
    }
    public MOVE_TYPE MOVETYPE{
        set{moveType =value;}
        get{return moveType;}
    }

    public ABNORMAL_STATUS AbnormalStatus{
        set{abnormalStatus =value;}
        get{return abnormalStatus;}
    }
}
