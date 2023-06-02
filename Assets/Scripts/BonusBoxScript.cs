using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BonusBoxScript : MonoBehaviour
{
    public float speedBoostDuration = 5f; // Durée du bonus de vitesse en secondes
    public float deactivateTime = 3f; // Temps en secondes avant de désactiver la sphère bonus
    public Image iconImage; // Référence à l'icône à afficher

    private bool isActive = true; // Indique si la boîte bonus est active ou non

    private void Awake()
    {
        // Rechercher l'Image dans la scène
        iconImage = GameObject.Find("fusee.png").GetComponent<Image>();
    }

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
            StartCoroutine(ReactivateAfterDelay(deactivateTime));
        }
    }

    private IEnumerator ReactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isActive = true; // réactiver la boîte bonus
        gameObject.SetActive(true); // réactiver l'objet BonusBox
    }

    private IEnumerator ApplySpeedBoost(GameObject player)
    {
        float originalTimeScale = Time.timeScale;
        float originalSpeed = player.GetComponent<VehicleMovement>().motorTorque;
        player.GetComponent<VehicleMovement>().motorTorque *= 2f; // multiplier la vitesse du joueur par 2
        Time.timeScale = 1.5f; // accélérer le temps dans le jeu

        // Activer l'icône
        iconImage.enabled = true;

        yield return new WaitForSecondsRealtime(speedBoostDuration); // attendre la durée spécifiée

        // Réinitialiser la vitesse du joueur
        player.GetComponent<VehicleMovement>().motorTorque = originalSpeed;

        // Réinitialiser le temps dans le jeu
        Time.timeScale = originalTimeScale;

        // Désactiver l'icône
        iconImage.enabled = false;
    }
}
