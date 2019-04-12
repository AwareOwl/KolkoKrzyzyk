using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {

    public int sizeX;
    public int sizeY;
    public int [,] board;
    public int turnOfPlayer;
    public int winner;

    public GameState (int x, int y) {
        sizeX = x;
        sizeY = y;
        board = new int [x, y];
        turnOfPlayer = 1;
        winner = 0;
    }

    public GameState (GameState boardClass) {
        sizeX = boardClass.board.GetLength (0);
        sizeY = boardClass.board.GetLength (1);
        board = new int [sizeX, sizeY];
        for (int x = 0; x < sizeX; x++) {
            for (int y = 0; y < sizeY; y++) {
                board [x, y] = boardClass.board [x, y];
            }
        }
        turnOfPlayer = boardClass.turnOfPlayer;
    }

    public void MakeAMove (int x, int y) {
        if (winner > 0 || board [x, y] != 0) {
            return;
        }
        board [x, y] = turnOfPlayer;
        winner = CheckWinCondition ();
        SwitchPlayer ();
    }

    public void SwitchPlayer () {
        turnOfPlayer = (turnOfPlayer % 2) + 1;
    }


    public int CheckWinCondition () {
        int [] count;
        for (int x = 0; x < sizeX; x++) {
            count = new int [3];
            for (int y = 0; y < sizeY; y++) {
                count [board [x, y]]++;
            }
            if (count [1] == 3) {
                return 1;
            } else if (count [2] == 3) {
                return 2;
            }
            count = new int [3];
            for (int y = 0; y < sizeY; y++) {
                count [board [y, x]]++;
            }
            if (count [1] == 3) {
                return 1;
            } else if (count [2] == 3) {
                return 2;
            }

        }
        count = new int [3];
        for (int x = 0; x < sizeX; x++) {
            count [board [x, x]]++;
        }
        if (count [1] == 3) {
            return 1;
        } else if (count [2] == 3) {
            return 2;
        }
        count = new int [3];
        for (int x = 0; x < sizeX; x++) {
            count [board [x, 2 - x]]++;
        }
        if (count [1] == 3) {
            return 1;
        } else if (count [2] == 3) {
            return 2;
        }
        return 0;
    }

    override public string ToString () {
        string s = "";
        for (int x = 0; x < sizeX; x++) {
            for (int y = 0; y < sizeY; y++) {
                s += board [x, y] + " ";
            }
            s += System.Environment.NewLine;
        }
        return s;
    }
}
