using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float transitionTime = 0.4f;

    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private Coroutine transitionRoutine;

    void Awake()
    {
        // Store the default camera position and rotation
        defaultPosition = mainCamera.transform.position;
        defaultRotation = mainCamera.transform.rotation;
    }

    public void FocusOnTower(Transform tower)
    {
        Debug.Log("Camera focusing on tower: " + tower.name);
        if (transitionRoutine != null)
        StopCoroutine(transitionRoutine);

        // Camera position
        Vector3 offset =
            Vector3.back * 4f
            + Vector3.right * 3f
            + Vector3.up * 3f;

        Vector3 targetPosition = tower.position + offset;

        // Look point is NOT the tower center
        Vector3 lookTarget =
            tower.position;

        Quaternion targetRotation =
            Quaternion.LookRotation(lookTarget - targetPosition);

        transitionRoutine = StartCoroutine(
            MoveCamera(targetPosition, targetRotation)
        );
    }

    public void ResetCamera()
    {
        if (transitionRoutine != null)
            StopCoroutine(transitionRoutine);

        transitionRoutine = StartCoroutine(
            MoveCamera(defaultPosition, defaultRotation)
        );
    }

    private IEnumerator MoveCamera(Vector3 targetPos, Quaternion targetRot)
    {
        Vector3 startPos = mainCamera.transform.position;
        Quaternion startRot = mainCamera.transform.rotation;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / transitionTime;

            mainCamera.transform.position =
                Vector3.Lerp(startPos, targetPos, t);

            mainCamera.transform.rotation =
                Quaternion.Slerp(startRot, targetRot, t);

            yield return null;
        }
    }
}
