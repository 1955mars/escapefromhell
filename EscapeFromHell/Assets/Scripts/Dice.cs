using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public GameObject[] players;
    public Text text;
    public Text playerNumberText;
    int playerIndex;

    private bool gameOver = false;

    List<int> donePlayerIndices = new List<int>();
    List<int> possessedPlayerIndices = new List<int>();

    bool isInProgress = false;

    private bool hasCrossedRotatable = false;
    public Tile crossedRotatableTile;
    public GameObject GGPromptUI;
    public Text GGText;
    public Button ExitButton;
    public GameObject RotatePromptUI;
    public Button YesButton;
    public Button NoButton;

    bool isShowingPrompt = false;

    // Start is called before the first frame update
    void Start()
    {
        playerIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isShowingPrompt)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (isInProgress)
            //{
            //    return;
            //}

            if (playerIndex == players.Length)
            {
                playerIndex = 0;
            }
            if (donePlayerIndices.Contains(playerIndex))
            {
                playerIndex++;
                return;
            }
            if (playerIndex == 2)
            {
                playerNumberText.text = "Player 1\'s turn";
            }
            else
            {
                playerNumberText.text = "Player " + (playerIndex + 2) + "\'s turn";
            }
            RollDice();
            isInProgress = true;
        }
    }

    void RollDice()
    {
        int roll = Random.Range(1, 7);

        if (players[playerIndex].GetComponent<Player>().Move(roll))
        {
            text.text = roll.ToString();
        }
        else
            text.text = "S";
    }

    public void MoveDone()
    {
        Debug.Log("Move Done!");
        isInProgress = false;
        playerIndex++;
        ShowPromptIfNeeded();
    }

    public void playerFinished()
    {
        donePlayerIndices.Add(playerIndex);
        if(donePlayerIndices.Count == players.Length)
        {
            ShowGGPromptWin();
        }
    }

    public void playerPossessed()
    {
        possessedPlayerIndices.Add(playerIndex);
        if (possessedPlayerIndices.Count == players.Length)
        {
            ShowGGPromptLose();
        }
    }

    public bool everyoneUnpossessed()
    {
        return (possessedPlayerIndices.Count == 0);
    }

    public void playerUnPossessed()
    {
        possessedPlayerIndices.Remove(playerIndex);
    }

    public void CrossedRotatable(Tile tile)
    {
        hasCrossedRotatable = true;
        crossedRotatableTile = tile;
    }

    public void ShowPromptIfNeeded()
    {
        if (hasCrossedRotatable)
        {
            RotatePromptUI.GetComponent<RotatePrompt>().RotatePromptDisplay();
            isShowingPrompt = true;
            YesButton.onClick.AddListener(RotateTile);
            YesButton.onClick.AddListener(RotatePromptUI.GetComponent<RotatePrompt>().CloseRotatePrompt);
            NoButton.onClick.AddListener(RotatePromptUI.GetComponent<RotatePrompt>().CloseRotatePrompt);
            NoButton.onClick.AddListener(UserTakenAction);
            if (gameOver)
            {
                RotatePromptUI.GetComponent<RotatePrompt>().CloseRotatePrompt();
            }
        }
    }

    public void ShowGGPromptLose()
    {
        gameOver = true;
        CloseRotatePrompt();
        GGText.text = "LOSE!";
        GGPromptUI.GetComponent<GGPrompt>().GGPromptDisplay();
        isShowingPrompt = true;
        ExitButton.onClick.AddListener(QuitGame);
    }

    private void CloseRotatePrompt()
    {
        RotatePromptUI.GetComponent<RotatePrompt>().CloseRotatePrompt();
    }

    public void ShowGGPromptWin()
    {
        gameOver = true;
        CloseRotatePrompt();
        GGText.text = "WIN!";
        GGPromptUI.GetComponent<GGPrompt>().GGPromptDisplay();
        isShowingPrompt = true;
        ExitButton.onClick.AddListener(QuitGame);
    }

    public void RotateTile()
    {
        crossedRotatableTile.SwitchDirection();
        UserTakenAction();
    }

    public void UserTakenAction()
    {
        isShowingPrompt = false;
        hasCrossedRotatable = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}