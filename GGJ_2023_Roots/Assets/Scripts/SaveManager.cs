using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class SaveManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadGame());
    }
    IEnumerator LoadGame()
    {
        yield return new WaitForEndOfFrame();
        if (PlayerPrefs.HasKey("Save"))
            Load();
    }
    private void Update()
    {
        /*
          //used with the input system but breaks game atm
        if (Keyboard.current.nKey.wasPressedThisFrame)
            Save();
        if (Keyboard.current.nKey.wasPressedThisFrame)
            Load();
        */
    }
    void Save()
    {
        SaveData data = new SaveData();

        // Camera position
        data.cameraPos = new SVec3(CameraController.instance.transform.position);
        data.cameraRot = new SVec3(CameraController.instance.transform.eulerAngles);
        

        data.nutrients = GameManager.instance.nutrients;
        data.food = GameManager.instance.curFood;
        data.water = GameManager.instance.curWater;
        data.curPopulation = GameManager.instance.curPopulation;
        data.curJobs = GameManager.instance.curJobs;
        data.maxJobs = GameManager.instance.maxJobs;
        data.maxPopulation = GameManager.instance.maxPopulation;

        // buildings
        Building[] buildingObjects = FindObjectsOfType<Building>();
        data.buildings = new SBuilding[buildingObjects.Length];

        for (int x = 0; x < buildingObjects.Length; x++)
        {
            data.buildings[x] = new SBuilding();
            data.buildings[x].buildingId = buildingObjects[x].data.id;
            data.buildings[x].position = new SVec3(buildingObjects[x].transform.position);
            data.buildings[x].rotation = new SVec3(buildingObjects[x].transform.eulerAngles);
        }

        //NPCs
        NPC[] npcs = FindObjectsOfType<NPC>();
        data.npcs = new SNPC[npcs.Length];
        for (int x = 0; x < npcs.Length; x++)
        {
            data.npcs[x] = new SNPC();
            data.npcs[x].prefabId = npcs[x].data.id;
            data.npcs[x].position = new SVec3(npcs[x].transform.position);
            data.npcs[x].rotation = new SVec3(npcs[x].transform.eulerAngles);
            //data.npcs[x].aiState = (int)npcs[x].aiState;
            //data.npcs[x].hasAgentDestination = !npcs[x].agent.isStopped;
            //data.npcs[x].agentDestination = new SVec3(npcs[x].agent.destination);

            //time of day
            data.timeOfDay = LightingManager.instance.TimeOfDay;

            string rawData = JsonUtility.ToJson(data);

            PlayerPrefs.SetString("Save", rawData);
        }
    }
    void Load()
    {

    }
}
