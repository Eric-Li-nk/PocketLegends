using System.Collections;
using UnityEngine;

public class CubePlacementAbility : MonoBehaviour
{
    public GameObject cubePrefab; // Préfabriqué du cube à déposer
    public KeyCode placementKey = KeyCode.C; // Touche pour déposer le cube
    public float cooldownTime = 5f; // Temps d'attente en secondes
    public float cubeLifetime = 3f; // Durée de vie du cube

    private bool canPlaceCube = true; // Variable pour vérifier si le cube peut être placé

    private void Update()
    {
        if (Input.GetKeyDown(placementKey) && canPlaceCube)
        {
            PlaceCube();
            StartCoroutine(StartCooldown());
        }
    }

    private void PlaceCube()
    {
        GameObject cube = Instantiate(cubePrefab, transform.position - transform.forward, Quaternion.identity);
        StartCoroutine(DestroyCube(cube));
    }

    private IEnumerator StartCooldown()
    {
        canPlaceCube = false; // Désactiver la possibilité de placer le cube

        yield return new WaitForSeconds(cooldownTime);

        canPlaceCube = true; // Réactiver la possibilité de placer le cube
    }

    private IEnumerator DestroyCube(GameObject cube)
    {
        yield return new WaitForSeconds(cubeLifetime);

        Destroy(cube);
    }
}