using UnityEngine;

class DayNightManager : MonoBehaviour
{
    [SerializeField] private float CycleSpeed = 1f;
    [SerializeField] private float TimeOfDay = 12;

    [SerializeField] private DayNightPresets Presets;

    static private DayNightManager Instance;

    private Color originalSkyboxTint;
    
    private void Awake()
    {
        if(Instance != null) Destroy(this);
        Instance = this;
    }

    void Start(){
        originalSkyboxTint = RenderSettings.skybox.GetColor("_Tint");

        if (RenderSettings.sun == null)
        {
            var lights = FindObjectsOfType<Light>();
            foreach (var l in lights)
            {
                if (l.type  == LightType.Directional)
                {
                    RenderSettings.sun = l;
                    break;
                }
            }
        }
    }

    void OnDestroy(){
        RenderSettings.skybox.SetColor("_Tint",  originalSkyboxTint);
    }

    void Update(){
        TimeOfDay += Time.deltaTime * CycleSpeed;
        TimeOfDay %= 24;
        var dayPercent = TimeOfDay / 24f;

        RenderSettings.skybox.SetColor("_Tint",  Presets.TintColor.Evaluate(dayPercent));
        RenderSettings.ambientLight = Presets.FogColor.Evaluate(dayPercent);
        RenderSettings.fogColor = Presets.AmbientColor.Evaluate(dayPercent);
        RenderSettings.sun.color = Presets.DirectionalColor.Evaluate(dayPercent);
        RenderSettings.sun.transform.rotation = Quaternion.Euler((360 * dayPercent) - 90, 0, 0);
    }
}