using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[SerializeField] private float m_DashForce = 400f;							// Amount of force added when the player slam.	
	[SerializeField] private float m_Speedup = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] public LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] public Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] public Transform m_CeilingCheck;							// A position marking where to check for ceilings


    public bool firstland = true;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded.
	public bool m_midair;            // Whether or not the player is midair.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	public BoolEvent OnSpeedupEvent;
	private bool m_wasspeedup = false;

	public float moves;
	public Transform attackcheck;
	public GameObject dust;
	public Animator player;

	public bool onDash = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}
    
    private void Update()
    {
    	if(m_Grounded == true)
		{
			m_midair = false;
			if(firstland == true)
			{
				firstland = false;
			}
		}
		else
		{
			StartCoroutine(slammer());
			firstland = true;
		}
    }
    IEnumerator slammer()
    {
    	yield return new WaitForSeconds(0.1f);
    	m_midair = true;
    }
	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
				{
					FindObjectOfType<soundmanager>().Play("jump");
					Instantiate(dust, m_GroundCheck.position, m_GroundCheck.rotation);
					OnLandEvent.Invoke();
				}
				if(onDash == true)
				{
					player.Play("land");
                    onDash = false;

                }
			}
		}
	}


	public void Move(float move, bool crouch, bool jump, bool slam, bool speedup,bool dash)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		if (dash)
		{
            m_Rigidbody2D.AddForce(new Vector2(0f, m_DashForce));
            FindObjectOfType<soundmanager>().Play("dash");
            Instantiate(dust, m_GroundCheck.position, m_GroundCheck.rotation);
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
		{

			if(speedup)
			{
				if (!m_wasspeedup)
				{
					m_wasspeedup = true;
                    OnSpeedupEvent.Invoke(true);
				}
				move = m_Speedup;

			} else
			{
				if (m_wasspeedup)
				{
					m_wasspeedup = false;
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
				attackcheck.rotation = Quaternion.Euler(0,0,0);
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
				attackcheck.rotation = Quaternion.Euler(0,180,0);
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            FindObjectOfType<soundmanager>().Play("jump");
            Instantiate(dust, m_GroundCheck.position, m_GroundCheck.rotation);
		}
	}



	public void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
