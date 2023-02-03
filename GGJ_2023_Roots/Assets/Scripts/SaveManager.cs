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
        //if (Keyboard.current.nKey.wasPressedThisFrame)
       // {

       // }
    }
    void Save()
    {

    }
    void Load()
    {

    }
}
