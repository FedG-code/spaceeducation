using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TechContainer
{
    public Tech tech;
    public int points;
}

public class TechManager : MonoBehaviour
{
    public int maxTechPoints = 900;
    public int myTechPointsFree = 10;
    public int myTechPointsSpent = 0;

    public List<TechContainer> aTechs = new List<TechContainer>();

    public TechContainer getTechByName(string techName)
    {
        return aTechs.FirstOrDefault(o => o.tech.techName == techName);
    }

    public void removeTalentByName(string techName)
    {
        int cnt = -1;
        foreach (var t in aTechs)
        {
            cnt++;
            if(t.tech.techName == techName)
            {
                aTechs.RemoveAt(cnt);
                break;
            }

        }
    }

    private void Update()
    {
        if (myTechPointsFree > maxTechPoints)
        {
            myTechPointsFree = maxTechPoints;
        }
    }
}
