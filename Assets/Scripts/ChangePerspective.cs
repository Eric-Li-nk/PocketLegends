using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangePerspective : MonoBehaviour
{
    [SerializeField] private Transform playerCameras;
    [SerializeField] private Transform carCameras;

    public void OnChangePerspective(InputAction.CallbackContext context)
    {
        foreach (Transform camera in playerCameras)
            camera.gameObject.SetActive(!camera.gameObject.activeSelf);
        foreach (Transform camera in carCameras)
            camera.gameObject.SetActive(!camera.gameObject.activeSelf);
    }
}
