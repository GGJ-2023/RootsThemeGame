using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public BuildingData[] buildings;
    
    public NPCData[] npcs;

    public static ObjectManager instance;

    private void Awake()
    {
        instance = this;

        //load all assets
        buildings = Resources.LoadAll<BuildingData>("Buildings");
    }

    public BuildingData GetBuildingByID(string id)
    {
        for(int i = 0; i <buildings.Length; i++)
        {
            if (buildings[i].id == id)
            {
                return buildings[i];

            }
        }
        Debug.LogError("No items have been found.");
        return null;
    }
}
