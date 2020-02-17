using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MovementScript))]
public class Projectile : MonoBehaviour
{
    MovementScript movementScript;
    // public float speed;

    private void Awake() {
        movementScript = GetComponent<MovementScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementScript.Move(transform.up);
    }
}
