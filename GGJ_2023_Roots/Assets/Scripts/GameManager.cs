using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public int nutrients;
    public int day;
    public int curPopulation;
    public int curFood;
    public int curJobs;
    public int curWater;
    public int maxPopulation;
    public int maxJobs;
    public int nutrientsIncome;
    public int foodIncome;
    public int waterIncome;

    public TextMeshProUGUI cannotBuildText;
    public TextMeshProUGUI statsText;

    public List<Building> buildings = new List<Building>();
    public static GameManager instance;
    
    public bool menuShowing = false;

    public GameObject buildMenu;
    
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        UpdateStatText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && PauseMenu.GameIsPaused == false)
        {
            BuildMenu();
        }
    }

    public void OnPlaceBuilding(Building building)
    {
        AudioManager.instance.PlaceSound();

            nutrients -= building.preset.cost;

            maxPopulation += building.preset.population;
            maxJobs += building.preset.jobs;
            buildings.Add(building);

            UpdateStatText();



    }

    public void OnRemoveBuilding(Building building)
    {
        AudioManager.instance.DestroySound();
        nutrients += ((1 / 2) * building.preset.cost);
        maxPopulation -= building.preset.population;
        maxJobs -= building.preset.jobs;
        buildings.Remove(building);

        Destroy(building.gameObject);

        UpdateStatText();
    }

    void UpdateStatText()
    {
        statsText.text = string.Format("Day: {0}   Nutrients: {1}   Water: {2}   Food: {3}   Pop: {4} / {5}   Jobs: {6} / {7}", 
            new object[8] { day, nutrients, curWater, curFood, curPopulation, maxPopulation, curJobs, maxJobs } );
    }

    public void EndDay()
    {
        LightingManager.instance.TimeOfDay = 120f;
        day++;

        CalculateJobs();
        CalculatePopulation();
        CalculateResources();
        UpdateStatText();
    }

    void CalculateResources()
    {

        foreach(Building building in buildings)
        {
            nutrients -= building.preset.costPerTurn;
            curFood += building.preset.food;
            curWater += building.preset.water;
            nutrients += building.preset.nutrients;
        }
            

    }
    void CalculateJobs()
    {
        curJobs = Mathf.Min(curPopulation, maxJobs);
    }
    void CalculatePopulation()
    {
        if (curFood >= curPopulation && curPopulation < maxPopulation)
        {
            curFood -= curPopulation / 4;
            curPopulation = Mathf.Min(curPopulation + (curFood / 4), maxPopulation);
        }
        else if (curFood < curPopulation)
            curPopulation = curFood;

    }
    public void DoubleSpeedUp()
    {
        Time.timeScale = 4f;
    }
    public void TripleSpeedUp()
    {
        Time.timeScale = 8f;
    }
    public void NormalSpeed()
    {
        Time.timeScale = 1f;
    }

    public void BuildMenu()
    {
        if(buildMenu != null)
        {
            Animator anim = buildMenu.GetComponent<Animator>();
            if(anim != null)
            {
                bool isOpen = anim.GetBool("Open");
                anim.SetBool("Open", !isOpen);
            }
        }
    }


}
