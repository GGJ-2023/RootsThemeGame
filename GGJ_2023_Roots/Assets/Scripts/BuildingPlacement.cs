using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlacing;
    private bool currentlyDestroying;

    private BuildingPreset curBuildingPreset;

    private float indicatorUpdateRate = 0.05f;
    private float lastUpdateRate;
    private Vector3 curIndicatorPos;

    public GameObject placementInd;
    public GameObject DestroyInd;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelPlacement();
            if (currentlyDestroying == true)
                currentlyDestroying = !currentlyDestroying;
                DestroyInd.SetActive(currentlyDestroying);
        }
            

        if (Time.time - lastUpdateRate>indicatorUpdateRate)
        {
            lastUpdateRate = Time.time;
            curIndicatorPos = Selector.instance.GetCurTilePos();

            if (currentlyPlacing)
                placementInd.transform.position = curIndicatorPos;
            else if (currentlyDestroying)
                DestroyInd.transform.position = curIndicatorPos;
        }

        if (Input.GetMouseButtonDown(0) && currentlyPlacing)
            PlaceBuilding();
        else if (Input.GetMouseButtonDown(0) && currentlyDestroying)
            DestroyBuilding();
    }
    public void BeginNewPlacement(BuildingPreset preset)
    {
        if (currentlyDestroying == true)
            currentlyDestroying = !currentlyDestroying;
        //money check


        currentlyPlacing = true;
        curBuildingPreset = preset;
        placementInd.SetActive(true);
        placementInd.transform.position = new Vector3(0, -99, 0);
    }
    void CancelPlacement()
    {
        currentlyPlacing = false;
        placementInd.SetActive(false);
    }

    public void ToggleDestroy()
    {
        //disables placement
        currentlyPlacing = false;
        placementInd.SetActive(false);

        //enables destroy
        DestroyInd.transform.position = new Vector3(0, -99, 0);
        currentlyDestroying = !currentlyDestroying;
        DestroyInd.SetActive(currentlyDestroying);
    }

    void PlaceBuilding()
    {
        GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curIndicatorPos, Quaternion.identity);
        // tell city script?

        CancelPlacement();
    }

    void DestroyBuilding()
    {

    }
}
