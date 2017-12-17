using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2dcontroller : MonoBehaviour {

    float speedForce = 10f;
    float torqueForce = -200f;

    float driftFactorSticky = 0.9f;
    float driftFactorSlippy = 1.0f;
    float maxStickyVelocity = 2.5f;
    float minStickyVelocity = 1.5f;

	// Use this for initialization
	void Start ( ) {
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        float driftFactor = driftFactorSticky;

        if(RightVelocity().magnitude > maxStickyVelocity) {
            driftFactor = driftFactorSlippy;        
        }

        rb.velocity = ForwardVelocity() + RightVelocity() * driftFactor;

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Accelerate");
            rb.AddForce(this.transform.up * speedForce);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Deaccelerate");
            rb.AddForce(this.transform.up * - speedForce / 2);   
        }

        float tf = Mathf.Lerp(0, torqueForce, rb.velocity.magnitude / 5);

        rb.angularVelocity = Input.GetAxis("Horizontal") * tf;
    }

    Vector2 ForwardVelocity() {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }

    Vector2 RightVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }
}
