using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlchemyTable : MonoBehaviour {
	
	public List<GameObject> itemsOnTable = new List<GameObject>();
	public GameObject bridge;
	// Use this for initialization
	void Start () {
	}
	//Debug.Log (itemsOnTable.Contains (GameObject.Find ("Sword").GetComponent<ObjectTimeManager> ().swordList [GameObject.Find ("sword").GetComponent<ObjectTimeManager> ().currentState].item));
	// Update is called once per frame
	void Update () {
			if (Input.GetKeyDown (KeyCode.F)) {
			if((itemsOnTable.Contains(GameObject.Find("nail"))) && itemsOnTable.Contains(GameObject.Find("wood")) && (itemsOnTable.Contains(GameObject.Find("smallwood")))){
				itemsOnTable.Remove(GameObject.Find("smallwood"));
				itemsOnTable.Remove (GameObject.Find("nail"));
				itemsOnTable.Remove (GameObject.Find("wood"));
				Destroy(GameObject.Find("nail"));
				Destroy(GameObject.Find("wood"));
				Destroy(GameObject.Find("smallwood"));
				Instantiate(bridge,transform.position,Quaternion.identity);
			}
//			if((itemsOnTable.Contains(GameObject.Find("nail"))) && itemsOnTable.Contains(GameObject.Find("wood"))){
//			}
//		}
		}
	}
	void OnCollisionEnter(Collision collisionInfo) {
		itemsOnTable.Add (collisionInfo.collider.gameObject);
	}
	void OnCollisionExit(Collision collisionInfo) {
		itemsOnTable.Remove (collisionInfo.collider.gameObject);
	}
}
