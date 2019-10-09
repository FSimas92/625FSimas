using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TerrainGenerator : MonoBehaviour
{
    [RangeAttribute(1f, 20f)]
    public float flatness = 1f;
    [RangeAttribute(1f, 20f)]
    public float frequency = 1f;
    [RangeAttribute(1, 10)]
    public int octaves = 8;

    Texture2D image;
    Terrain terrain;

    float offset1 = 0f;
    float offset2 = 0f;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        image = new Texture2D(terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight);
        image.LoadImage(File.ReadAllBytes("Assets/Height Maps/lucas-donderis.jpeg"));
    }

    // Update is called once per frame
    void Update()
    {
        offset1 += (Input.GetAxis("Horizontal") / 4);
        offset2 += (Input.GetAxis("Vertical") / 4);

        float[,] heightmap = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight);
        
        for (int i=0; i < terrain.terrainData.heightmapHeight; ++i)
        {
            for (int j=0; j < terrain.terrainData.heightmapWidth; ++j)
            {
                float x = j / (float) terrain.terrainData.heightmapWidth;
                float y = i / (float) terrain.terrainData.heightmapHeight;
                float height = image.GetPixel(i, j).b;

                
                float current_frequency = frequency;
                float amplitude = 1;
                for (int z = 0; z < octaves; ++z)
                {
                    height = height + Mathf.PerlinNoise(x * current_frequency + offset1, y * current_frequency + offset2) * amplitude;
                    amplitude /= 2;
                    current_frequency *= 2;
                }
                

                heightmap[i, j] = height / flatness;
                
            }
        }

        terrain.terrainData.SetHeights(0, 0, heightmap);
    }
}
