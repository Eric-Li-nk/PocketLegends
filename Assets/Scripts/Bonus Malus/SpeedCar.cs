using UnityEngine;

public class SpeedCar : MonoBehaviour
{
    public float speedBoostDuration = 5f; // Durée du bonus de vitesse en secondes
    public float deactivateTime = 3f; // Temps en secondes avant de désactiver la sphère bonus

    private bool isActive = true; // Indique si la boîte bonus est active ou non
    private float boostStartTime; // Heure de début du bonus
    private GameObject player; // Référence au joueur

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isActive)
        {
            ApplySpeedBoost(other.gameObject);
            isActive = false; // désactiver la boîte bonus une fois qu'elle a été collectée

            // Désactiver l'objet BonusBox et jouer une animation de particules
            
            /*
            gameObject.SetActive(false);
            GetComponent<ParticleSystem>().Play();
            */
            
            // Réactiver l'objet BonusBox après un certain temps
            Invoke("Reactivate", deactivateTime);
        }
    }

    private void Reactivate()
    {
        isActive = true; // réactiver la boîte bonus
        gameObject.SetActive(true); // réactiver l'objet BonusBox
    }

    private void ApplySpeedBoost(GameObject player)
    {
        this.player = player; // enregistrer la référence au joueur
        float originalSpeed = player.GetComponentInParent<VehicleMovement>().motorTorque;
        player.GetComponentInParent<VehicleMovement>().motorTorque *= 2f; // multiplier la vitesse du joueur par 2

        boostStartTime = Time.time; // enregistrer l'heure de début du bonus
    }

    private void Update()
    {
        if (!isActive && Time.time >= boostStartTime + speedBoostDuration)
        {
            ResetSpeed();
        }
    }

    private void ResetSpeed()
    {
        float originalSpeed = player.GetComponentInParent<VehicleMovement>().motorTorque;
        player.GetComponentInParent<VehicleMovement>().motorTorque = originalSpeed;
    }
}
