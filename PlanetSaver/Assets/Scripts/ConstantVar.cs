public class ConstantVar
{
	#region "Events"

		//MovementEvent
		public const string DESTINATION_REACH = "DestinationReach";

		//ActionEvent
		public const string USE_ATTACK = "UseAttack";
		public const string USE_SKILL = "UseSkill";
		public const string DO_DAMAGE = "DoDamage";
		public const string HEAL = "Heal";
		public const string TAKE_DAMAGE = "TAKE_DAMAGE";
		public const string COLLECTIBLE_PICKEDUP = "CollectiblePickedUp";
		public const string COLLECT_COIN = "CollectCoin";
		public const string CREATE_BULLET = "CreateBullet";
		public const string BOOST_WEAPON = "BoostWeapon";
		public const string BOOST_SPEED = "BoostSpeed";
		public const string FIND_PRIORITY_TARGET = "FindPriorityTarget";
		public const string SET_PRIORITY_TARGET = "SetPriorityTarget";
		public const string RESET_PRIORITY_TARGET = "ResetPriorityTarget";
		public const string COLLIDE_WITH_SOMETHING = "CollideWithSomething";
		public const string BLOCK_MOVEMENT = "BlockMovement";
		public const string MIND_CONTROL = "MindControl";
		public const string RESET_MIND_CONTROL = "ResetMindControl";

		//CommonEvent
		public const string GET_OWNER = "GetOwner";
		public const string SET_TARGET = "SetTarget";
		public const string ENEMY_SPAWNED = "EnemySpawned";
		public const string STOP_SPAWNING = "StopSpawning";
		public const string REGISTER_DEFENSIVE_MODIFIER = "RegisterDefensiveModifier";
		public const string GET_HEALTH = "GetHealth";
		public const string SEND_HEALTH = "SendHealth";
		public const string IS_DEAD = "IsDead";
		public const string ON_SENSOR_ENTER = "OnSensorEnter";
		public const string ON_SENSOR_EXIT = "OnSensorExit";
	#endregion

	#region "Attack Attributes"
		public const string ATK_ATR_COLLISION = "Collision";
		public const string ATK_ATR_EXPLOSION = "Explosion";
		public const string ATK_ATR_POISON = "Poison";
		public const string ATK_ATR_PIERCING = "Piercing";
	#endregion

	#region "Buff Names"
		public const string BUFF_MAGNET = "magnet";
		public const string BUFF_TAUNT = "taunt";
		public const string BUFF_DAMAGE = "damage";
		public const string BUFF_FIRERATE = "firerate";
	#endregion
}
