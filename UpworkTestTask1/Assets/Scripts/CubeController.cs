using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class CubeController : MonoBehaviour
{
    private const string CUBE_AMOUNT_TEXT = "Current amount of cubes is: {0}";
    private const string PAUSE_TEXT = "Pause";
    private const string UNPAUSE_TEXT = "Unpause";
    private const float CUBE_WAIT_TIME = 2.0f;
    
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private Text cubeText;
    [SerializeField] private Text pauseText;
    [SerializeField] private GameObject pauseObj;
    [SerializeField] private List<Transform> points;

    private int cubesAmount;
    private bool isPaused;

    public void PauseGame()
    {
        Time.timeScale = !isPaused ? 0.0f : 1.0f;
        isPaused = !isPaused;
        pauseObj.SetActive(isPaused);
        pauseText.text = !isPaused ? PAUSE_TEXT : UNPAUSE_TEXT;
    }

    public void SpawnCubes()
    {
        CubeSequence cubeSequenceForward = new CubeSequence();
        CubeSequence cubeSequenceBackward = new CubeSequence();
            
        cubeSequenceForward.SetCallback(cubeSequenceBackward.Execute);
        cubeSequenceBackward.SetCallback(cubeSequenceForward.Execute);
            
        Cube newCube = Instantiate(cubePrefab);
        newCube.transform.position = points[0].position;

        cubeSequenceForward.Add(callback => newCube.CubeMove(points[1].position, callback));
        cubeSequenceForward.Add(callback => newCube.CubeWait(CUBE_WAIT_TIME, callback));
        cubeSequenceForward.Add(callback => newCube.CubeMove(points[2].position, callback));
            
        cubeSequenceBackward.Add(callback => newCube.CubeMove(points[1].position, callback));
        cubeSequenceBackward.Add(callback => newCube.CubeWait(CUBE_WAIT_TIME, callback));
        cubeSequenceBackward.Add(callback => newCube.CubeMove(points[0].position, callback));
            
        cubeSequenceForward.Execute();
        cubesAmount++;
        UpdateText();
    }
    
    private void Start()
    {
        UpdateText();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnCubes();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    private void UpdateText()
    {
        cubeText.text = string.Format(CUBE_AMOUNT_TEXT, cubesAmount);
    }
}
