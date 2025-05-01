using UnityEngine;
using System.Collections.Generic;

public class StateControllerScript : MonoBehaviour
{
    public static string currLevel = "Entrance";
    public static string currZone = "SurfacePipesComp";

    public static List<string> zoneOrder = new List<string>();
    private static int zoneIndex = 0;

    private void Awake()
    {
        if (zoneOrder.Count == 0)
        {
            GenerateZoneOrder();
        }
    }

    public static void GenerateZoneOrder()
    {
        zoneOrder.Clear();
        zoneOrder.Add("Entrance");
        zoneOrder.Add("Combat1");
        zoneOrder.Add("Cafe");
        // 1/3
        zoneOrder.Add("Combat2");
        zoneOrder.Add("Combat1");
        zoneOrder.Add("Shop");
        // 2/3
        zoneOrder.Add("Combat2");
        zoneOrder.Add("Combat3");
        zoneOrder.Add("Cafe");
        // 3/3
        zoneOrder.Add("Boss");

        /*
        List<string> combatZones = new List<string> { "Combat1", "Combat2", "Combat3" };
        for (int i = 0; i < combatZones.Count; i++)
        {
            int rand = Random.Range(i, combatZones.Count);
            (combatZones[i], combatZones[rand]) = (combatZones[rand], combatZones[i]);
        }
        zoneOrder.AddRange(combatZones);
        zoneOrder.Add("Cafe");
        zoneOrder.Add("Shop");
        zoneOrder.Add("Boss");
        */

    }

    public static string GetNextZone()
    {
        zoneIndex++;
        if (zoneIndex < zoneOrder.Count)
        {
            currLevel = zoneOrder[zoneIndex];
            return currLevel;
        }
        else
        {
            return null; 
        }
    }
}
