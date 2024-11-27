using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaves : MonoBehaviour
{
    //script isn't used and not done for what i wnated it to do, but it used perlin noise to create a wave effect
    public float waveScale = 0.3f;
    public float waveSpeed = 0.5f;
    public float noiseStrength = 0.1f;
    public float foamThreshold = 0.2f;

    Vector3[] baseVerticies;
    Mesh mesh;
    Color[] colors;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVerticies = mesh.vertices;

        colors = new Color[baseVerticies.Length];
    }


    void Update()
    {
        Vector3[] verticies = new Vector3[baseVerticies.Length];

        for (int i = 0; i < verticies.Length; i++)
        {
            Vector3 baseVertex = baseVerticies[i];

            float noise = Mathf.PerlinNoise(
                (baseVertex.x + Time.time * waveSpeed) * waveScale,
                (baseVertex.z + Time.time * waveSpeed) * waveScale
            );

            verticies[i] = baseVertex + new Vector3(0, noise * noiseStrength, 0);

            colors[i] = noise > foamThreshold ? Color.white : Color.blue; // couldn't figure out the color of the 'foam' of the water so switched to shaders
        }

        mesh.vertices = verticies;
        mesh.colors = colors;
        mesh.RecalculateNormals();
    }
}
