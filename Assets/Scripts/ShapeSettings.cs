﻿using UnityEngine;

[CreateAssetMenu(fileName = "ShapeSettings", menuName = "ShapeSettings", order = 0)]
public class ShapeSettings : ScriptableObject
{
    public float planetRadius = 1;
    public NoiseLayer[] noiseLayers;

    [System.Serializable]
    public class NoiseLayer
    {
        public bool enabled = true;
        public bool useFirstLayerAsMask;
        public NoiseSettings noiseSettings;
    }
}