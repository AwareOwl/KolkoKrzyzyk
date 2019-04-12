using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    int x;
    int y;

    public void Init (int x, int y) {
        this.x = x;
        this.y = y;
    }

    private void OnMouseOver () {
        if (Input.GetMouseButtonDown (0)) {
            MainScript.UIMakeAMove (x, y);
        }
    }
}
