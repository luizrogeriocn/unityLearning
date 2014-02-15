using UnityEngine;
using System.Collections;

public class KeyBoardController : MonoBehaviour {

	private HeroProperties heroProperties;
	
	// Use this for initialization
	void Start () {
		heroProperties = GetComponent<HeroProperties>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.LeftArrow)){
			heroProperties.SendMessage("MoveLeft");
		}
		
		if(Input.GetKey(KeyCode.RightArrow)){
			heroProperties.SendMessage("MoveRight");
		}
		
		if(Input.GetKey(KeyCode.Space)){
			heroProperties.SendMessage("Jump");
		}
	
	}
}
