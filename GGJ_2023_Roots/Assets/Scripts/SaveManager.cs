using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        if (Keyboard.current.nKey.wasPressedThisFrame)
            Save();
        if (Keyboard.current.nKey.wasPressedThisFrame)
            Load();

    }
    void Save()
    {
        SaveData data = new SaveData();

        // Camera position
        //data.cameraPos = new SVec3(CameraController.instance.transform.position);
        //data.cameraRot = new SVec3(CameraController.instance.transform.eulerAngles);
        //data.cameraLook = new SVec3(CameraController.instance.cameraContainer.localEularAngles);

        data.nutrients = GameManager.instance.nutrients;
        data.food = GameManager.instance.curFood;
        data.water = GameManager.instance.curWater;
        data.curPopulation = GameManager.instance.curPopulation;
        data.curJobs = GameManager.instance.curJobs;
        data.maxJobs = GameManager.instance.maxJobs;
        data.maxPopulation = GameManager.instance.maxPopulation;

    }
    void Load()
    {

    }
}
