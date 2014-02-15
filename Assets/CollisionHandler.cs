using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {

	private HeroProperties heroProperties;

	void OnCollisionEnter2D(Collision2D c){
		if(c.gameObject.tag == "ground")
			heroProperties.SendMessage("GroundCollided", true);
	}
	
	void OnCollisionExit2D(Collision2D c){
		if(c.gameObject.tag == "ground")
			heroProperties.SendMessage("GroundCollided", false);
	}
	
	// Use this for initialization
	void Start () {
		heroProperties = transform.parent.gameObject.GetComponent<HeroProperties>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
