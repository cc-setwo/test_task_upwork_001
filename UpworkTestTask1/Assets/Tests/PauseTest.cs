using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PauseTest
    {
        private const float APPROX_VALUE = 0.00000001f;
        
        [UnityTest]
        public IEnumerator PauseTestWithEnumeratorPasses()
        {
            // Get the controller and spawn some cubes
            CubeController controller = Object.Instantiate(Resources.Load<CubeController>("TestPrefabs/CubeController"));
            for (int i = 0; i < 10; i++)
            {
                controller.SpawnCubes();
                yield return null;
            }
            
            // Pause the game
            controller.PauseGame();
            
            // Wait for the pause for 2 frames
            yield return null;
            yield return null;

            // Get all the cubes
            Cube[] allCubes = Object.FindObjectsOfType<Cube>();
            List<Vector3> previousFramePositions = new List<Vector3>();

            // If there is some cubes, get their positions and add them to a list to compare later
            for (int i = 0; i < allCubes.Length; i++)
            {
                previousFramePositions.Add(allCubes[i].transform.position);
            }
            
            // Skip the frame to check the positions on the next frame
            yield return null;

            // Get all positions again and compare to a list, if at least one position is not equal to previous one
            // Mark test as failed!
            for (int i = 0; i < allCubes.Length; i++)
            {
                if (Vector3.Distance(allCubes[i].transform.position, previousFramePositions[i]) >= APPROX_VALUE)
                {
                    Assert.Fail();
                }
            }
        }
    }
}
