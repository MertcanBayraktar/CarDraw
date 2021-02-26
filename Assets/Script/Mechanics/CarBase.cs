using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CarBase : MonoBehaviour
{

    public Text goldText;
    public bool isStart;
    public Transform frontW;
    public Transform backW;
    [SerializeField]public Vector3 lastCheckpoint;
    [SerializeField] private float FallingTimer;
    [SerializeField] private float checkpointTimer = 5f;
    public WheelCollider[] wheelColliders;
    [HideInInspector]
    public Rigidbody rb;
    private Car car; 
    private void Start()
    {
        car = GetComponent<Car>();
        goldText.text = car.goldTotal.ToString();
        rb = GetComponent<Rigidbody>();
    }
    public void FixedUpdate()
    {
        if (GameManager.instance.gameState == GameManager.GameState.play)
        {
            if (rb.velocity.magnitude < car.setCarMaxSpeed)
            {
                foreach (var item in wheelColliders)
                {
                    item.motorTorque = Time.deltaTime * car.setTorque;
                }
                if (IsGround())
                {
                    rb.AddForce(Vector3.right * Time.deltaTime * car.setPower, ForceMode.Acceleration);
                    FallingTimer = 5;
                }
            }
            if (rb.angularVelocity.magnitude > 5)
            {
                rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, Time.deltaTime * 10);
            }
            checkpointTimer -= Time.deltaTime;
            if(checkpointTimer <=0 && IsGround())
			{
                lastCheckpoint = this.transform.position;
                checkpointTimer = 5f;
			}
        }
		if (!IsGround())
		{
            FallingTimer -= Time.deltaTime;
            if (FallingTimer < 0)
            {
                if(lastCheckpoint !=null)
                    this.transform.position = lastCheckpoint;
            }
		}
    }
    private bool IsGround()
    {
        foreach (var item in wheelColliders)
        {
            if (!item.isGrounded)
            {
                return false;
            }
        }
        return true;
    }
    public void Break()
    {
        foreach (var item in wheelColliders)
        {
            item.brakeTorque = 10;
        }
    }
	private void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "coin")
		{
            Destroy(other.gameObject);
            car.addCoin();
            UpdateGoldText();
		}
	}
    public void UpdateGoldText()
	{
        goldText.text = car.goldTotal.ToString();
	}
}
