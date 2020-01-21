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
	}

	public enum CommonEvent
	{
		GetOwner,
		SetTarget,
		EnemySpawned,
		StopSpawning,
	}
}
