using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour {

    public float bounciness;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.rigidbody.AddForce(new Vector2(collision.relativeVelocity.x, -collision.relativeVelocity.y) * bounciness, ForceMode2D.Impulse);
    }
}
