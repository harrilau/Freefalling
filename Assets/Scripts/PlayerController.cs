using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpHeight;

    private float moveVelocity;

    public Transform groundCheck; //check if player is touching platform
    public float groundCheckRadius;
    public LayerMask whatIsGround; //lmao
    public LayerMask whatIsIce;
    public LayerMask whatIsQuicksand;
    public LayerMask whatIsCorruptedGrass;
    private bool grounded;
    private bool onIce;
    private bool onQuicksand;
    private bool onCorruptedGrass;

    private float quicksandTimer;

    public LevelManager levelManager;

    private Animator anim;

    public bool canMove; //for text boxes (if we want player to be able to move)

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        onIce = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsIce);
        onQuicksand = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsQuicksand);
        onCorruptedGrass = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsCorruptedGrass);
    }

    // Update is called once per frame
    void Update () {

        if (!canMove)
            return;

        anim.SetBool("Grounded", grounded); //play idle animation if idle and grounded

        //kills player if they've been on quicksand for too long
        if (onQuicksand && quicksandTimer < 3.0f)
            quicksandTimer += Time.deltaTime;
        else if (onQuicksand && quicksandTimer >= 3.0f)
            levelManager.RespawnPlayer();
        else
            quicksandTimer = 0;

        if (Input.GetButtonDown("Jump") && (grounded || onIce)) //prevents infinite jumping
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight); 
        }
        else if (Input.GetButtonDown("Jump") && onCorruptedGrass)
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight/2);

        moveVelocity = moveSpeed * Input.GetAxisRaw("Horizontal"); //take movement from Input menu

        if (onIce)
            GetComponent<Rigidbody2D>().AddForce(new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y)); //use AddForce for ice
        else if (onCorruptedGrass)
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity/2, GetComponent<Rigidbody2D>().velocity.y); //move player
        else { 
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y); //move player
        }

        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)); //play walking animation if moving

        if (GetComponent<Rigidbody2D>().velocity.x > 0)
            transform.localScale = new Vector3(2f, 2f, 1f);
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
            transform.localScale = new Vector3(-2f, 2f, 1f);
    }
}
