using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsInfo : MonoBehaviour
{
    private int level;
    private int strenght;
    private int intelligent;
    private int agility;

    public int Level
    {
        set { level = value; }
        get { return level; }
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
