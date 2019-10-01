using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour {


    public TextAsset text;

    public int startLine;
    public int endLine;

    public TextBoxManager textBox;

    public bool destroyWhenActivated;

	// Use this for initialization
	void Start () {
        textBox = FindObjectOfType<TextBoxManager>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            textBox.ReloadScript(text);
            textBox.currentLine = startLine;
            textBox.endLine = endLine;

            textBox.EnableTextBox();

            if (destroyWhenActivated)
                Destroy(gameObject);
        }
    }
}
