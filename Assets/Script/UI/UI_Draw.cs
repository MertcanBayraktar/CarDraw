﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using ProBuilder2.Common;
public class UI_Draw : MonoBehaviour 
{
    private TouchEvent touch;
    private List<Vector2> touchPositions = new List<Vector2>();
    public UILineTextureRenderer linerenderer;
    public float twoPointMaxDis = 75f;
    public Material material;
    [HideInInspector]
    public GameObject currentDrawcar;
    private RectTransform rectTransform;
	public GameObject road;
    
    private void Start()
    {
        touch = GetComponent<TouchEvent>();
        rectTransform = transform as RectTransform;
    }
    private void Update()
    {
        if (GameManager.instance.gameState == GameManager.GameState.play)
        {
            if (touch.isEvent)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    FirstLinePoint();
                }
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    UpdateLine();
                }
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    if (touchPositions.Count > 5)
                    {
						CreateCar(touchPositions);
					}
                    else
                    {
                        Time.timeScale = 1;
                    }
                }
            }
        }
    }
    private void FirstLinePoint()
    {
        Time.timeScale = 0.3f;
        touchPositions.Clear();
        Vector2 firstPosition = touch.startPosition.InversePoint(linerenderer.transform);
        touchPositions.Add(firstPosition);
        linerenderer.Points = touchPositions.ToArray();
    }
    private void UpdateLine()
    {
        Vector2 touchPos = touch.currentPosition.InversePoint(linerenderer.transform);

        var heightClamp = rectTransform.rect.height / 2.2f;
		var widthClamp = myCanvas.canvasSize.x / 1.2f;
		touchPos = new Vector2(Mathf.Clamp(touchPos.x, widthClamp * -1f, widthClamp), Mathf.Clamp(touchPos.y, heightClamp * -1f, heightClamp));

        float distance = Vector2.Distance(touchPositions[touchPositions.Count - 1], touchPos);
        if (distance > twoPointMaxDis)
        {
            touchPositions.Add(touchPos);
            linerenderer.Points = touchPositions.ToArray();
        }
    }
	private void CreateCar(List<Vector2> points)
	{
		GameObject createCar = new GameObject();
		var carObject = createCar.AddComponent<pb_Object>();
		createCar.AddComponent<pb_Entity>();
		pb_BezierShape pathObject = createCar.AddComponent<pb_BezierShape>();
		List<pb_BezierPoint> beizers = BeizerListCreate(points);
		pathObject.m_Points = beizers;
		pathObject.m_Radius = 25;
		pathObject.Refresh();
		createCar.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
		carObject.ToMesh();
		Mesh m = createCar.GetComponent<MeshFilter>().sharedMesh;
		MeshRenderer mr = createCar.GetComponent<MeshRenderer>();
		MeshCollider mc = createCar.AddComponent<MeshCollider>();
		mc.sharedMesh = m;
		mc.convex = true;
		mr.sharedMaterial = material;
		createCar.transform.parent = GameManager.instance.car.transform;
		createCar.transform.position = GameManager.instance.car.transform.position;
		WheelPlacement(createCar.transform, pathObject);
		NewDrawCarPlace(createCar);
		CarReset();
		Time.timeScale = 1;
	}
	private List<pb_BezierPoint> BeizerListCreate(List<Vector2> points)
	{
		List<pb_BezierPoint> beizerPoints = new List<pb_BezierPoint>();
		for (int i = 0; i < points.Count; i++)
		{
			beizerPoints.Add(new pb_BezierPoint(points[i], points[i], points[i], Quaternion.identity));
		}
		return beizerPoints;
	}
	private void NewDrawCarPlace(GameObject cCar)
	{
		if (currentDrawcar != null)
			Destroy(currentDrawcar);
		currentDrawcar = cCar;
	}
	public void WheelPlacement(Transform parent, pb_BezierShape pathObject)
	{
		Vector3 frontPos = pathObject.m_Points[pathObject.m_Points.Count - 1].position * parent.localScale.x;
		frontPos.z = frontPos.x;
		frontPos.x = 0;
		Vector3 backPos = pathObject.m_Points[0].position * 0.005f;
		backPos.z = backPos.x;
		backPos.x = 0;
		GameManager.instance.car.frontW.localPosition = frontPos;
		GameManager.instance.car.backW.localPosition = backPos;
	}
	private void CarReset()
	{
		GameManager.instance.car.isStart = true;
		GameManager.instance.car.rb.useGravity = true;
		GameManager.instance.car.rb.angularVelocity = Vector3.zero;
		Vector3 carMainRot = GameManager.instance.car.transform.localEulerAngles;
		carMainRot.z = 0;
		carMainRot.y = 90;
		carMainRot.x = 0;
		GameManager.instance.car.transform.localEulerAngles = carMainRot;
		Vector3 newCarPos = road.transform.localPosition; // olmazsa levelmanager implement et
		newCarPos.y += 5;
		newCarPos.x = GameManager.instance.car.transform.position.x;
		GameManager.instance.car.transform.position = newCarPos;
		Vector3 carRot = currentDrawcar.transform.localEulerAngles;
		carRot.z = 0;
		carRot.y = -90;
		carRot.x = 0;
		currentDrawcar.transform.localEulerAngles = carRot;
	}
}
