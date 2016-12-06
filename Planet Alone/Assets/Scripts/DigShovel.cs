using UnityEngine;
using System.Collections;

public class DigShovel : MonoBehaviour
{

    public Terrain TerrainMain;
    /*
    private void OnCollisionEnter(Collider collider)
    {
        int xRes = TerrainMain.terrainData.heightmapWidth;
        int yRes = TerrainMain.terrainData.heightmapHeight;

        int xBase = 0;
        int yBase = 0;

        float[,] heights = TerrainMain.terrainData.GetHeights(xBase, yBase, xRes, yRes);

        Debug.Log(" heights lenght "+heights.Length);
        heights[10, 10] = 1f;
        heights[20, 20] = 0.5f;

        TerrainMain.terrainData.SetHeights(xBase, yBase, heights);

    }
    */
}
