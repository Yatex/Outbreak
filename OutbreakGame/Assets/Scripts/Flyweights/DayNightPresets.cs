using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayNightPresets", menuName = "presets/DayNightPresets", order = 0)]
public class DayNightPresets : ScriptableObject
{
    [SerializeField] private DayNightPresetsValues _Values;

    public Gradient FogColor => _Values.FogColor;
    public Gradient AmbientColor => _Values.AmbientColor;
    public Gradient DirectionalColor => _Values.DirectionalColor;
    public Gradient TintColor => _Values.TintColor;
}

[System.Serializable]
public struct DayNightPresetsValues
{
    public Gradient FogColor;
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient TintColor;

    
}