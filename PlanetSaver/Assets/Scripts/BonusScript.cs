using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScript : CollectibleScript
{
	public List<TemporaryBonusScript> bonusList;

	protected override void GetCollected(GameObject target){
		if(target == null){
            return;
        }

		int randomBonusIndex = Random.Range(0, bonusList.Count);
		bonusList[randomBonusIndex].Effect(target);
		Debug.Log("You get the "+bonusList[randomBonusIndex].bonusName+" bonus !");

		Destroy(this.gameObject);
	}
}
