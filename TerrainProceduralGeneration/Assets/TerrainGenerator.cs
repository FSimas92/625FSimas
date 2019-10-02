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

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        image = new Texture2D(terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight);
        image.LoadImage(File.ReadAllBytes("Assets/mt-taranaki.png"));
    }

    // Update is called once per frame
    void Update()
    {
        
        float[,] heightmap = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight);
        
        for (int i=0; i < terrain.terrainData.heightmapHeight; ++i)
        {
            for (int j=0; j < terrain.terrainData.heightmapWidth; ++j)
            {
                float x = j / (float) terrain.terrainData.heightmapWidth;
                float y = i / (float) terrain.terrainData.heightmapHeight;
                float height = image.GetPixel(i, j).b;

                /* Perlin noise version
                float current_frequency = frequency;
                float height = 0f;
                float amplitude = 1;
                for (int z = 0; z < octaves; ++z)
                {
                    height = height + Mathf.PerlinNoise(x * current_frequency, y * current_frequency) * amplitude;
                    current_frequency *= 2;
                }
                

                float heightP = Mathf.PerlinNoise(x * frequency, y * flatness);
                heightmap[i, j] = height / flatness + Random.Range(0f, 0.1f);
                */
            }
        }

        terrain.terrainData.SetHeights(0, 0, heightmap);
    }
}
