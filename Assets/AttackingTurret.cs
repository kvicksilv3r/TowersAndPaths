using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TurretTargetMode
{
	First,
	Closest,
	Last
}

public class AttackingTurret : Turret
{
	[SerializeField]
	protected float baseAttacksPerSecond = 1;

	[SerializeField]
	protected TurretTargetMode targetMode;

	[SerializeField]
	protected float attackRange = 5;

	[SerializeField]
	private int attackDamage = 5;

	protected MovingEnemy currentTarget;

	protected float sqrAttackRange;
	protected float timeBetweenAttacks;
	protected float lastShot;

	public virtual void Start()
	{
		lastShot = Time.time;
		SetTimeBetweenAttacks();
		sqrAttackRange = Mathf.Pow(attackRange, 2);
	}

	protected virtual void SetTimeBetweenAttacks()
	{
		timeBetweenAttacks = 1 / baseAttacksPerSecond;
	}


	private void Update()
	{
		FetchTarget();

		if (Input.GetKeyDown(KeyCode.Space))
		{
			FetchTarget();
		}

		ValidateTargetInRange();
	}

	private void ValidateTargetInRange()
	{
		if (!currentTarget) { return; }

		if ((currentTarget.transform.position - transform.position).sqrMagnitude > sqrAttackRange)
		{
			currentTarget = null;
		}
	}

	private void FetchTarget()
	{
		var enemies = WaveController.Instance.GetEnemies;
		var inRange = enemies.Where(e => (e.transform.position - transform.position).sqrMagnitude <= sqrAttackRange);

		if (inRange.Count() <= 0)
		{
			return;
		}

		switch (targetMode)
		{
			case TurretTargetMode.First:
				currentTarget = inRange.OrderBy(e => e.DistanceTraveled).Last();
				break;
			case TurretTargetMode.Closest:
				currentTarget = inRange.OrderBy(e => (e.transform.position - transform.position).sqrMagnitude).FirstOrDefault();
				break;
			case TurretTargetMode.Last:
				currentTarget = inRange.OrderBy(e => e.DistanceTraveled).First();
				break;
		}

	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, attackRange);

		if (currentTarget)
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawWireCube(currentTarget.transform.position, Vector3.one * 1.5f);
		}
	}
}
