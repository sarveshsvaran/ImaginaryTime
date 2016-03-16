using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemObject // Used in Lists.
{
	public GameObject item { get; set; }
	public string itemName { get; set; }
}

public class ObjectTimeManager : MonoBehaviour {

	float speed = 50;
	public float timeamount;
	public bool _onfire;
	bool _poofFlag;
	public GameObject poof;
	
	


	public int currentState;

	public List<GameObject> itemProxy = new List<GameObject>();
	public List<string> nameProxy = new List<string>();
	public List<ItemObject> swordList = new List<ItemObject> ()
	{
		new ItemObject(){ item = null, itemName = ""},
		new ItemObject(){ item = null, itemName = ""},
		new ItemObject(){ item = null, itemName = ""}
	};
	
	// Use this for initialization
	void Start () {
		//currentState = 0;
		//timeamount = 1;
		_onfire = false;
		_poofFlag = true;
		for(int i=0;i<3;i++){
			Debug.Log(i);
			swordList[i].item = itemProxy[i];
			swordList[i].itemName = nameProxy[i];
			Debug.Log(swordList[i].item.name);
		}
	}

	// Update is called once per frame
	void Update () {
		Debug.Log (swordList[currentState].item.name);
		//GetComponent<MeshFilter>().sharedMesh = swordList[currentState].item.GetComponent<MeshFilter>().sharedMesh;
		GetComponent<MeshFilter>().sharedMesh = itemProxy[currentState].GetComponent<MeshFilter>().sharedMesh;
		gameObject.name = swordList [currentState].itemName;
		if (timeamount < 100) {
			Debug.Log(itemProxy[currentState].name);
			currentState =0;
			GetComponent<MeshFilter>().sharedMesh = itemProxy[currentState].GetComponent<MeshFilter>().sharedMesh;
		} 
		else if ((timeamount<200) && (timeamount> 100)) { 
			currentState =1;
		} 
		else if ((timeamount<300) && (timeamount> 200)) {
			currentState =2;
		}
		if(_onfire)
		{
		float AngleAmount = (Mathf.Cos (Time.time * 20) * 180) / Mathf.PI * 0.5f;
         AngleAmount = Mathf.Clamp (AngleAmount, -15, 15);
         transform.localRotation = Quaternion.Euler (0, 0, AngleAmount);
		}
		if(timeamount%100 == 0)
		{
			var clone = Instantiate(poof,transform.position,Quaternion.identity) as GameObject;
			Destroy(clone,2);
		}
	}
}


