using UnityEngine;
using System.Collections;

public class Terrain_Collider : MonoBehaviour {

    public Terrain TerrainMain;

    void OnCollisionEnter(Collision collision)
    {
        //Vector3 closestPoint = collision.collider.ClosestPointOnBounds(explosionPos);
        ContactPoint contact = collision.contacts[0];
        Vector3 pos = contact.point;
        Vector3 pos_world = transform.InverseTransformPoint(pos);
        
        if (collision.gameObject.CompareTag("Shovel"))
        {
            Debug.Log("COllision with shovel!!!!!!");

            int xRes = TerrainMain.terrainData.heightmapWidth;
            int yRes = TerrainMain.terrainData.heightmapHeight;

            int xBase = 0;
            int yBase = 0;

            float[,] heights = TerrainMain.terrainData.GetHeights(xBase, yBase, xRes, yRes);

            //Debug.Log(" heights lenght " + heights.Length);
            Debug.Log("pos.x = " + (int)pos_world.x + ",  pos.z = "+ (int)pos_world.z);
            heights[(int)pos_world.x, (int)pos_world.y] = 1f;
            //heights[20, 20] = 0.5f;

            TerrainMain.terrainData.SetHeights(xBase, yBase, heights);
        }
    }
}
