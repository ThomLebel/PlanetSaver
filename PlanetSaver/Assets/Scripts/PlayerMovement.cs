using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed;
	public Joystick joystick;

	private Vector2 move;
	private Camera cam;
	private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
		move.x = joystick.Horizontal;
		move.y = joystick.Vertical;
    }

	private void FixedUpdate()
	{
		rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);

		if (move != Vector2.zero)
		{
			float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg - 90f;
			rb.rotation = angle;
		}
	}
}
