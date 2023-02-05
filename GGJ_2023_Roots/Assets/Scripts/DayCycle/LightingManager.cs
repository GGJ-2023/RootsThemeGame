using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;

    [SerializeField, Range(0, 120)] public float TimeOfDay;
    public float dayLength;
    public float counter;
    public float counterRate;
    public float startTime = 60f;

    public static LightingManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        counterRate = 120.0f / dayLength;
        counter = startTime;
        TimeOfDay = startTime;
    }

    private void Update()
    {
        if (Preset == null)
        {
            return;
        }
        if (Application.isPlaying)
        {

            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 120f;
            UpdateLighting(TimeOfDay / 120f);
            counter += counterRate * Time.deltaTime;
            if (counter >= 120f)
            {
                StartCoroutine(UpdateDay());
            }
        }
        else
        {
            UpdateLighting(TimeOfDay / 120f);
        }

        IEnumerator UpdateDay()
        {
            GameManager.instance.cannotBuildText.text = string.Format($"Its a new day!");

            counter = 0.0f;
            GameManager.instance.EndDay();

            yield return new WaitForSeconds(1f);

            GameManager.instance.cannotBuildText.text = null;
        }
    }
    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170, 0));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;
        if(RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
