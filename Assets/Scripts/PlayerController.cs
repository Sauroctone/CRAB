using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Movement")]

    float hinput;
    float vinput;
    public float speed;
    Vector3 direction;
    Vector3 movement;

    [Header("Raycast and rotation")]

    public LayerMask layer;
    RaycastHit hit;
    public float rotLerp;


    [Header("References")]

    public Rigidbody rb;

	void Update ()
    {
        //Calculate the direction vector based on inputs

        hinput = Input.GetAxisRaw("Horizontal");
        vinput = Input.GetAxisRaw("Vertical");
		print (hinput);
		print (vinput);
        direction = new Vector3(hinput, 0f, vinput).normalized;

/*        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
*/

        movement = direction * speed;

    }

    void FixedUpdate()
    {
        //Set velocity based on movement vector

        rb.velocity = transform.TransformDirection(movement);

        //Rotate to align the player's "up" vector with surface normal

        if (rb.velocity != Vector3.zero && Physics.Raycast(transform.position, -transform.up, out hit, 1f, layer))
        {
            Debug.DrawRay(transform.position, -transform.up, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.blue);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward, hit.normal), rotLerp);
          //  print(transform.rotation);
        }
    }
}
