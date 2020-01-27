using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public Tile currentTile;
    public bool isPossessed = false;
    public MeshRenderer meshrend;
    public MeshFilter playerMesh;
    public Material unpossess;
    public Mesh unpossessMesh;
    public Material possess;
    public Mesh possessMesh;
    //public int steps = 5;
    // Start is called before the first frame update
    void Start()
    {
        meshrend = GetComponent<MeshRenderer>();
        playerMesh = GetComponent<MeshFilter>();
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
                meshrend.material = possess;
                playerMesh.mesh = possessMesh;
            }
            else
            {

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
        meshrend.material = possess;
        playerMesh.mesh = possessMesh;

    }

    public void UnPossess()
    {
        isPossessed = false;
        meshrend.material = unpossess;
        playerMesh.mesh = unpossessMesh;
    }

    public void UpdateCurrentTile(Tile tile)
    {
        currentTile = tile;
    }
}
