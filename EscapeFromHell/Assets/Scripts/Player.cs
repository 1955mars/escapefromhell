using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public Tile currentTile;

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
        currentTile.Move(gameObject, steps);
    }

    public void UpdateCurrentTile(Tile tile)
    {
        currentTile = tile;
    }
}
