using UnityEngine;
using System.Collections.Generic;

public class RowInfo : MonoBehaviour {
    public int rowCount;
    public GameObject[] formation;

	// Use this for initialization
	void Start () {
        formation = new GameObject[6];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int GetRowCount() {
        return rowCount;
    }

    public void IncRowCount() {
        rowCount++;
    }
}
