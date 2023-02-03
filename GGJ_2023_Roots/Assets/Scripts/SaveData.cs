using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //Camera Location
    public SVec3 cameraPos;
    public SVec3 cameraRot;
    public SVec3 cameraLook;
    //player stats
    public float day;
    public float nutrients;
    public float food;
    public float water;
    public float curPopulation;
    public float maxPopulation;
    public float curJobs;
    public float maxJobs;

    //buildings
    public SBuilding[] buildings;
    //npcs
    public SNPC[] npcs;
    //time
    public float timeOfDay;
}
[System.Serializable]
public struct SVec3
{
    public float x;
    public float y;
    public float z;

    public SVec3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }
    public Vector3 GetVector3()
    {
        return new Vector3(x, y, z);
    }
}

[System.Serializable]
public struct SBuilding
{
    public string buildingId;
    public SVec3 position;
    public SVec3 rotation;
    public string customProperties;
}

[System.Serializable]
public struct SNPC
{
    public string prefabId;
    public SVec3 position;
    public SVec3 rotation;
    public int aiState;
    public bool hasDestination;
    public SVec3 npcDestination;
}