using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float radius;
    public float lineSpeed;
    public float circleSpeed;
    public Vector2 endLoc;
    float frameCount;

	// Use this for initialization
	void Start () {
        StartCoroutine(LineMove(new Vector2(0, 0)));
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    public void SetEndLoc(Vector2 endLoc) {
        this.endLoc = endLoc;
    }

    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log(col.gameObject.tag);

        if(col.gameObject.tag == "Enemy") {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col.collider);
        }

        if(col.gameObject.tag == "Bullet") {
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }
    }

    IEnumerator LineMove(Vector3 target) {
        float step;

        while(Vector2.Distance(transform.position, target) > 0.005f) {
            step = Time.deltaTime * lineSpeed;
            transform.position = Vector2.MoveTowards(transform.position, target, step);

            yield return null;
        }

        StartCoroutine(CircleMove());
    }

    IEnumerator CircleMove() {
        float totalRadians = 0;

        while(totalRadians < 250) {
            frameCount -= Time.fixedDeltaTime * circleSpeed;
            
            totalRadians += Mathf.Abs(frameCount);

            Vector2 circlePos = new Vector2(Mathf.Cos(frameCount), Mathf.Sin(frameCount)) * radius;
            transform.position = circlePos;

            yield return null;
        }

        StartCoroutine(FormationMove());
    }

    IEnumerator FormationMove() {
        float step;

        while(Vector2.Distance(transform.position, endLoc) > 0.005f) {
            step = Time.deltaTime * lineSpeed;
            transform.position = Vector2.MoveTowards(transform.position, endLoc, step);

            yield return null;
        }
    }
}
