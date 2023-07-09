using System;
using UnityEngine;

public enum BonusType
{
    Bonus,
    Inverse,
    Slow
}

public class CanvaFuse : MonoBehaviour
{
    private GameObject UiObject;
    private GameObject additionalUiObject;
    public BonusType bonusType;
    public GameObject cube;
    private bool triggerExited = false;
    private bool resetTimerActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (bonusType)
            {
                case BonusType.Bonus:
                    UiObject = other.transform.parent.Find("bonus").Find("fuse").gameObject;
                    break;
                case BonusType.Inverse:
                    UiObject = other.transform.parent.Find("bonus").Find("inverse").gameObject;
                    break;
                case BonusType.Slow:
                    UiObject = other.transform.parent.Find("bonus").Find("slow").gameObject;
                    break;
            }
            
            additionalUiObject = other.transform.parent.Find("bonus").Find("none").gameObject;
            
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