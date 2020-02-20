using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed;
	public Joystick joystick;
	public Vector2 xClampValue;
	public Vector2 yClampValue;

	[SerializeField] private float speed;
	private Vector2 move;
	private Rigidbody2D rb;
	private bool buffed;
	private float buffDuration;

    // Start is called before the first frame update
    void Awake()
    {
		rb = GetComponent<Rigidbody2D>();
    }

	private void Start() {
		EventManager.StartListening(ConstantVar.BOOST_SPEED, BoostSpeed);
		speed = moveSpeed;
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

		if(buffed && buffDuration > 0f){
			buffDuration -= Time.deltaTime;
		}else{
			buffed = false;
			speed = moveSpeed;
		}
    }

	private void FixedUpdate()
	{
		// rb.MovePosition(rb.position + move * speed * Time.deltaTime);
		rb.AddForce(move * speed, ForceMode2D.Force);

		if (move != Vector2.zero)
		{
			float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg - 90f;
			rb.rotation = angle;
		}
	}

	private void BoostSpeed(){
		var eventData = EventManager.GetIndexedDataGroup(ConstantVar.BOOST_SPEED);
		GameObject target = eventData.ToGameObject("target");
        if(target == null || target != gameObject){
            return;
        }

		float value = eventData.ToFloat("value");
		float duration = eventData.ToFloat("duration");

		speed += Mathf.Floor((speed * value) / 100);
		buffDuration += duration;
		buffed = true;
	}
}
