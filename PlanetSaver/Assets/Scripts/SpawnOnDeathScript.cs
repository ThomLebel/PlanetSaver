using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class SpawnOnDeathScript : MonoBehaviour
{
    public GameObject goToSpawn;
    public int maxSpawn = 3;
    public float forceModifier = 25f;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(ConstantVar.IS_DEAD, SpawnOnDeath);
    }

    private void SpawnOnDeath(){
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.IS_DEAD);
        if(sender == null || sender != gameObject){
            return;
        }

        int randSpawn = Random.Range(1, maxSpawn);
        Debug.Log(randSpawn+" objects have spawned");

        for(int i=0; i<randSpawn; i++){
            GameObject go = Instantiate(goToSpawn, transform.position, Quaternion.identity);

            Vector2 direction = new Vector2(UnityEngine.Random.Range(-forceModifier,forceModifier), UnityEngine.Random.Range(-forceModifier,forceModifier));
            Rigidbody2D rb2d = go.GetComponent<Rigidbody2D>();
            rb2d.AddForce(direction * forceModifier);
        }
    }
}
