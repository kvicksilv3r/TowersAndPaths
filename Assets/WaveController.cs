using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{

	public static WaveController Instance;

	public List<MovingEnemy> activeEnemies = new List<MovingEnemy>();

	private void Awake()
	{
		Instance = this;
	}

	public List<MovingEnemy> GetEnemies
	{
		get { return activeEnemies; }
	}

	public void RegisterEnemy(MovingEnemy enemy)
	{
		activeEnemies.Add(enemy);
	}

	public void RemoveEnemy(MovingEnemy enemy)
	{
		activeEnemies.Remove(enemy);
		//activeEnemies = activeEnemies.Where(item => item != null).ToList();
	}
}
