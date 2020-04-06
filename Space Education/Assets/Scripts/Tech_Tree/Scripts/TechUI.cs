using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TechUI : MonoBehaviour
{
    public Tech tech;

    public Image iconHolder;

    public Text techName;
    public Text techPoints;
    public Text techDescription;

    public TechManager tm;

    public int techPointCount = 0;
    public int prevTechPointCount = 0;

    public bool unlockChildren = false;
    public bool weEnabled = false;

    public TechUI[] children;
    public TechUI techParent;

    public Color enabledColor;
    public Color disabledColor;

    private void OnGUI()
    {
        iconHolder.sprite = tech.icon;

        if (techName != null)
            techName.text = tech.techName;

        techPoints.text = techPointCount.ToString();

        if(weEnabled)
        {
            iconHolder.color = enabledColor;
        }
        else
        {
            iconHolder.color = disabledColor;
        }
    }

    private void start()
    {
        if (tech.parentTech == null) 
            weEnabled = true;
        else
        {
            weEnabled = false;
        }

        
    }

    private void Update()
    {
        if (!weEnabled)
        {
            lockChildren();
        }
    }

    public void lockChildren()
    {
        foreach(var c in children)
        {
            if(c!= null)
            {
                c.lockAndFlush();
            }
        }
    }

    public void unlockChilds()
    {
        foreach(var c in children)
        {
            if (c.tech.parentTech.techName == tech.techName)
            {
                c.weEnabled = true;
            }
        }
        
    }

    public void lockAndFlush()
    {
        weEnabled = false;
        int leftover = techPointCount;
        techPointCount = 0;
        tm.myTechPointsSpent -= leftover;
    }

    public void clickedAdd()
    {
        if (!weEnabled) return;

        if(techPointCount < tech.maxTechPoints && tm.myTechPointsFree != 0 && tm.myTechPointsSpent < tm.myTechPointsFree)
        {
            techPointCount += 1;
            tm.myTechPointsSpent += 1;

            if (techPointCount == tech.maxTechPoints)
            {

                unlockChildren = true;
                unlockChilds();
            }       
           
        }
    }

    public void clickedRemove()
    {
        if (!weEnabled) return;
        if(techPointCount > 0 && tm.myTechPointsFree != tm.maxTechPoints && techPointCount > prevTechPointCount)
        {
            techPointCount -= 1;

            tm.myTechPointsSpent -= 1;

            unlockChildren = false;

            if(techPointCount < tech.maxTechPoints)
            {
                lockChildren();
            }
        }
    }
}
