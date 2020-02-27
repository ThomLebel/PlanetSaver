using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class SensorScript : MonoBehaviour
{
    public List<string> targetsTag;
    public GameObject owner;

    private void Start() {
        owner = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(gameObject.tag))
		{
			return;
		}

		foreach (string tag in targetsTag)
		{
			if (other.CompareTag(tag))
			{
				EventManager.SetData(ConstantVar.ON_SENSOR_ENTER, other.gameObject);
				EventManager.EmitEvent(ConstantVar.ON_SENSOR_ENTER, this.gameObject);
			}
		}
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag(gameObject.tag))
		{
			return;
		}

		foreach (string tag in targetsTag)
		{
			if (other.CompareTag(tag))
			{
				EventManager.SetData(ConstantVar.ON_SENSOR_EXIT, other.gameObject);
				EventManager.EmitEvent(ConstantVar.ON_SENSOR_EXIT, this.gameObject);
			}
		}
    }
}
