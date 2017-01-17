using UnityEngine;
using System.Collections;

public class Controller2D : MonoBehaviour {
    Vector2 cameraPos;
    Vector2 left;
    Vector2 right; 

    // Use this for initialization
    void Start () {
        left = Camera.main.ViewportToWorldPoint(new Vector2(0.0f, 0.0f));
        right = Camera.main.ViewportToWorldPoint(new Vector2(1.0f, 0.0f));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Move(Vector2 dirInput) {
        cameraPos = Camera.main.WorldToScreenPoint(transform.position);

        transform.Translate(dirInput);

        if(cameraPos.x < 0.0) {
            transform.position = new Vector2(left.x, transform.position.y);
            Debug.Log("Left");
        } else if(cameraPos.x > Screen.width) {
            transform.position = new Vector2(right.x, transform.position.y);
            Debug.Log("Right");
        } else {
            transform.Translate(dirInput);
        } 
    }
}
