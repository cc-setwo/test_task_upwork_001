using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public static class CubeMovement
    {
        private const float MOVE_TIME = 2.0f;
        
        public static void CubeWait(this MonoBehaviour cube, float waitTime, Action callback = null)
        {
            cube.StartCoroutine(CubeWaitCour(waitTime, callback));
        }
        
        public static void CubeMove(this MonoBehaviour cube, Vector3 finalPos, Action callback = null)
        {
            cube.StartCoroutine(CubeMovementCour(cube.transform, finalPos, callback));
        }

        private static IEnumerator CubeWaitCour(float time, Action callback)
        {
            float elapsedTime = 0;
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            callback?.Invoke();
        }

        private static IEnumerator CubeMovementCour(Transform cube, Vector3 finalPos, Action callback)
        {
            float elapsedTime = 0;
            Vector3 startingPos = cube.transform.position;
            while (elapsedTime < MOVE_TIME)
            {
                cube.transform.position = Vector3.Lerp(startingPos, finalPos, elapsedTime / MOVE_TIME);
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            
            cube.transform.position = finalPos;
            callback?.Invoke();
        }
    }
}