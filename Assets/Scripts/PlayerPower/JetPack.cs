using System.Collections;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    public float forceJetpack = 2f;
    public float dureeJetpack = 3f;
    public float delaiReutilisationJetpack = 25f;

    private float tempsRestantJetpack = 0f;
    private bool estJetpackActif = false;

    private Rigidbody rb;
    private CharacterController characterController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (tempsRestantJetpack <= 0f && !estJetpackActif)
            {
                // Activer le jetpack s'il n'est pas déjà actif
                estJetpackActif = true;
                StartCoroutine(ActiverJetpack());
            }
        }

        // Si le jetpack est actif et que le temps restant est supérieur à zéro, appliquer la force du jetpack
        if (estJetpackActif && tempsRestantJetpack > 0f)
        {
            Vector3 forceJetpackVector = Vector3.up * forceJetpack;
            rb.AddForce(forceJetpackVector, ForceMode.Force);
        }
    }

    private IEnumerator ActiverJetpack()
    {
        // Appliquer la force du jetpack pendant la durée
        tempsRestantJetpack = dureeJetpack;
        while (tempsRestantJetpack > 0f)
        {
            yield return new WaitForSeconds(1f);
            tempsRestantJetpack--;
        }

        // Délai avant que le jetpack puisse être réutilisé
        yield return new WaitForSeconds(delaiReutilisationJetpack);
        estJetpackActif = false;
    }
}