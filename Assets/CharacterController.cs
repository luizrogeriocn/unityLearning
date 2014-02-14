using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public float animSpeed;
	
	Animator anim;

	// Use this for initialization
	void Start () {
	
		anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if( Input.GetKey(KeyCode.RightArrow) )
		{
			rigidbody2D.AddForce( transform.right * animSpeed );
			anim.SetFloat( "Speed", animSpeed );
			
			transform.localScale = transform.localScale.x < 0 ?
				new Vector3( transform.localScale.x * -1.0f,
					transform.localScale.y, transform.localScale.z )
				: transform.localScale;
		}
		else if( Input.GetKey(KeyCode.LeftArrow) )
		{
			rigidbody2D.AddForce( (-transform.right) * animSpeed );
			anim.SetFloat( "Speed", animSpeed );
			
			transform.localScale = transform.localScale.x > 0 ?
				new Vector3( transform.localScale.x * -1.0f,
				            transform.localScale.y, transform.localScale.z )
					: transform.localScale;
		}
		else
		{
			anim.SetFloat( "Speed", 0.0f );
		}
	
	}
}
