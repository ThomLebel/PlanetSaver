using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class DestroyOnMultipleCollisions : MonoBehaviour
{
    public int collisionsBeforeDestruction;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(ConstantVar.COLLIDE_WITH_SOMETHING, DestroyGameObject);
    }

    private void DestroyGameObject(){
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.COLLIDE_WITH_SOMETHING);
        if(sender == null ||sender != gameObject){
            return;
        }

        collisionsBeforeDestruction --;
        
        if(collisionsBeforeDestruction <= 0){
            Destroy(gameObject);
        }
    }
}
