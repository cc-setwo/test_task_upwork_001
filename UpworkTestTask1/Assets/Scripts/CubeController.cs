using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private const float CUBE_WAIT_TIME = 2.0f;
    
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private List<Transform> points;

    private bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
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
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = !isPaused ? 0.0f : 1.0f;
            isPaused = !isPaused;
        }
    }
}
