using UnityEngine;

public class NoiseFilter
{
    private readonly Noise _noise = new Noise();
    private readonly NoiseSettings _noiseSettings;

    public NoiseFilter(NoiseSettings noiseSettings)
    {
        _noiseSettings = noiseSettings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        var frequency = _noiseSettings.baseRoughness;
        float amplitude = 1;
        
        for (var i = 0; i < _noiseSettings.numberOfLayers; i++)
        {
            var v = _noise.Evaluate(point * frequency + _noiseSettings.centre);
            noiseValue += (v + 1) * 0.5f * amplitude;
            frequency *= _noiseSettings.roughness;
            amplitude *= _noiseSettings.persistence;
        }

        noiseValue = Mathf.Max(0, noiseValue - _noiseSettings.minValue);
        return noiseValue * _noiseSettings.strength;
    }
}