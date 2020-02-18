using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BonusScript : CollectibleScript
{
	public Bonus[] bonusList;
	int totalDropRate;

	private void Start() {
		bonusList = bonusList.OrderByDescending(x => x.weight).ToArray();
        foreach(Bonus bonus in bonusList){
            totalDropRate += bonus.weight;
        }
	}

	protected override void GetCollected(GameObject target){
		if(target == null){
            return;
        }

		int randomBonusIndex = UnityEngine.Random.Range(0, totalDropRate);
		int bonusIndex = 0;
		for(int j=0; j<bonusList.Length; j++){
			Bonus bonus = bonusList[j];
			
			if(randomBonusIndex <= bonus.weight){
				bonusIndex = j;
				break;
			}else{
				randomBonusIndex -= bonus.weight;
			}
		}

		bonusList[bonusIndex].bonus.Effect(target);
		Debug.Log("You get the "+bonusList[bonusIndex].bonus.bonusName+" bonus !");

		Destroy(this.gameObject);
	}

	[Serializable]
    public struct Bonus{
        public TemporaryBonusScript bonus;
        public int weight;
    }
}
