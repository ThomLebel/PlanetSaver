using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class DamageHandlerScript : MonoBehaviour
{
    private int attackReceived = 0;
    private List<AttackInfo> attackInfoList;

    // Start is called before the first frame update
    void Start()
    {
        attackInfoList = new List<AttackInfo>();

        EventManager.StartListening(ConstantVar.DO_DAMAGE, this.gameObject, TakeDamage);
        EventManager.StartListening(ConstantVar.ATTACK_MODIFIER_COUNT, this.gameObject, CountAttackModifier);
    }

    private void TakeDamage(){
        var eventData = EventManager.GetIndexedDataGroup(ConstantVar.DO_DAMAGE);

        AttackInfo attackInfo = new AttackInfo(
            attackReceived,
            eventData.ToGameObject("initiator"),
            eventData.ToGameObject("sender"),
            eventData.ToFloat("damage"),
            eventData.ToString("attackTypes")
        );
        attackInfoList.Add(attackInfo);

        attackReceived ++;

        //Checker quels composants peuvent modifier cette attaque
        EventManager.SetIndexedDataGroup(ConstantVar.CAN_MODIFY_ATTACK,
            new EventManager.DataGroup{id = "id", data = attackInfo.id},
            new EventManager.DataGroup{id = "types", data = eventData.ToString("attackTypes")}
        );
        EventManager.EmitEvent(ConstantVar.CAN_MODIFY_ATTACK, this.gameObject);
    }

    private void CountAttackModifier(){
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.ATTACK_MODIFIER_COUNT);
        if(sender == null || sender != gameObject){
            return;
        }

        var eventData = EventManager.GetIndexedDataGroup(ConstantVar.ATTACK_MODIFIER_COUNT);
        int attackID = eventData.ToInt("id");
        AttackInfo attackInfo = attackInfoList.Find(x => x.id == attackID);
        attackInfo.personnalAttackModifier ++;
    }

    public struct AttackInfo{
        public int id;
        public GameObject initiator;        //Quel personnage a initié cette attaque
        public GameObject sender;           //De quel entité provient l'event de l'attaque
        public float damage;                //Combien de dégâts inflige cette attaque
        public string[] attackTypes;        //Quels sont les types de cette attaque
        public int personnalAttackModifier; //Combien de composants réagissent à cette attaque

        public AttackInfo(int _id, GameObject _initiator, GameObject _sender, float _damage, string _attackTypes){
            id = _id;
            initiator = _initiator;
            sender = _sender;
            damage = _damage;
            attackTypes = _attackTypes.Split(',');
            personnalAttackModifier = 0;
        }
    }
}
