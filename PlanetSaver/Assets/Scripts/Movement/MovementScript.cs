﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
	public float speed;

    // Update is called once per frame
    // void FixedUpdate()
    // {
    //   transform.Translate(Vector2.up * speed * Time.deltaTime);
    // }

    public void Move(Vector2 velocity){
      transform.position += (Vector3)velocity * speed * Time.deltaTime;
    }
}
