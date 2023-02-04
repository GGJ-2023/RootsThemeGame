using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingData data;
    public BuildingPreset preset;

    public virtual string GetCustomProperties()
    {
        return string.Empty;
    }
    public virtual void RecieveCustomProperties(string props)
    {

    }
}
