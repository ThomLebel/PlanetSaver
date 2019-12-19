using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class ShootScript : MonoBehaviour
{
	public GameObject bullet;
	public float fireRate;

	private float nextShot = 0f;

	private InputManager inputManager;

	private void Awake()
	{
		inputManager = GetComponent<InputManager>();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (inputManager.primaryAttack && nextShot <= 0f)
		{
			nextShot += fireRate;
			Instantiate(bullet, transform.position, transform.rotation);
		}
		if (nextShot > 0f)
		{
			nextShot -= Time.deltaTime;
		}
    }
}
