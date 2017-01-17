using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    float playerDeltaTime;
    public Vector2 dirInput;
    public float moveSpeed;

    Controller2D controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<Controller2D>();
	}
	
	// Update is called once per frame
	void Update () {
        controller.Move(dirInput * moveSpeed * Time.deltaTime);
	}

    public void SetDirectionalInput(Vector2 dirInput) {
        this.dirInput = dirInput;
    }
}
