using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed = 60;
    public float flightDir = 1;
    Vector2 cameraPos;
    Vector2 target;
    public bool isPlayer = true;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        float step;
        cameraPos = Camera.main.WorldToScreenPoint(transform.position);

        if(isPlayer) {
            transform.Translate(new Vector2(0, speed * flightDir * Time.deltaTime));
        } else {
            if(Vector2.Distance(transform.position, target) > 0.005f) {
                step = Time.deltaTime * speed;
                transform.position = Vector2.MoveTowards(transform.position, target, step);
            } else {
                Destroy(gameObject);
            }
            
        }

        //Destroys bullet if out of screen bounds
        if(cameraPos.y > Screen.height) {
            Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player"  && !(isPlayer)) {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Respawn();
            
        } else {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), col.collider);
        }
    }

    public void SetTarget(Vector2 target) {
        this.target = target;
    }

    public void SetIsPlayer(bool isPlayer) {
        this.isPlayer = isPlayer;
    }
}
