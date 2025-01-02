using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	[SerializeField]
	protected int currentTier = 0;

	[SerializeField]
	protected int buyCost = 100;
}
