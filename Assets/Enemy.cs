using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
	Ground,
	Flying,
	Stealth
}

public class Enemy : MonoBehaviour
{
	[SerializeField]
	protected WaypointPoint currentTarget;

	[SerializeField]
	protected EnemyType type;

	[SerializeField]
	protected float movementSpeed = 1;

	[SerializeField]
	protected float health = 10;

	[SerializeField]
	protected float damage = 1;
}
