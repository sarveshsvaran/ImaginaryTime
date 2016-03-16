#pragma strict

//Variables

var mCorrectionForce = 50.0; /*Variable for the force the rigidbody will be 
using*/
	
var mPointDistance = 1.0; //Variable for the distance away from camera
	
var heldObject: Rigidbody =  null; //Rigidbody
	
var objectHeld;

function Update () //Runs through each frame.
{	
	if (Input.GetKey ("P")) //When the E key is held down.
	{		
		if (!objectHeld) //If variable: objectHeld is false.
		{
			var hit : RaycastHit; /*Variable named hit that will get information back 
			from a raycast.*/
			
			var ray : Ray = Camera.main.ScreenPointToRay(Vector3 (Screen.width * 0.5, 
			Screen.height * 0.5, 0)); /*Variable named ray that creates a raycast
			going away from the camera with an origin of half the screen width and 
			height of the camera.*/
			
			Debug.DrawRay (ray.origin, ray.direction * 5, Color.red); /*A debug ray
			that goes from ray's origin, the direction of ray * 5 (length) and 
			finally the colour of red.*/
			
			if (Physics.Raycast (ray, hit, 5)) /*Casts a ray against all colliders in
			the scene by using the ray variable's origin and direction, then the hit 
			variable's RaycastHit information, and finally, the length of the ray to 
			check if it hits a collider.*/
			{
				if (hit.collider.gameObject.tag == "Pickable") /*If hit returns true
				by hitting a collider with the tag "PickUp."*/
	
	            {
					heldObject = hit.rigidbody; /*heldObject = the gameobjects 
					rigidbody.*/
					objectHeld = true; //objectHeld = true.
	            }
	
			}
		}
	}
	else //If key E isn't held down.
	{
		objectHeld = false; //ObjectHeld = false.
	}
}
	
function FixedUpdate() //Runs through every fixed frame.
{
	if (objectHeld) //If objectHeld is true.
    {
	    
	    if(heldObject.constraints != RigidbodyConstraints.FreezeRotation) /*If 
	    rigidbody's rotation is not frozen.*/
	
        heldObject.constraints = RigidbodyConstraints.FreezeRotation; /*Freeze 
        rigidbody's rotation.*/
		
	    if(heldObject.useGravity) //If rigidbody is using gravity.
	
        heldObject.useGravity = false; //Stop rigidbody using gravity.
		
	    var targetPoint : Vector3 = Camera.main.ScreenToWorldPoint(new 
	    Vector3(Screen.width / 2, Screen.height / 2, 0)); /*Creates a new 
	    Vector 3 = half of the screen width and half of the screen height.*/
		
	    targetPoint += Camera.main.transform.forward * mPointDistance; /*Adds 
	    another position to targetPoint that is forward from the camera * the 
	    mPointDistance to determine how far away from the 
	    camera*/
		
	    var force: Vector3 = targetPoint - heldObject.transform.position; 
	    /*Creates a new Vector 3 that equals the targetPoint position - the 
	    rigidbody's position so as to get the direction to the targetPoint*/
	
	    heldObject.GetComponent.<Rigidbody>().velocity = force.normalized * 
	    heldObject.GetComponent.<Rigidbody>().velocity.magnitude; /*heldObjects velocity 
	    equals a magnitude of 1 * heldObject's magnitude to get a new 
	    direction but the same speed*/
		
	    heldObject.GetComponent.<Rigidbody>().AddForce(force * mCorrectionForce); 
	    /* the mCorrectionForce to helObjects force to move heldObject to 
	    targetPoint*/
		
	    heldObject.GetComponent.<Rigidbody>().velocity *= Mathf.Min(1.0f, force.magnitude / 
	    2); /*heldObject's velocity *= minimum of 1.0 or the magnitude of 
	    the force / 2 - smallest one */
	    
	    var distance = Vector3.Distance (heldObject.position, targetPoint);
			
		if (distance >= 2.7)
		{
			objectHeld = false;
		}
	 }
	 else
	 {
	 	UnHeld ();
	 }
}

function UnHeld ()
{
	if(heldObject.constraints == RigidbodyConstraints.FreezeRotation) 
	//If rigidbody constriants are frozen
 	{
		heldObject.constraints = RigidbodyConstraints.None; 
        //Un-freeze rigidbody's rotation
	    	    
        heldObject.useGravity = true;       
    }
}

