// based on: http://forum.unity3d.com/threads/swipe-in-all-directions-touch-and-mouse.165416/
// add video: https://developer.vuforia.com/forum/faq/unity-how-do-i-create-simple-videoplayback-app
// and: https://developer.vuforia.com/forum/faq/unity-how-do-i-play-video-url
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwipeImage : MonoBehaviour
{
	public GameObject[] page;

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;

	Vector2 deltaPosition;
	Vector2 currentPosition;
	
	int amount = 5;
	int i = 0;
	float accumulatePosition = 0.0f;
	public Image[] image;
	public float SlidingSpeed = 2.0f;

	public GameObject[] model;

	//Vector3 originalPosition = new Vector3 (-200,0,-6);


	// Use this for initialization
	void Start ()
	{

        for (int k = 0; k < 5 ;k++)  // iterate through model names
		{
			//	page[i].GetComponent<RectTransform>().localPosition = new Vector3 (originalPosition.x,originalPosition.y,originalPosition.z); // set to original position
			page[k].SetActive(false);
			model[k].SetActive (false);
		}
		i = 0;
		page [0].SetActive (true);
		model[0].SetActive (true);
		Debug.Log(amount);
		for(int j = 0; j < amount; j++ ) {
		Debug.Log (j + ": " + page[j]); 
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		Swipe ();
	}

	//inside class

		
	public void Swipe ()
	{
		if (Input.GetMouseButtonDown (0)) {
			//save began touch 2d point
			firstPressPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		}
		if (Input.GetMouseButton(0)) {
			//Vector2 touchDeltaPosition = Input.mousePosition;
			//lastMousePosition +=touchDeltaPosition.x;
			currentPosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			//create vector from the two points
			//deltaPosition = new Vector2 (currentPosition.x - lastPosition.x, currentPosition.y - lastPosition.y);
			deltaPosition = new Vector2 (currentPosition.x - firstPressPos.x, currentPosition.y - firstPressPos.y);
			//if (deltaPosition.x<0) 
			accumulatePosition += deltaPosition.x;

			//if (deltaPosition.x>0) 
			//	accumulatePosition -= deltaPosition.x;
			//Debug.Log ("accumulateDelta: " + accumulatePosition + " deltaPosition: " + deltaPosition.x);
			float val = System.Math.Abs(deltaPosition.x);
			float remap = val.Remap(0, 200, 1, 0);
			Debug.Log ("ABS" + val + " REMAP: " + remap);
			//image[i].color =  new Color(1.0f,1.0f,1.0f,remap);

			if(deltaPosition.x < 0.0f){
				page[i].GetComponent<RectTransform>().localPosition = new Vector3(deltaPosition.x * SlidingSpeed, 0, -6); // page[i].GetComponent<RectTransform>().localPosition.x + 
				//page[i].transform.Translate(Vector3.left  * 500 * Time.deltaTime);
			} else if (deltaPosition.x > 0.0f) {
				page[i].GetComponent<RectTransform>().localPosition = new Vector3(deltaPosition.x * SlidingSpeed, 0, -6); // page[i].GetComponent<RectTransform>().localPosition.x + 
				//page[i].transform.Translate(Vector3.right * 500 * Time.deltaTime);
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			accumulatePosition = 0;
			//save ended touch 2d point
			secondPressPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				
			//create vector from the two points
			currentSwipe = new Vector2 (secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
			//Debug.Log ("delta" + (secondPressPos.x - firstPressPos.x));
			//normalize the 2d vector
			currentSwipe.Normalize ();
				
			//swipe upwards
			if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
				Debug.Log ("up swipe");
			}
			//swipe down
			if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
				Debug.Log ("down swipe");
			}
			//swipe left
			if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
				Debug.Log ("left swipe");

				page[i].GetComponent<RectTransform>().localPosition = new Vector3 (0,0,-6); // set to original position
				page[i].SetActive (false);
				model[i].SetActive (false);	
				//image[i].color =  new Color(1.0f,1.0f,1.0f,1.0f);
				i--;

				if (i < 0)
					i = amount-1;

				model[i].SetActive (true);
				page[i].SetActive (true);
			}
			//swipe right
			if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
				Debug.Log ("right swipe");
				page[i].GetComponent<RectTransform>().localPosition = new Vector3 (0,0,-6); // set to original position
				page[i].SetActive (false);
				model[i].SetActive (false);
				//image[i].color =  new Color(1.0f,1.0f,1.0f,1.0f);
				i++;
                Debug.Log(i);
                if (i > (amount-1))
					i=0;
                Debug.Log(i);
                page[i].SetActive (true);
				model[i].SetActive (true);
			}
			if ( (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) == false && (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) == false && (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) == false && (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) == false)  {
				Debug.Log("Click");
				//if (i==0) 
				//	Application.OpenURL("https://www.youtube.com/EdgarasArt");
				//if (i==1) 
				//	Application.OpenURL("https://www.ourtechart.com");
				//if(i==2)
				//	Application.OpenURL("https://www.youtube.com/EdgarasArt");
			}
		}
	}



}

public static class ExtensionMethods {
	
	public static float Remap (this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
	
}