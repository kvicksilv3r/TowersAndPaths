using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
	private Vector3 dirToTarget;

	[SerializeField]
	protected float distanceForNewTarget = 0.1f;

	private float sqrDistanceForNewTarget;

	private bool isActive = true;

	private float distanceTraveled = 0;

	private bool countDistance = false;
	public float DistanceTraveled { get => distanceTraveled; protected set => distanceTraveled = value; }

	public virtual void Start()
	{
		WaveController.Instance.RegisterEnemy(this);
		currentTarget = WaypointController.Instance.GetFirstWaypoint();
		sqrDistanceForNewTarget = Mathf.Pow(distanceForNewTarget, 2);
		GetTargetDirection();
	}

	private void FetchNextTarget()
	{
		if (!countDistance)
		{
			countDistance = true;
		}

		currentTarget = currentTarget.GetNextWaypoint();

		if (currentTarget)
		{
			GetTargetDirection();
		}

		else
		{
			PathComplete();
		}
	}

	private void GetTargetDirection()
	{
		dirToTarget = currentTarget.transform.position - transform.position;
		dirToTarget.Normalize();
	}

	public virtual void Update()
	{
		if (!isActive)
		{
			return;
		}

		var distanceToTravel = dirToTarget * movementSpeed * Time.deltaTime;

		transform.position += distanceToTravel;

		if (countDistance)
		{
			distanceTraveled += distanceToTravel.sqrMagnitude;
		}

		CheckNewTarget();
	}

	private void CheckNewTarget()
	{
		if (DistanceToTarget() < sqrDistanceForNewTarget)
		{
			FetchNextTarget();
		}
	}

	private void PathComplete()
	{
		isActive = false;
		print("Explosion, you dead rip riprip");
		Destroy(gameObject);
	}

	private float DistanceToTarget()
	{
		return (transform.position - currentTarget.transform.position).sqrMagnitude;
	}

	private void OnDestroy()
	{
		WaveController.Instance.RemoveEnemy(this);
	}
}
