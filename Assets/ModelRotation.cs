using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]	//or other collider
public class ModelRotation : MonoBehaviour {

	public float rotationSpeed;
	public bool rightSide = false;
	private bool onOff = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(onOff && !rightSide)
			transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
		else if(onOff && rightSide)
			transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime, Space.World);
	}


}
