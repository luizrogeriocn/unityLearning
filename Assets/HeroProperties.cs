using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class HeroProperties : MonoBehaviour {

	public float speed;
	private Animator animator;
	private bool onGround = false;

	public void LookLeft(){
		if(transform.localScale.x > 0)
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	
	public void LookRight(){
		if(transform.localScale.x < 0)
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}

	public void MoveRight(){
		LookRight();	
		if(onGround){	
			rigidbody2D.AddForce(transform.right * speed*rigidbody2D.mass);
			animator.SetFloat("speed", speed);
		}
	}
	
	public void MoveLeft(){
		LookLeft ();
		if(onGround){	
			rigidbody2D.AddForce(-transform.right * speed*rigidbody2D.mass);
			animator.SetFloat("speed", speed);
		}
	}
	
	public void Jump(){
		if(onGround){
			rigidbody2D.AddForce(transform.up * speed * 30 * rigidbody2D.mass);
			animator.SetFloat("verticalSpeed", speed);
			animator.SetBool("onGround", false);
		}
	}
	
	public void GroundCollided(bool b){
		onGround = b;
		animator.SetBool("onGround", b);
	}
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if( rigidbody2D.velocity.magnitude <= 0 ){
			animator.SetFloat("speed", 0);
			animator.SetFloat("verticalSpeed", 0);
		}

	}
}
