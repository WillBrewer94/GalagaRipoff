using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public List<GameObject> formation;
    public GameObject enemy;
    public GameObject rowOne;
    public GameObject rowTwo;
    public GameObject rowThree;
    public GameObject player;

    public int[] randomLocation = {1, 2, 3, 4};
    public Vector2 startPos;
    public float enemyTimer;
    public float moveTimer;
    public float counter;
    public int lives;
    public int score;
    public int randDir = 0;
    public int wavesSpawned;
    public int enemiesSpawned = 0;
    public bool leftMove = true;
    
    // Use this for initialization
    void Start () {
        formation = new List<GameObject>();
        lives = 3;
        score = 0;
        moveTimer = 0;
        counter = enemyTimer - 1;
	}

    // Update is called once per frame
    void Update() {
        counter += Time.deltaTime;

        if(counter > enemyTimer) {
            int ran = Random.Range(0, 4);

            switch(ran) {
                case 0:
                    startPos = new Vector2(50, 10);
                    break;
                case 1:
                    startPos = new Vector2(-30, 10);
                    break;
                case 2:
                    startPos = new Vector2(-30, -10);
                    break;
                case 3:
                    startPos = new Vector2(50, -10);
                    break;
                default:
                    startPos = new Vector2(50, -10);
                    break;
            }

            //Debug.Log(startPos);

            SpawnEnemies();
            randDir = -1;
            wavesSpawned++;
            counter = 0;
        }

        if(score > 30) {
            SceneManager.LoadScene("Win");
        }
    }

    public void SpawnEnemies() {
        //Calculate formation position
        Vector2 anchor;
        Vector2 pos;

        if(formation.Count > 12) {
            anchor = rowThree.transform.position;
            pos = new Vector2(anchor.x + (6 * rowThree.GetComponent<RowInfo>().GetRowCount()), anchor.y);
            rowThree.GetComponent<RowInfo>().IncRowCount();
        } else if(formation.Count > 6) {
            anchor = rowTwo.transform.position;
            pos = new Vector2(anchor.x + (6 * rowTwo.GetComponent<RowInfo>().GetRowCount()), anchor.y);
            rowTwo.GetComponent<RowInfo>().IncRowCount();
        } else {
            anchor = rowOne.transform.position;
            pos = new Vector2(anchor.x + (6 * rowOne.GetComponent<RowInfo>().GetRowCount()), anchor.y);
            rowOne.GetComponent<RowInfo>().IncRowCount();
        }

        GameObject newEnemy = (GameObject)Instantiate(enemy, startPos, Quaternion.identity);
        Debug.Log(startPos);
        newEnemy.GetComponent<Enemy>().SetEndLoc(new Vector2(pos.x , pos.y));
        formation.Add(newEnemy);

        enemiesSpawned++;

        if(enemiesSpawned < 8) {
            Invoke("SpawnEnemies", 0.3f);
        } else {
            enemiesSpawned = 0;
        }
    }

    public void Respawn() {
        Debug.Log("Respawn");
        lives--;

        if(lives == 0) {
            SceneManager.LoadScene("GameOver");
        }

        StartCoroutine(Wait(4));
    }

    public void IncScore() {
        score++;
    }

    IEnumerator Wait(float sec) {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<SpriteRenderer>().enabled = false;
        Time.timeScale = 0.1f;

        yield return new WaitForSecondsRealtime(sec);

        player.GetComponent<SpriteRenderer>().enabled = true;
        Time.timeScale = 1;

        StartCoroutine(Invincible());
    }

    IEnumerator Invincible() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(3);

        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<SpriteRenderer>().color = Color.white;

    }
}
