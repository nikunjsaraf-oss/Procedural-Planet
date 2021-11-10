using UnityEngine;

public class ShapeGenerator
{
    private readonly ShapeSettings _shapeSettings;
    private readonly NoiseFilter[] _noiseFilters;

    public ShapeGenerator(ShapeSettings shapeSettings)
    {
        _shapeSettings = shapeSettings;
        _noiseFilters = new NoiseFilter[_shapeSettings.noiseLayers.Length];
        for (int i = 0; i < _noiseFilters.Length; i++)
        {
            _noiseFilters[i] = new NoiseFilter(_shapeSettings.noiseLayers[i].noiseSettings);
        }
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float firstLayerValue = 0;
        float elevation = 0;

        if (_noiseFilters.Length > 0)
        {
            firstLayerValue = _noiseFilters[0].Evaluate(pointOnUnitSphere);
            if (_shapeSettings.noiseLayers[0].enabled)
            {
                elevation = firstLayerValue;
            }
        }

        for (var i = 1; i < _noiseFilters.Length; i++)
        {
            if (_shapeSettings.noiseLayers[i].enabled)
            {
                float mask = (_shapeSettings.noiseLayers[i].useFirstLayerAsMask ? firstLayerValue : 1);
                elevation += _noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
            }
        }

        return pointOnUnitSphere * (_shapeSettings.planetRadius * (1 + elevation));
    }
}