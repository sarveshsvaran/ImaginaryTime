using UnityEngine;
using System.Collections;

public class PlayerLookMAnager : MonoBehaviour {
	Camera camera;
	
	void Start() {
		camera = GetComponent<Camera>();
	}
	
	void Update() {
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);


		if ((Physics.Raycast (ray, out hit))&&(hit.collider.gameObject.tag == "Pickable")) {
			if((hit.collider.transform.position - transform.position).magnitude <5){
				if (Input.GetButton("ConvertUp")){
					hit.collider.gameObject.GetComponent<ObjectTimeManager>()._onfire = true;
					hit.collider.gameObject.GetComponent<ObjectTimeManager>().timeamount+=1;
				}
				else if (Input.GetButton("ConvertDown")){
					if(hit.collider.gameObject.GetComponent<ObjectTimeManager>()._onfire == false){
						hit.collider.gameObject.GetComponent<ObjectTimeManager>()._onfire = true;
					}
					hit.collider.gameObject.GetComponent<ObjectTimeManager>()._onfire = true;
					hit.collider.gameObject.GetComponent<ObjectTimeManager>().timeamount-=1;
				}else{
					hit.collider.gameObject.GetComponent<ObjectTimeManager>()._onfire = false;
				}
		}

}
}
}


