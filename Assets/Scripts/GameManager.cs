using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour {
    public int index = 0;
    public RubikCube.Axis axis = RubikCube.Axis.X;
    public enum Direction { LEFT = -1, RIGHT = 1 };
    public Direction direction = Direction.RIGHT;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
