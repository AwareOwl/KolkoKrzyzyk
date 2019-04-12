using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {

    static MainScript instance;
    
    GameState gameState;
    GameObject [,] tile;
    Material [,] tileMaterial;

    AITree refTree1;
    AITree refTree2;
    AITree tempTree;

    // Use this for initialization
    void Start () {
        instance = this;

        GameState gs1 = new GameState (3, 3);
        gs1.turnOfPlayer = 1;
        refTree1 = new AITree (gs1);

        GameState gs2 = new GameState (3, 3);
        gs2.turnOfPlayer = 2;
        refTree2 = new AITree (gs2);

        Restart ();
    }

    public void GenerateTiles () {
        tile = new GameObject [gameState.sizeX, gameState.sizeY];
        tileMaterial = new Material [gameState.sizeX, gameState.sizeY];
        for (int x = 0; x < gameState.sizeX; x++) {
            for (int y = 0; y < gameState.sizeY; y++) {
                GameObject clone = GameObject.CreatePrimitive (PrimitiveType.Quad);
                clone.transform.position = new Vector3 (x * 2 - 2, y * 2 - 2, 0);
                tileMaterial [x, y] = clone.GetComponent<Renderer> ().material;
                tileMaterial [x, y].shader = Shader.Find ("Sprites/Default");
                clone.AddComponent<UIController> ().Init (x, y);
            }
        }
    }

    static public void UIMakeAMove (int x, int y) {
        instance.MakeAMove (x, y);
    }

    public void MakeAMove (int moveX, int moveY) {
        if (gameState.winner > 0) {
            return;
        }
        gameState.MakeAMove (moveX, moveY);
        SetColor (moveX, moveY);
        if (tempTree != null && tempTree.moves != null) {
            tempTree = tempTree.moves [moveX, moveY];
        }
        if (gameState.winner > 0) {
            Debug.Log (gameState.winner);
        }
        RunAI ();
    }

    public void RunAI () {
        if (gameState.turnOfPlayer == 2) {
            for (int x = 0; x < gameState.sizeX; x++) {
                for (int y = 0; y < gameState.sizeY; y++) {
                    if (tempTree != null && tempTree.moves != null && tempTree.moves [x, y] != null) {
                        MakeAMove (x, y);
                        return;
                    }
                }
            }
        }
    }
    
    public void Restart () {
        gameState = new GameState (3, 3);
        if (tile == null) {
            GenerateTiles ();
        }
        for (int x = 0; x < gameState.sizeX; x++) {
            for (int y = 0; y < gameState.sizeY; y++) {
                SetColor (x, y);
            }
        }
        int player = Random.Range (1, 3);
        gameState.turnOfPlayer = player;
        if (player == 1) {
            tempTree = refTree1;
        } else {
            tempTree = refTree2;
        }
        RunAI ();
    }

    public void SetColor (int x, int y) {
        Color col;
        switch (gameState.board [x, y]) {
            case 1:
                col = Color.green;
                break;
            case 2:
                col = Color.red;
                break;
            default:
                col = Color.white;
                break;
        }
        tileMaterial [x, y].color = col;
    }
}
