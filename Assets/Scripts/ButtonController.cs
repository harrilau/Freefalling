using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    public Sprite pressedSprite;

    public Transform spawnpoint;
    public GameObject blockToSpawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            this.GetComponent<SpriteRenderer>().sprite = pressedSprite;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            Destroy(GetComponent<Collider2D>());
            Instantiate(blockToSpawn, spawnpoint.position, spawnpoint.rotation);
        }
    }
}
