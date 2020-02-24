using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class MovementBlockerScript : MonoBehaviour
{
    public float blockDuration;

    private IEnumerator allowMovement;
    private WaitForSeconds wait;

    private void Start() {
        wait = new WaitForSeconds(blockDuration);

        EventManager.SetIndexedDataGroup(ConstantVar.BLOCK_MOVEMENT,
            new EventManager.DataGroup{id = "target", data = gameObject},
            new EventManager.DataGroup{id = "canMove", data = false}
        );
        EventManager.EmitEvent(ConstantVar.BLOCK_MOVEMENT, gameObject);

        allowMovement = AllowMovement();
        StartCoroutine(allowMovement);
    }
    
    private IEnumerator AllowMovement(){
        yield return wait;

        EventManager.SetIndexedDataGroup(ConstantVar.BLOCK_MOVEMENT,
            new EventManager.DataGroup{id = "target", data = gameObject},
            new EventManager.DataGroup{id = "canMove", data = true}
        );
        EventManager.EmitEvent(ConstantVar.BLOCK_MOVEMENT, gameObject);

        StopCoroutine(allowMovement);
        Destroy(this);
    }
}
