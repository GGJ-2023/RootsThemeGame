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
    public void Save()
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
        data.day = GameManager.instance.day;

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
    public void Load()
    {
        SaveData data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("Save"));

        CameraController.instance.transform.position = data.cameraPos.GetVector3();
        CameraController.instance.transform.eulerAngles = data.cameraRot.GetVector3();

        //Stats
        GameManager.instance.nutrients = (int)data.nutrients;
        GameManager.instance.curFood = (int)data.food;
        GameManager.instance.curWater = (int)data.water;
        GameManager.instance.curPopulation = (int)data.curPopulation;
        GameManager.instance.curJobs = (int)data.curJobs;
        GameManager.instance.maxJobs= (int)data.maxJobs;
        GameManager.instance.maxPopulation = (int)data.maxPopulation;
        GameManager.instance.day = (int)data.day;

        // buildings
        for (int x = 0; x < data.buildings.Length; x++)
        {
            GameObject prefab = ObjectManager.instance.GetBuildingByID(data.buildings[x].buildingId).spawnPrefab;
            GameObject building = Instantiate(prefab, data.buildings[x].position.GetVector3(), Quaternion.Euler(data.buildings[x].rotation.GetVector3()));
            //building.GetComponent<Building>().ReceiveCustomProperties(data.buildings[x].customProperties);
        }

        // destroy all pre existing NPCs
        NPC[] npcs = FindObjectsOfType<NPC>();
        for (int x = 0; x < npcs.Length; x++)
            Destroy(npcs[x].gameObject);
        // spawn in saved NPCs
        for (int x = 0; x < data.npcs.Length; x++)
        {
            GameObject prefab = ObjectManager.instance.GetNPCByID(data.npcs[x].prefabId).spawnPrefab;
            GameObject npcObj = Instantiate(prefab, data.npcs[x].position.GetVector3(), Quaternion.Euler(data.npcs[x].rotation.GetVector3()));

            NPC npc = npcObj.GetComponent<NPC>();
            //npc.aiState = (AIState)data.npcs[x].aiState;
            //npc.agent.isStopped = !data.npcs[x].hasAgentDestination;
            //if (!npc.agent.isStopped)
            //npc.agent.SetDestination(data.npcs[x].agentDestination.GetVector3());

            LightingManager.instance.TimeOfDay = data.timeOfDay;
        }
    }
}
