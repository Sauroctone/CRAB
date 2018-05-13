using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public static bool isVisible = true;

    [Header("Movement")]

    float hinput;
    float vinput;
    public float speed;
    Vector3 direction;
    Vector3 movement;
	public static bool inFlow = false;
	public static bool controlsAble = true;
	Vector3 lastDirection;
	public Transform meshObject;
	[Range(1,5)]
	public float falseGravity;

    [Header("Raycast and rotation")]

    public LayerMask layer;
    RaycastHit hit;
    public float rotLerp;
	bool onFloor;

	[Header("Claws")]
	[Range(0.2f, 1f)]
	public float inputTrigger;
	public static bool leftClaw;
	public static bool rightClaw;
	bool leftPressed;
	bool rightPressed;
	public enum Claw {Left, Right};
	public float clawCooldown;
	Coroutine leftPincerCor;
    Coroutine rightPincerCor;

    [Header("References")]

    public Rigidbody rb;
	public SeaWeedManager sW;
	public HornDetection hrnD;
    public SoundManager soundMan;
    public ParticleSystem leftMoveBubbles;
    public ParticleSystem rightMoveBubbles;
    public FlowInterface flowInt;
    public Animator anim;

	[Header("Death")]

	public float deathTime;


    void Update ()
    {
		if (controlsAble) 
		{
            //Check claws
            if (Input.GetAxisRaw("LeftClaw") > 0 && !leftClaw && !leftPressed)
            {
                StartCoroutine(ClawInput(Claw.Left));
            }

            if (Input.GetAxisRaw("RightClaw") > 0 && !rightClaw && !rightPressed)
            {
                StartCoroutine(ClawInput(Claw.Right));
            }

            if (Input.GetAxisRaw("LeftClaw") == 0 && leftPressed)
            {
                leftPressed = false;
            }

            if (Input.GetAxisRaw("RightClaw") == 0 && rightPressed)
            {
                rightPressed = false;
            }

            //Calculate the direction vector based on inputs
            hinput = Input.GetAxisRaw ("Horizontal");
			vinput = Input.GetAxisRaw ("Vertical");

			direction = new Vector3 (hinput, 0f, vinput).normalized;

			movement = direction * speed;

            //Animation Bubble particles while moving
            if (movement != Vector3.zero)
            {
                if (!leftMoveBubbles.isPlaying)
                    leftMoveBubbles.Play();
                if (!rightMoveBubbles.isPlaying)
                    rightMoveBubbles.Play();
                
                if (!anim.GetBool("isMoving"))
                {
                    anim.SetBool("isMoving", true);
                }
            }

            else
            {
                if (leftMoveBubbles.isPlaying)
                    leftMoveBubbles.Stop();
                if (rightMoveBubbles.isPlaying)
                    rightMoveBubbles.Stop();

                if (anim.GetBool("isMoving"))
                {
                    anim.SetBool("isMoving", false);
                }
            }
        }
			
    }

    void FixedUpdate()
    {
		
		if (!inFlow && PlayerController.controlsAble) 
		{
			//Set velocity based on movement vector
			Vector3 velocityVector = Camera.main.transform.TransformDirection (movement);
			Vector3 currentNormal = GetNormalAverage ();
			Vector3 left = Vector3.Cross (velocityVector,currentNormal);
			velocityVector = Vector3.Cross (currentNormal,left);

			rb.velocity = velocityVector;

			//Get and paply new rotation of the player depending of the surface
			Quaternion newRotation = GetRotationFromNormal (GetNormalAverage (), transform.forward);
			rb.MoveRotation (Quaternion.Slerp(transform.rotation,newRotation,rotLerp));
			Vector3 crabDirection = transform.InverseTransformDirection (rb.velocity);
			crabDirection = new Vector3 (crabDirection.x, 0f, crabDirection.z);

			//Change mesh orientation with movement 
			if (direction.magnitude > 0.2f) 
			{
				meshObject.localRotation = Quaternion.Slerp(meshObject.localRotation, Quaternion.LookRotation (crabDirection, Vector3.up), 0.1f);
				lastDirection = crabDirection;
			} 
			else if (lastDirection != Vector3.zero) //Keeps last orientation if the player is not moving
			{
				meshObject.localRotation = Quaternion.Slerp(meshObject.localRotation, Quaternion.LookRotation (lastDirection, Vector3.up), 0.1f);
			}

			//false "gravity", keeps the player on the surface
			if (!onFloor) 
			{
				rb.velocity += -transform.up*falseGravity;
			}
         
		}
    }


	/// /////////////////////////////////////Death Coroutine
	public IEnumerator Die()
    {
		PlayerController.controlsAble = false;
		//Anim + sound
		yield return new WaitForSeconds(deathTime);

		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

    }

	///////////////////////////////////////Input window for claws
	IEnumerator ClawInput(Claw side)
	{
		if (side == Claw.Left)
        {
			leftClaw = true;
			leftPressed = true;
            //print("gauche");
            anim.SetTrigger("snapsLeft");
        }

        else
        {
			rightClaw = true;
			rightPressed = true;
            //print("droite");
            anim.SetTrigger("snapsRight");
        }
			
		bool hasSnipped = sW.OnClaw (side);
		hrnD.OnClaw (side);

        if (hasSnipped)
        {
            if (leftClaw)
                soundMan.Play(soundMan.pincerAlgae, 1f, 0.95f, 1.05f, -0.1f);
            else if (rightClaw)
                soundMan.Play(soundMan.pincerAlgae, 1f, 0.95f, 1.05f, 0.1f);
        }

        else
        {
            if (leftClaw)
                soundMan.Play(soundMan.pincer, 0.95f, 1f, 1.05f, -0.1f);
            else if (rightClaw)
                soundMan.Play(soundMan.pincer, 0.95f, 1f, 1.05f, 0.1f);
        }


		yield return new WaitForSeconds (clawCooldown);

		if (side == Claw.Left) 
			leftClaw = false;
		else 
			rightClaw = false;
	}

	//////////////Updates a boolean tracking if the player is touching the floor or not
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Wall" && !onFloor) 
		{
			onFloor = true;
        }
	}

	void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == "Wall" && onFloor) 
		{
			onFloor = false;
		}
	}


	//////////////Functions for orienting the player depending of the surface 

	//Get new rotation from the surface normal
	Quaternion GetRotationFromNormal(Vector3 normal, Vector3 forward)
	{
		//cross product to get the right forward 
		Vector3 left = Vector3.Cross (forward,normal);
		Vector3 newForward = Vector3.Cross (normal,left);

        //new rotation thanks to normal and forward

        Quaternion newRotation = Quaternion.LookRotation (newForward, normal);

		return newRotation;
	}

	//Get the average normal from 4 raycasts
	Vector3 GetNormalAverage()
	{
		//Init rays for raycast and variable for the average
		List<Ray> rays = new List<Ray>();
		Ray fRay = new Ray (meshObject.position + meshObject.forward*0.1f, -transform.up);
		Ray bRay = new Ray (meshObject.position - meshObject.forward*0.1f, -transform.up);
		Ray lRay = new Ray (meshObject.position + Vector3.Cross (meshObject.forward, transform.up)*0.3f, -transform.up);
		Ray rRay = new Ray (meshObject.position - Vector3.Cross (meshObject.forward, transform.up)*0.3f, -transform.up);
		Ray ffRay = new Ray (meshObject.position + meshObject.forward*0.1f, (/*-transform.up+*/meshObject.forward).normalized*0.1f);
		Ray fbRay = new Ray (meshObject.position + meshObject.forward*0.05f - meshObject.up*0.2f, (-transform.up*2f-meshObject.forward).normalized);

		rays.Add (fRay);
		rays.Add (bRay);
		rays.Add (lRay);
		rays.Add (rRay);
		rays.Add (ffRay);
		rays.Add (fbRay);

		Vector3 averageNormal = new Vector3 (0,0,0);

		//Adding each raycast's normal to the average
		for (int i = 0; i < rays.Count; i++) 
		{
			if (Physics.Raycast (rays[i], out hit, 1f, layer)) 
			{
				Debug.DrawRay (rays[i].origin, rays[i].direction, Color.red);
				averageNormal += hit.normal;
			}
		}
		//Then divide by number of normals added
		averageNormal /= rays.Count;

		Debug.DrawRay (transform.position, averageNormal, Color.green);

		return averageNormal;
	}

	//Resets local rotation of the messh to (0,0,0)
	public IEnumerator ResetMeshRotation()
	{
		Quaternion startRotation = meshObject.localRotation;
		Quaternion resetRotation = Quaternion.EulerAngles (new Vector3 (0, 0, 0));

		for (float i = 0f; i <= 1f; i += Time.deltaTime) 
		{
			meshObject.localRotation = Quaternion.Slerp(startRotation, resetRotation, i);
			yield return null;
		}

		meshObject.localRotation = resetRotation;
	}
}
