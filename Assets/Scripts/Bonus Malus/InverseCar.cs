using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseCar : MonoBehaviour
{
    public float timeBeforeEnd = 5f; // Temps avant la fin du malus en secondes
    private bool isMalusActive = false; // Booléen pour savoir si le malus est actif
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si la voiture a entré en collision
        {
            VehicleMovement vehicle = other.GetComponent<VehicleMovement>(); // Récupère le script VehicleMovement attaché à la voiture
            if (vehicle != null)
            {
                StartCoroutine(ActivateMalus(vehicle)); // Lance la coroutine d'activation du malus sur la voiture
            }
        }
    }
    
    private IEnumerator ActivateMalus(VehicleMovement vehicle)
    {
        isMalusActive = true; // Met le booléen à vrai pour indiquer que le malus est actif
        float originalMaxSteer = vehicle.maxSteer; // Stocke la valeur originale de maxSteer
        vehicle.maxSteer = -originalMaxSteer; // Inverse la valeur de maxSteer pour inverser les contrôles de direction
        yield return new WaitForSeconds(timeBeforeEnd); // Attend le temps spécifié
        vehicle.maxSteer = originalMaxSteer; // Rétablit la valeur originale de maxSteer
        isMalusActive = false; // Remet le booléen à faux
    }
    
    private void Update()
    {
        // Empêche le joueur de toucher le malus pendant qu'il est actif
        if (isMalusActive)
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