using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
	#region Singleton
	public static Car instance;
	private void Awake()
	{
		if (instance != null)
			Debug.Log("More than one car script in scene");
		instance = this;
	}
	#endregion
	public CarBase cm;
	private int goldCoin = 10;
	[SerializeField]private float torquePower = 1500;
	[SerializeField] private float power = 750;
	[SerializeField] private float carMaxSpeed=28;
	public int goldTotal
	{
		get { return goldCoin; }
		set { goldCoin = value; }
	}
	public float setCarMaxSpeed
	{
		get { return carMaxSpeed; }
		set { carMaxSpeed = value; }
	}
	public float setPower
	{
		get { return power = 750;; }
		set { power = value; }
	}
	public float setTorque
	{
		get { return torquePower; }
		set { torquePower = value; }
	}
	public void BuySpeedPerk(int value)
	{
		if (goldCoin >= 3)
		{
			goldTotal -= 3;
			value = 5;
			setCarMaxSpeed += value;
			cm.UpdateGoldText();
		}
	}
	public void BuyTorkPower(int value)
	{
		if (goldCoin >= 3)
		{
			goldTotal -= 3;
			value = 50;
			setTorque += value;
			cm.UpdateGoldText();
		}
	}
	public void BuyHorsePower(int value)
	{
		if (goldCoin >= 3)
		{
			goldTotal -= 3;
			value = 10;
			setPower += value;
			cm.UpdateGoldText();
		}
	}
	public void addCoin()
	{
		goldTotal += 1;
	}

}
