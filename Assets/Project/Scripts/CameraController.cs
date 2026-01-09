using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float transitionTime = 0.4f;

    [SerializeField] private Vector3 followOffset = new Vector3(0, 6, -6);
    [SerializeField] private float followSmoothSpeed = 6f;

    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private Coroutine transitionRoutine;

    public Transform followTarget;


    void Awake()
    {
        // Store the default camera position and rotation
        defaultPosition = mainCamera.transform.position;
        defaultRotation = mainCamera.transform.rotation;
    }

    void LateUpdate()
    {
        // Follow hero if one is set
        if (followTarget == null)
            return;

        Vector3 desiredPosition = followTarget.position + followOffset;

        mainCamera.transform.position = Vector3.Lerp(
            mainCamera.transform.position,
            desiredPosition,
            followSmoothSpeed * Time.deltaTime
        );

        Quaternion targetRotation =
            Quaternion.LookRotation(followTarget.position - mainCamera.transform.position);

        mainCamera.transform.rotation = Quaternion.Slerp(
            mainCamera.transform.rotation,
            targetRotation,
            followSmoothSpeed * Time.deltaTime
        );
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
