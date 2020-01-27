using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Direction currentDirection;
    public Direction[] possibleDirections;
    public Tile[] possibleTiles;

    public bool isRotatable = false;

    private GameObject player;
    public Tile nextTile;
    private int playerSteps;
    public GameObject RotatePromptUI;
    public Button YesButton;
    public Button NoButton;

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
        if (endTile)
        {
            return;
        }

        if(steps > 0)
        {
            player = gO;
            //playerSteps = steps;
            Vector3 nextPos = Vector3.zero;

            Tile next;

            if (player.GetComponent<Player>().isPossessed)
            {
                foreach(Player otherPlayer in players)
                {
                    if (otherPlayer.currentTile.Equals(this))
                    {
                        otherPlayer.Possess();
                    }
                }
            }

            if (!player.GetComponent<Player>().isPossessed)
            {
                foreach (Player otherPlayer in players)
                {
                    if (otherPlayer.currentTile.Equals(this))
                    {
                        otherPlayer.UnPossess();
                    }
                }
            }

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

            player.GetComponent<Player>().UpdateCurrentTile(nextTile);

            next = nextTile;

            if (isRotatable)
            {
                //Prompt player to rotate tile or not
                RotatePromptUI.GetComponent<RotatePrompt>().RotatePromptDisplay();
                //If yes
                YesButton.onClick.AddListener(SwitchDirection);
                //SwitchDirection();
                //If no
                NoButton.onClick.AddListener(RotatePromptUI.GetComponent<RotatePrompt>().CloseRotatePrompt);
            }

            next.Move(gO, steps - 1);

        }
    }


    void OnCompleteMove()
    {
        //Debug.Log("On Complete Move");

        //if (playerSteps == 1)
        //{
        //    player.GetComponent<Player>().UpdateCurrentTile(nextTile);
        //    return;
        //}
        //nextTile.Move(player, playerSteps - 1);
    }

    void SwitchDirection()
    {
        if (isRotatable)
        {
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

            // Close Prompt after pressing Yes
            RotatePromptUI.GetComponent<RotatePrompt>().CloseRotatePrompt();
        }        
    }
}
