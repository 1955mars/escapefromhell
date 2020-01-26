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

    public Direction currentDirection;
    public Direction[] possibleDirections;
    public Tile[] possibleTiles;

    public bool isRotatable = false;

    private GameObject player;
    public Tile nextTile;
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

            //switch (currentDirection)
            //{
            //    case Direction.East:
            //        nextTile = eastTile;
            //        break;
            //    case Direction.North:
            //        nextTile = northTile;
            //        break;
            //    case Direction.South:
            //        nextTile = southTile;
            //        break;
            //    case Direction.West:
            //        nextTile = westTile;
            //        break;
            //    default:
            //        nextTile = northTile;
            //        Debug.LogError("Unknown direction");
            //        break;
            //}

            nextPos = nextTile.transform.position;
            nextPos.y = gO.transform.position.y;
            
            iTween.MoveTo(player, iTween.Hash("position", nextPos, "time", 2, "easetype", iTween.EaseType.linear, "onComplete", "OnCompleteMove",
    "onCompleteTarget", gameObject));

            if (isRotatable)
            {
                //Prompt player to rotate tile or not

                //If yes
                SwitchDirection();
            }

            nextTile.Move(gO, steps - 1);
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

    void SwitchDirection()
    {
        if (isRotatable) {
            if (possibleDirections[0].Equals(currentDirection))
            {
                currentDirection = possibleDirections[1];
            }
            else
            {
                currentDirection = possibleDirections[0];
            }
            if (possibleTiles[0].Equals(nextTile))
            {
                nextTile = possibleTiles[1];
            }
            else
            {
                nextTile = possibleTiles[0];
            }
        }
    }
}
