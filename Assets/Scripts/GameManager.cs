using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public List<GameObject> formation;
    public GameObject enemy;
    public GameObject rowOne;
    public GameObject rowTwo;
    public GameObject rowThree;

    public float enemyTimer = 20;
    public float counter;
    public int lives;
    public int score;
    public int enemiesSpawned = 0;


    // Use this for initialization
    void Start () {
        formation = new List<GameObject>();
        lives = 3;
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;

        if(counter > enemyTimer) {
            SpawnEnemies();

            counter = 0;
        }

        //Move enemies in formation
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

        GameObject newEnemy = (GameObject)Instantiate(enemy, new Vector2(-30, 0), Quaternion.identity);
        newEnemy.GetComponent<Enemy>().SetEndLoc(new Vector2(pos.x , pos.y));
        formation.Add(newEnemy);

        enemiesSpawned++;

        if(enemiesSpawned < 8) {
            Invoke("SpawnEnemies", 0.3f);
        } else {
            enemiesSpawned = 0;
        }
    }
}
