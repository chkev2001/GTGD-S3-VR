using UnityEngine;
using System.Collections;
using System.Linq;

public class Terrain_Painter : MonoBehaviour {

	private TerrainData myTerrainData;
	private float[,,] map;
	//private int numberOfTextures = 2;
	private float normX;
	private float normY;
	private float angle;
	private float fraction;
    private float height;
	private int controlTextureResolution;
   // private bool isSampled;
    private float[] splatWeights;
    public float sandHeightLimit = 5;
    public float grassHeightLimit = 40;
    public float grassyRockHeightStart = 25;
    public float grassSlopeLimit = 45;

	// Use this for initialization
	void Start () 
	{
		SetInitialReferences();
		PaintTerrain();
	}
	
	void SetInitialReferences ()
	{
		myTerrainData = GetComponent<Terrain>().terrainData;
		controlTextureResolution = myTerrainData.alphamapWidth;
        splatWeights = new float[myTerrainData.alphamapLayers];

        map = new float[controlTextureResolution, controlTextureResolution, myTerrainData.alphamapLayers];
        //Debug.Log(myTerrainData.alphamapWidth.ToString());
        //Debug.Log(myTerrainData.alphamapHeight.ToString());
    }

    void PaintTerrain()
    {
        for (int y = 0; y < controlTextureResolution; y++)
        {
            for (int x = 0; x < controlTextureResolution; x++)
            {
                normX = x * 1.0f / (controlTextureResolution - 1.0f);
                normY = y * 1.0f / (controlTextureResolution - 1.0f);

                angle = myTerrainData.GetSteepness(normX, normY);

                height = myTerrainData.GetHeight(x, y);

                fraction = angle / 90.0f;

                if(height < sandHeightLimit)
                {
                    splatWeights[0] = 1.5f;
                    //splatWeights[1] = 0.5f;
                }
                else
                {
                    splatWeights[0] = 1 - (height / myTerrainData.heightmapHeight);
                }

                //if(height > 5 && height < 40)
                if (height > sandHeightLimit && height < grassHeightLimit && angle < grassSlopeLimit)
                {
                    splatWeights[1] = 2.5f - (height / myTerrainData.heightmapHeight);
                }

                if (height > grassyRockHeightStart)
                {
                    splatWeights[2] = 1 - fraction;
                }

                //if (!isSampled)
                //{
                //    isSampled = true;
                //    Debug.Log(splatWeights[1].ToString());
                //}

                splatWeights[3] = fraction;

                float z = splatWeights.Sum();

                for(int i = 0; i < myTerrainData.alphamapLayers; i++)
                {
                    splatWeights[i] /= z;
                    map[y, x, i] = splatWeights[i];
                }

            }
        }
        myTerrainData.SetAlphamaps(0, 0, map);
    }
}
