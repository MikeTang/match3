using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour{

	public int width;
	public int height;
	public GameObject tilePrefab;
	public GameObject[] blocks;
	private BackgroundTile[,] allTiles;
	public GameObject[,] allBlocks;

    // Start is called before the first frame update
    void Start(){
        allTiles = new BackgroundTile[width, height];
        allBlocks = new GameObject[width, height];
        SetUp();
    }

    private void SetUp(){
    	for (int i = 0; i < width; i ++){
    		for (int j = 0; j < height; j ++){
    			Vector2 tempPosition = new Vector2(i,j);

                //int blockToUse = Random.Range(0, blocks.Length);
                int blockToUse = Random.Range(0, 6);
                GameObject block = Instantiate(blocks[blockToUse], tempPosition, Quaternion.identity);
		    	block.transform.parent = this.transform;
		    	block.name = "block (" + i + "," + j + ")";
		    	allBlocks[i,j] = block;
    		}
    	}
    }
}
