using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
    public GameObject bulletPrefab;
    Player player;
    
	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 dirInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        if(Input.GetKeyDown(KeyCode.Space)) {
            GameObject bullet = (GameObject) Instantiate(bulletPrefab, player.transform.position, Quaternion.identity);
        }

        player.SetDirectionalInput(dirInput);
	}
}
