using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public List<string> blockTarget; 

    private void OnTriggerEnter2D(Collider2D other) {
        for(int i=0; i<blockTarget.Count; i++){
            if(other.CompareTag(blockTarget[i])){
                Destroy(other.gameObject);
            }
        }
    }
}
