using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float multiplier = 1.4f;
    public float duration = 3f;
    public float respawnDelay = 5f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        player.transform.localScale *= multiplier;

        yield return new WaitForSeconds(duration);

        player.transform.localScale /= multiplier;

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