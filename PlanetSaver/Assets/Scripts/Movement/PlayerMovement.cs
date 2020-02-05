using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed;
	public Joystick joystick;
	public Vector2 xClampValue;
	public Vector2 yClampValue;

	private Vector2 move;
	private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
		rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		move.x = joystick.Horizontal;
		move.y = joystick.Vertical;

		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, xClampValue.x, xClampValue.y),
			Mathf.Clamp(transform.position.y, yClampValue.x, yClampValue.y),
			transform.position.z);
    }

	private void FixedUpdate()
	{
		//rb.velocity = new Vector2(move.x, move.y);
		rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);

		if (move != Vector2.zero)
		{
			float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg - 90f;
			rb.rotation = angle;
		}
	}
}
