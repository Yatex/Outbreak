using UnityEngine;

class DayNightManager : MonoBehaviour
{
    public float CycleSpeed = 1f;
    private float TimeOfDay = 12;


    public float MinExposure = 0.25f;
    public float MaxExposure = 0.5f;

    private Light Sun;
    static private DayNightManager Instance;
    
    private void Awake()
    {
        if(Instance != null) Destroy(this);
        Instance = this;
    }

    void Start(){
        var lights = FindObjectsOfType<Light>();
        foreach (var l in lights)
        {
            if (l.type  == LightType.Directional)
            {
                this.Sun = l;
                break;
            }
        }
    }

    void Update(){
        TimeOfDay += CycleSpeed * Time.deltaTime;
        TimeOfDay = TimeOfDay % 24;
        if (this.Sun != null)
        {
            this.Sun.transform.rotation = Quaternion.Euler(90 * (1 + (TimeOfDay - 12) / 12),0,0);

            var percentageToDayPeek = 1 - Mathf.Abs(TimeOfDay - 12) / 12;
            var exposure = percentageToDayPeek * (MaxExposure - MinExposure) + MinExposure;
            RenderSettings.skybox.SetFloat("_Exposure",  exposure);
        }
    }
}