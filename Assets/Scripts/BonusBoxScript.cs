using System.Collections;
using UnityEngine;

public class BonusBoxScript : MonoBehaviour
{
    public float speedBoostDuration = 5f; // Durée du bonus de vitesse en secondes
    public float deactivateTime = 3f; // Temps en secondes avant de désactiver la sphère bonus

    private bool isActive = true; // Indique si la boîte bonus est active ou non

    // La méthode OnTriggerEnter est appelée lorsque l'objet entre en collision avec un autre objet
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isActive)
        {
            StartCoroutine(ApplySpeedBoost(other.gameObject));
            isActive = false; // désactiver la boîte bonus une fois qu'elle a été collectée
            
            // Désactiver l'objet BonusBox et jouer une animation de particules
            gameObject.SetActive(false);
            GetComponent<ParticleSystem>().Play();
            
            // Réactiver l'objet BonusBox après un certain temps
            Invoke("Reactivate", deactivateTime);
        }
    }
    
    // La méthode Reactivate réactive la sphère bonus
    private void Reactivate()
    {
        isActive = true; // réactiver la boîte bonus
        gameObject.SetActive(true); // réactiver l'objet BonusBox
    }

    // La méthode ApplySpeedBoost augmente la vitesse du joueur pendant la durée spécifiée
    private IEnumerator ApplySpeedBoost(GameObject player)
    {
        float originalTimeScale = Time.timeScale;
        float originalSpeed = player.GetComponent<VehicleMovement>().motorTorque;
        player.GetComponent<VehicleMovement>().motorTorque *= 2f; // multiplier la vitesse du joueur par 2
        Time.timeScale = 1.5f; // accélérer le temps dans le jeu
        yield return new WaitForSecondsRealtime(speedBoostDuration); // attendre la durée spécifiée
        player.GetComponent<VehicleMovement>().motorTorque = originalSpeed; // réinitialiser la vitesse du joueur
        Time.timeScale = originalTimeScale; // réinitialiser le temps dans le jeu
    }
}
