using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Collider border;
    public bool canPlace;
    public MeshRenderer Ground;
    public float GroundBorderWidth = 2f;
    

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

            canPlace = true;
            var nb = new Bounds(Ground.bounds.center, new Vector3(Ground.bounds.size.x - GroundBorderWidth, 0f, Ground.bounds.size.z - GroundBorderWidth));
            if (!nb.Contains(curIndicatorPos))
            {
                canPlace = false;
                return;
            }

            if (currentlyPlacing)
                placementInd.transform.position = curIndicatorPos;
            else if (currentlyDestroying)
                DestroyInd.transform.position = curIndicatorPos;
        }

        if (Input.GetMouseButtonDown(0) && currentlyPlacing && canPlace)
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

            if (GameManager.instance.buildings.Find(x => x.transform.position == curIndicatorPos) != null)
            {
                // do something to indicate to the player that they cannot place a building here!
                StartCoroutine(CannotPlaceBuildingHere());
                Debug.LogWarning($"You cannot place a building at {curIndicatorPos} !!");
                return;
            }

            GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curIndicatorPos, Quaternion.identity);
            GameManager.instance.OnPlaceBuilding(buildingObj.GetComponent<Building>());

            IEnumerator CannotPlaceBuildingHere()
            {
                GameManager.instance.cannotBuildText.text = string.Format($"You cannot place a building here!!");

                yield return new WaitForSeconds(1f);

                GameManager.instance.cannotBuildText.text = null;
            }

    }

    void DestroyBuilding()
    {
        Building buildingToDestroy = GameManager.instance.buildings.Find(x => x.transform.position == curIndicatorPos);

        if(buildingToDestroy != null)
        {
            GameManager.instance.OnRemoveBuilding(buildingToDestroy);
        }
    }
}
