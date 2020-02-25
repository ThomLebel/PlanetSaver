using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class MindControlScript : MonoBehaviour
{
    public float duration;
    public string targetTag;
    public string newTag;

    private IEnumerator cancelMindControl;
    private WaitForSeconds wait;
    private string ownTag;

    // Start is called before the first frame update
    void Start()
    {
        ownTag = gameObject.tag;
        gameObject.tag = newTag;
        EventManager.SetData(ConstantVar.MIND_CONTROL, targetTag);
        EventManager.EmitEvent(ConstantVar.MIND_CONTROL, gameObject);
        
        wait = new WaitForSeconds(duration);
        cancelMindControl = CancelMindControl();
        StartCoroutine(cancelMindControl);
    }

    private IEnumerator CancelMindControl(){
        yield return wait;

        StopCoroutine(cancelMindControl);
        gameObject.tag = ownTag;
        EventManager.EmitEvent(ConstantVar.RESET_MIND_CONTROL, gameObject);
        Destroy(this);
    }
}
