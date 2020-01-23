using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Player") && !other.CompareTag("Planet") && !other.CompareTag("Collectible")){
            Destroy(other.gameObject);
        }
    }
}
