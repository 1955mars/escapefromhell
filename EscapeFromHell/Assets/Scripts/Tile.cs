using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { North, South, East, West };

public class Tile : MonoBehaviour
{
    public Player[] players;
    public int index;

    public Tile northTile;
    public Tile southTile;
    public Tile eastTile;
    public Tile westTile;
    public bool endTile = false;
    Direction prevDirection;
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
        players = GameObject.FindObjectsOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(GameObject gO, int steps)
    {
        Debug.Log("Index : " + index);
        player = gO;
        playerSteps = steps;

        if (endTile)
        {
            player.GetComponent<Player>().UnPossess();
            GameObject.FindObjectOfType<Dice>().MoveDone();
            return;
        }

        if (steps > 0)
        {
            Vector3 nextPos = Vector3.zero;

            nextPos = nextTile.transform.position;
            nextPos.y = player.transform.position.y;

            iTween.MoveTo(player, iTween.Hash("position", nextPos, "time", 1, "easetype", iTween.EaseType.linear, "onComplete", "OnCompleteMove",
    "onCompleteTarget", gameObject));

        }
    }


    void OnCompleteMove()
    {
        Debug.Log("On Complete Move");


        if (player.GetComponent<Player>().isPossessed)
        {
            foreach (Player otherPlayer in players)
            {
                if (otherPlayer.currentTile.Equals(nextTile))
                {
                    otherPlayer.Possess();
                }
            }
        }

        if (!player.GetComponent<Player>().isPossessed)
        {
            foreach (Player otherPlayer in players)
            {
                if (otherPlayer.currentTile.Equals(nextTile))
                {
                    otherPlayer.UnPossess();
                }
            }
        }

        player.GetComponent<Player>().UpdateCurrentTile(nextTile);

        if (isRotatable && !player.GetComponent<Player>().isPossessed)
        {
            GameObject.FindObjectOfType<Dice>().CrossedRotatable(this);
            //Prompt player to rotate tile or not

            //If yes
            //SwitchDirection();
        }

        if (playerSteps == 1)
        {
            GameObject.FindObjectOfType<Dice>().MoveDone();
            return;
        }

        nextTile.Move(player, playerSteps - 1);

    }

    public void SwitchDirection()
    {
        if (isRotatable)
        {
            if (possibleDirections[0].Equals(currentDirection))
            {
                prevDirection = currentDirection;
                currentDirection = possibleDirections[1];
                RotateTile();
            }
            else
            {
                prevDirection = currentDirection;
                currentDirection = possibleDirections[0];
                RotateTile();
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

    void RotateTile()
    {
        Debug.Log("Rotate Tile" + currentDirection);
        Vector3 prevRot = GiveRotation(prevDirection);
        Vector3 currRot = GiveRotation(currentDirection);
        Debug.Log("Curr Rot" + currRot);
        transform.rotation = Quaternion.Euler(currRot);
    }

    Vector3 GiveRotation(Direction dir)
    {
        switch(dir)
        {
            case (Direction.East):
                return new Vector3(0, 90f, 0);
            case (Direction.North):
                return new Vector3(0, 0, 0);
            case (Direction.South):
                return new Vector3(0, 180f, 0);
            case (Direction.West):
                return new Vector3(0, -90f, 0);
            default:
                return new Vector3(0, 0, 0);
        }
    }
}
