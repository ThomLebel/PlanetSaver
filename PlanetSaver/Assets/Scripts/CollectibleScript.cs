using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public abstract class CollectibleScript : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            GetCollected(other.gameObject);
        }
    }

    protected abstract void GetCollected(GameObject target);
}
