using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float multiplier = 1.4f;
    public float duration = 3f;
    public float respawnDelay = 5f;

    private Vector3 initialPosition;
    private bool isGrowEffect; // Variable pour déterminer si le power-up doit faire grandir ou rapetisser

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isGrowEffect = Random.value < 0.5f; // Génère une valeur aléatoire pour décider si c'est un effet de grandir ou de rapetisser
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        if (isGrowEffect)
        {
            player.transform.localScale *= multiplier; // Faire grandir le personnage
        }
        else
        {
            player.transform.localScale /= multiplier; // Faire rapetisser le personnage
        }

        yield return new WaitForSeconds(duration);

        if (isGrowEffect)
        {
            player.transform.localScale /= multiplier; // Rétablir la taille initiale du personnage
        }
        else
        {
            player.transform.localScale *= multiplier; // Rétablir la taille initiale du personnage
        }

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);

        transform.position = initialPosition;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}