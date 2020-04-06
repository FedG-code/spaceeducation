using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TechType
{
    BUFF_TYPE,
    BUFF_STAT
}

public enum TECH_ELEMENT_TYPE
{
    FOOD,
    FUEL,
    METAL,
    RESEARCH
}

[CreateAssetMenu(fileName = "Tech", menuName = "Proj/Techs/NewTech", order = 1)]

public class Tech : ScriptableObject
{
    public int maxTechPoints;

    public Tech parentTech;
    public Tech[] childTalents;

    public string techName;
    public string description;

    public Sprite icon;

    public TechType techType;

    public float buffPerPoint = 0.5f;

    public TECH_ELEMENT_TYPE[] elementsToEffect;

    public string statToEffect = "";

}
