using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { North, South, East, West };

public class Tile : MonoBehaviour
{
    public int index;

    public Tile northTile;
    public Tile southTile;
    public Tile eastTile;
    public Tile westTile;

    public Direction direction = Direction.North;

    public bool isRotatable = false;

    private GameObject player;
    private Tile nextTile;
    private int playerSteps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(GameObject gO, int steps)
    {
        Debug.Log("Index : " + index);
        if(steps > 0)
        {
            player = gO;
            playerSteps = steps;
            Vector3 nextPos = Vector3.zero;

            switch (direction)
            {
                case Direction.East:
                    nextTile = eastTile;
                    break;
                case Direction.North:
                    nextTile = northTile;
                    break;
                case Direction.South:
                    nextTile = southTile;
                    break;
                case Direction.West:
                    nextTile = westTile;
                    break;
                default:
                    nextTile = northTile;
                    Debug.LogError("Unknown direction");
                    break;
            }

            nextPos = nextTile.transform.position;
            nextPos.y = gO.transform.position.y;
            
            iTween.MoveTo(player, iTween.Hash("position", nextPos, "time", 2, "easetype", iTween.EaseType.linear, "onComplete", "OnCompleteMove",
    "onCompleteTarget", gameObject));
        }
    }


    void OnCompleteMove()
    {
        //Debug.Log("On Complete Move");

        if (playerSteps == 1)
        {
            player.GetComponent<Player>().UpdateCurrentTile(nextTile);
            return;
        }
        nextTile.Move(player, playerSteps - 1);
    }
}
