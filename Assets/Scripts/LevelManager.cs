using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckpoint;

    private PlayerController player;

    private CameraController cam;

    public GameObject deathParticle;
    public GameObject spawnParticle;

    public float respawnDelay;

    private float gravityStore;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();

        cam = FindObjectOfType<CameraController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
        Instantiate(deathParticle, player.transform.position, player.transform.rotation); //play death particle
        player.enabled = false;                                         //stop moving after dying
        player.GetComponent<Renderer>().enabled = false;                //makes player invisible when they die
        cam.isFollowing = false;
        gravityStore = player.GetComponent<Rigidbody2D>().gravityScale; //save gravity value
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;           //zero gravity to stop infinitely falling
        //player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Debug.Log("Player Respawn");
        yield return new WaitForSeconds(respawnDelay);
        player.GetComponent<Rigidbody2D>().gravityScale = gravityStore; //reset gravity to correct value
        player.transform.position = currentCheckpoint.transform.position;
        player.enabled = true;                                          //re-enable moving after respawning
        player.GetComponent<Renderer>().enabled = true;                 //render player again
        cam.isFollowing = true;
        Instantiate(spawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
    }
}
