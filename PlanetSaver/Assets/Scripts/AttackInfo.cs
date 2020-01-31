using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInfo
{
        public GameObject initiator;        //Quel personnage a initié cette attaque
        public GameObject sender;           //De quel entité provient l'event de l'attaque
        public GameObject target;           //La cible de cette attaque
        public float damage;                //Combien de dégâts inflige cette attaque
        public string[] attackAttributes;   //Quels sont les attributs de cette attaque

        public AttackInfo(GameObject _initiator, GameObject _sender, GameObject _target, float _damage, string _attackAttributes){
            initiator = _initiator;
            sender = _sender;
            target = _target;
            damage = _damage;
            attackAttributes = _attackAttributes.Split(',');
        }

        public void AdjustDamage(float value){
            damage -= value;
            if(damage < 0){
                damage = 0;
            }
        }
}
