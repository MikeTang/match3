using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int x_pos;
    public int y_pos;

    public int targetX;
    public int targetY;

    private Board board;
    private Vector2 firstTouchPosition;
    private Vector2 tempPosition;
    private List<GameObject> blocksToDestroy = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();

        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        x_pos = targetX;
        y_pos = targetY;

    }

    // Update is called once per frame
    void Update()
    {
        targetX = x_pos;
        targetY = y_pos;
        if (Mathf.Abs(targetX - transform.position.x) > .1){
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .2f);
            if(board.allBlocks[x_pos, y_pos] != this.gameObject){
                board.allBlocks[x_pos, y_pos] = this.gameObject;
            }
        }else{
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;

        }if (Mathf.Abs(targetY - transform.position.y) > .1){
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .2f);
            if(board.allBlocks[x_pos, y_pos] != this.gameObject){
                board.allBlocks[x_pos, y_pos] = this.gameObject;
            }
        }else{
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
        }
    }

    private void OnMouseDown() {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //DestroySameColor();
        int count = 0;
        findBlocksToDestroyRecursive(0, ref count, this.gameObject);
        // Debug.Log(blocksToDestroy.Count);
        foreach (GameObject blockToDestroy in blocksToDestroy)
        {
            if (blockToDestroy != null)
            {
                int block_x = (int)blockToDestroy.transform.position.x;
                int block_y = (int)blockToDestroy.transform.position.y;
                Destroy(blockToDestroy);
                board.allBlocks[block_x, block_y] = null;
            }
        }
        StartCoroutine(board.DecreaseRowCo());

    }

    public List<GameObject> findConnectedBlocks(GameObject block)
    {
        List<GameObject> connectedBlocks = new List<GameObject>();

        //offsets for the neighboring blocks (left, up, right, down)
        int[] x_offset = { -1, 0, 1, 0 };
        int[] y_offset = { 0, 1, 0, -1 };

        //check if each of the neighboring blocks is the same color
        for (int i = 0; i < x_offset.Length; i++)
        {
            int neighborX = (int)block.transform.position.x + x_offset[i];
            int neighborY = (int)block.transform.position.y + y_offset[i];

            if (neighborX < board.width && neighborY < board.height && neighborX >= 0 && neighborY >= 0)
            {
                GameObject neighbor = board.allBlocks[neighborX, neighborY];

                if (neighbor != null && neighbor.tag == this.tag && !blocksToDestroy.Contains(block))
                {

                    connectedBlocks.Add(neighbor);
                }
            }
        }
        return connectedBlocks;
    }

    public int findBlocksToDestroyRecursive(int value, ref int count, GameObject block) {
        //look for neighboring blocks and check if they're the same color and recursively
        //check for those blocks' neighors

        count++;
        if (value >= 50) //avoid infinite loop
        {
            return value;
        }
        //Find neighboring blocks of the same color
        List<GameObject> connectedBlocks = findConnectedBlocks(block);

        //Add current block to the list to be destroyed
        if (!blocksToDestroy.Contains(block))
        {
            blocksToDestroy.Add(block);
        }
        //if no neighboring blocks found, quit loop, otherwise, find their neighboring blocks
        if (connectedBlocks.Count == 0)
        {
            return value;
        }
        else
        {
            foreach (GameObject connectBlock in connectedBlocks)
            {
                findBlocksToDestroyRecursive(value + 1, ref count, connectBlock);
            }
            return value;
        }
    }
}
