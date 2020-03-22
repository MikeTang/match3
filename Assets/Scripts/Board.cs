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
                int blockToUse = Random.Range(0, 4);
                GameObject block = Instantiate(blocks[blockToUse], tempPosition, Quaternion.identity);
		    	block.transform.parent = this.transform;
		    	block.name = "block (" + i + "," + j + ")";
		    	allBlocks[i,j] = block;
    		}
    	}
    }

    public IEnumerator DecreaseRowCo(){

        int nullCount = 0;
        for (int i=0; i < width; i++){
            for (int j = 0; j < height; j ++){
                if(allBlocks[i,j] == null){
                    nullCount++;
                }else if(nullCount > 0){
                    allBlocks[i,j].GetComponent<Block>().y_pos -= nullCount;
                    allBlocks[i,j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);
    }
}
