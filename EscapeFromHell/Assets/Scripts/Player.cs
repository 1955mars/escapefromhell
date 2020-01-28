using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static public bool hasGameStarted = false;
    static public bool isEveryOneUnpossessed = false;


    public string playerName;
    public Tile currentTile;
    public bool isPossessed = false;

    private bool skipNextRoll = false;

    public MeshFilter playerMesh;
    public Mesh unPossessMesh;
    public Mesh possessMesh;

    //public int steps = 5;
    // Start is called before the first frame update
    void Start()
    {
        playerMesh = GetComponent<MeshFilter>();
        transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);

        //Move(steps);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Move(int steps)
    {
        if (!hasGameStarted)
        {
            if (steps == 1)
            {
                Possess();
                hasGameStarted = true;
                skipNextRoll = true;
            }
            GameObject.FindObjectOfType<Dice>().MoveDone();
            return true;
        }


        if (skipNextRoll)
        {
            skipNextRoll = false;
            GameObject.FindObjectOfType<Dice>().MoveDone();
            return false;
        }


        if (isEveryOneUnpossessed && steps == 1)
        {
            Possess();
            isEveryOneUnpossessed = false;
            skipNextRoll = true;
            GameObject.FindObjectOfType<Dice>().MoveDone();
            return true;
        }

        if (isPossessed && steps == 6)
        {
            GameObject.FindObjectOfType<Dice>().MoveDone();
            return true;
        }

        currentTile.Move(gameObject, steps);

        return true;
    }

    public void Possess()
    {
        GameObject.FindObjectOfType<Dice>().playerPossessed();
        isPossessed = true;
        playerMesh.mesh = possessMesh;
    }

    public void UnPossess()
    {
        GameObject.FindObjectOfType<Dice>().playerUnPossessed();
        isPossessed = false;
        playerMesh.mesh = unPossessMesh;
    }

    public void UpdateCurrentTile(Tile tile)
    {
        currentTile = tile;
    }
}
