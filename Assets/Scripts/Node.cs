﻿using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

    public float taskTime = 0.5f;       // seconds

    private bool selected = false;

    public void ToggleSelected() {
        selected = !selected;

        this.renderer.material.color = (selected) ? Color.green : Color.white;
    }
}
