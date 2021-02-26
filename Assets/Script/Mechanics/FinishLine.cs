using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			GameManager.instance.gameState = GameManager.GameState.finish;
			GameManager.instance.FinishLevel();
		}		
	}
}
