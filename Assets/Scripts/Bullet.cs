using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed;
    public float flightDir = 1;
    Vector2 cameraPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        cameraPos = Camera.main.WorldToScreenPoint(transform.position);

        transform.Translate(new Vector2(0, speed * flightDir * Time.deltaTime));

        //Destroys bullet if out of screen bounds
        if(cameraPos.y > Screen.height) {
            Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D col) {
        
    }
}
