public class EventsNames
{
    public enum MovementEvent
	{
		DestinationReach,
	}

	public enum ActionEvent
	{
		UseAttack,
		UseSkill,
		DoDamage,
		Heal,
	}

	public enum CommonEvent
	{
		GetOwner,
		SetTarget,
		EnemySpawned,
		StopSpawning,
		CollectiblePickedUp,
		CollectCoin,
		CreateBullet,
	}
}
