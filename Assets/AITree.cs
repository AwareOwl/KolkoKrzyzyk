using System.Collections.Generic;
using UnityEngine;

public class AITree {

    public GameState gameState;

    bool goodMove = true;

    public AITree [,] moves;
    
    int availableMoves = 0;

    public AITree () {

    }

    public AITree (GameState gameState) {
        this.gameState = gameState;
        if (gameState.winner == 2) {
            return;
        }
        moves = new AITree [gameState.sizeX, gameState.sizeY];
        for (int x = 0; x < gameState.sizeX; x++) {
            for (int y = 0; y < gameState.sizeY; y++) {
                MakeAMove (x, y);
                if (!goodMove) {
                    return;
                }
            }
        }
        if (availableMoves == 0) {
            return;
        }
        goodMove = false;
        for (int x = 0; x < gameState.sizeX; x++) {
            for (int y = 0; y < gameState.sizeY; y++) {
                if (moves [x, y] != null) {
                    goodMove = true;
                }
            }
        }

    }
    

    public void MakeAMove (int x, int y) {
        if (gameState.board [x, y] != 0) {
            return;
        }
        availableMoves++;
        GameState tempGameState = new GameState (gameState);
        tempGameState.MakeAMove (x, y);
        switch (tempGameState.winner) {
            case 0:
            case 2:
                AITree newTree = new AITree (tempGameState);
                if (newTree.goodMove) {
                    moves [x, y] = newTree;
                } else if (gameState.turnOfPlayer == 1) {
                    moves = null;
                    goodMove = false;
                }
                break;
            case 1:
                moves = null;
                goodMove = false;
                break;

        }
    }
}
