using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public Tile currentTile;
    public bool isPossessed = false;

    //public int steps = 5;
    // Start is called before the first frame update
    void Start()
    {
        //Move(steps);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(int steps)
    {
        if (!isPossessed)
        {
            if (steps == 1)
            {
                isPossessed = true;
            }
            currentTile.Move(gameObject, steps);
        }
        else
        {
            if(steps != 6)
            {
                currentTile.Move(gameObject, steps);
            }
        }
    }

    public void Possess()
    {
        isPossessed = true;
    }

    public void UnPossess()
    {
        isPossessed = false;
    }

    public void UpdateCurrentTile(Tile tile)
    {
        currentTile = tile;
    }
}
