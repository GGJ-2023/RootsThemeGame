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
    public int nutrientsIncome = 10;
    public int taxes;
    public int nutrientCost;

    public TextMeshProUGUI cannotBuildText;
    public TextMeshProUGUI startText; 
    public TextMeshProUGUI statsText;

    public List<Building> buildings = new List<Building>();
    public static GameManager instance;
    
    public bool s_menuShowing = false;
    public bool b_menuShowing = false;

    public GameObject buildMenu;
    public GameObject speedMenu;
    
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        startText.text = string.Format($"Press B or V to open a menu");
        StartBuildingHere();
        
        if (statsText != null)
        {
            UpdateStatText();
        }

        IEnumerator StartBuildingHere()
        {
            startText.text = string.Format($"Press B or V to open a menu");

            yield return new WaitForSeconds(5f);

            startText.text = null;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && PauseMenu.GameIsPaused == false)
        {
            startText.text = null;
            BuildMenu();
        }
        if (Input.GetKeyDown(KeyCode.V) && PauseMenu.GameIsPaused == false)
        {
            startText.text = null;
            SpeedMenu();
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
        statsText.text = string.Format("Day: {0}   Nutrients: {1}   Water: {2}   Food: {3}   Pop: {4} / {5}", 
            new object[6] { day, nutrients, curWater, curFood, curPopulation, maxPopulation,  } );
    }

    public void EndDay()
    {
        
        LightingManager.instance.TimeOfDay = 120f;
        LightingManager.instance.counter = 0f;
        day++;
        CalculatePopulation();
        CalculateResources();
              
        
        if (nutrients < 0)
            nutrients = 0;

        if (curWater < 0)
            curWater = 0;

        if (curFood < 0)
            curFood = 0;
        if (curPopulation < 0)
        {
            curPopulation = 0;
        }
        UpdateStatText();
    }

    void CalculateResources()
    {
        nutrients += nutrientsIncome + curPopulation/2;

        foreach (Building building in buildings)
        {
            curFood += building.preset.food;
            curWater += building.preset.water;
            nutrients -= building.preset.costPerTurn;
            nutrients += building.preset.nutrients + curPopulation/4;
        }
        if (nutrients < 0)        
            nutrients = 0;
        
        if (curWater < 0)        
            curWater = 0;
        
        if (curFood < 0)
            curFood = 0;
            

    }
    void CalculateJobs()
    {
        curJobs = Mathf.Min(curPopulation, maxJobs);
    }
    void CalculatePopulation()
    {
        
        if (curFood >= curPopulation && curPopulation <= maxPopulation)
        {

            curFood -= curPopulation * 4;
            curWater -= curPopulation  * 2;

            curPopulation = Mathf.Min(curPopulation + (curFood / 8) + (curWater / 6), maxPopulation);
        }
        else if (curFood < curPopulation)
            curPopulation = curFood;
        if (curPopulation < 0)
        {
            curPopulation = 0;
        }
        if (maxPopulation < curPopulation)
        {
            curPopulation = maxPopulation;
        }



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
        if (b_menuShowing == false)
        {
            b_menuShowing = true;
        }
        else if (b_menuShowing == true)
            b_menuShowing = false;
        if (buildMenu != null)
        {
            Animator anim = buildMenu.GetComponent<Animator>();
            if(anim != null)
            {
                bool isOpen = anim.GetBool("Open");
                anim.SetBool("Open", !isOpen);
            }
        }
    }
    public void SpeedMenu()
    {
        if (s_menuShowing == false)
        {
            s_menuShowing = true;
        }
        else if (s_menuShowing == true)
            s_menuShowing = false;

        if (speedMenu != null)
        {
            Animator anim = speedMenu.GetComponent<Animator>();
            if (anim != null)
            {
                bool isOpen = anim.GetBool("Open");
                anim.SetBool("Open", !isOpen);
            }
            
        }
    }


}
