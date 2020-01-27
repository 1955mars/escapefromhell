using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public GameObject[] players;
    public Text text;
    int playerIndex;

    List<int> donePlayerIndices = new List<int>();

    bool isInProgress = false;

    private bool hasCrossedRotatable = false;
    public Tile crossedRotatableTile;

    // Start is called before the first frame update
    void Start()
    {
        playerIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //if (isInProgress)
            //{
            //    return;
            //}

            if (playerIndex == players.Length)
            {
                playerIndex = 0;
            }
            if(donePlayerIndices.Contains(playerIndex))
            {
                playerIndex++;
                return;
            }
            RollDice();
            isInProgress = true;
        }
    }

    void RollDice()
    {
        int roll = Random.Range(1, 7);
        if (players[playerIndex].GetComponent<Player>().Move(roll))
            text.text = roll.ToString();
        else
            text.text = "S";
    }

    public void MoveDone()
    {
        Debug.Log("Move Done!");
        isInProgress = false;
        playerIndex++;
    }

    public void playerFinished()
    {
        donePlayerIndices.Add(playerIndex);
    }

    public void CrossedRotatable(Tile tile)
    {
        hasCrossedRotatable = true;
        crossedRotatableTile = tile;
    }

    public void ShowPromptIfNeeded()
    {

    }
}