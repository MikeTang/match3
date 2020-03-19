using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int column;
    public int row;

    private Board board;
    private Vector2 firstTouchPosition;


    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        column = (int)transform.position.x;
        row = (int)transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown(){
    	firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    	Debug.Log(firstTouchPosition);
    }
}
