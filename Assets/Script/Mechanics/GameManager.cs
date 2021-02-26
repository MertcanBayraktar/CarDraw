using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
            Debug.Log("More than one instance of GameManager in scene");
        instance = this;
    }
    #endregion
    public GameObject playButton;
    [HideInInspector]

    public enum GameState
    {
        play = 0,
        stop = 1,
        finish = 3
    }
    public Transform finishPoint;
    public Transform startPosition;
    public GameState gameState;
    public CarBase car;
    public GameObject endLevelPanel;
    public GameObject uiDrawPanel;
    public GameObject F_R;
    public GameObject B_R;
    void Start()
    {
        gameState = GameState.stop;
    }
    public void StartGame()
	{
        gameState = GameState.play;
        playButton.SetActive(false);

	}
    public void RestartGame()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void FinishLevel()
	{
        Car.instance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Camera.main.GetComponent<CameraFollow>().enabled = false;
        Camera.main.transform.DOMove(finishPoint.position, 3f);
        Camera.main.transform.DORotate(new Vector3(0,-90,0), 3f,RotateMode.Fast);
        F_R.SetActive(false);
        B_R.SetActive(false);
        uiDrawPanel.SetActive(false);
        endLevelPanel.SetActive(true);
        
}
}
