using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownObjects : MonoBehaviour
{
    public float timeBeforeRespawn = 5f; // Temps avant que l'objet réapparaisse en secondes
    private bool isRespawning = false; // Booléen pour savoir si l'objet est en train de réapparaître
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si la voiture a entré en collision
        {
            VehicleMovement vehicle = other.GetComponent<VehicleMovement>(); // Récupère le script VehicleMovement attaché à la voiture
            if (vehicle != null)
            {
                vehicle.motorTorque /= 2f; // Divise la valeur de motorTorque par 2 pour ralentir la voiture
                gameObject.SetActive(false); // Désactive l'objet
                StartCoroutine(RespawnObject()); // Lance la coroutine de réapparition de l'objet
            }
        }
    }
    
    private IEnumerator RespawnObject()
    {
        isRespawning = true; // Met le booléen à vrai pour indiquer que l'objet est en train de réapparaître
        yield return new WaitForSeconds(timeBeforeRespawn); // Attend le temps spécifié
        gameObject.SetActive(true); // Réactive l'objet
        isRespawning = false; // Remet le booléen à faux
    }
    
    private void Update()
    {
        // Empêche le joueur de toucher l'objet pendant qu'il est en train de réapparaître
        if (isRespawning)
        {
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false; // Désactive le collider de l'objet
            }
        }
        else
        {
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true; // Réactive le collider de l'objet
            }
        }
    }
}
