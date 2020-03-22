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
        //StartCoroutine(WaitTillNextScene());
    }

    public void checkBoard()
    {
        StartCoroutine(DecreaseRowCo());
    }

    //If there are any empty spaces in the middle of the board, move blocks down to fill it
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
        
        yield return new WaitForSeconds(.3f);
        StartCoroutine(FillBoardCo());
    }

    public IEnumerator FillBoardCo()
    {
        RefillBoard();
        yield return new WaitForSeconds(.3f);
    }

    //Refill empty spaces at the top of the board with new blocks
    public void RefillBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allBlocks[i,j] == null)
                {
                    Vector2 tempPosition = new Vector2(i, j);
                    int blockToUse = Random.Range(0, blocks.Length);
                    GameObject piece = Instantiate(blocks[blockToUse], tempPosition, Quaternion.identity);
                    allBlocks[i, j] = piece;
                }
            }
        }
    }



}
