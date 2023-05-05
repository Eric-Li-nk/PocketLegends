using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangePerspective : MonoBehaviour
{
    [SerializeField] private GameObject firstPersonCam;
    [SerializeField] private GameObject thirdPersonCam;

    public void onChangePerspective(InputAction.CallbackContext context)
    {
        firstPersonCam.SetActive(!firstPersonCam.activeSelf);
        thirdPersonCam.SetActive(!thirdPersonCam.activeSelf);
    }
}
