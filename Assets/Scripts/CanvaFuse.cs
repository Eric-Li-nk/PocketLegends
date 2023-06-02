using UnityEngine;

public class CanvaFuse : MonoBehaviour
{
    public GameObject UiObject;
    public GameObject additionalUiObject;
    public GameObject cube;
    private bool triggerExited = false;
    private bool resetTimerActive = false;

    private void Start()
    {
        UiObject.SetActive(false);
        additionalUiObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UiObject.SetActive(true);
            triggerExited = false;

            if (resetTimerActive)
            {
                resetTimerActive = false; // Annuler le compte à rebours de réinitialisation s'il était actif
                CancelInvoke("ResetCanvas");
            }

            Invoke("ActivateAdditionalUi", 5f); // Déclencher l'activation de l'additionalUiObject après 5 secondes
        }
    }

    private void ActivateAdditionalUi()
    {
        if (!triggerExited)
        {
            additionalUiObject.SetActive(true);
            resetTimerActive = true;
            Invoke("ResetCanvas", 5f); // Déclencher la réinitialisation après 5 secondes
        }
    }

    private void ResetCanvas()
    {
        resetTimerActive = false;

        if (!triggerExited)
        {
            UiObject.SetActive(false);
            additionalUiObject.SetActive(false); // Désactiver additionalUiObject
            triggerExited = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        triggerExited = true;
    }
}