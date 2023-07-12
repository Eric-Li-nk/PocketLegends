using UnityEngine;

public class BotMovement : MonoBehaviour
{
    private Character character;
    private BotNavMesh botNavMesh;
    private Rigidbody rigidbody;

    private void Awake()
    {
        character = GetComponent<Character>();
        botNavMesh = GetComponent<BotNavMesh>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (character.isPlayer)
        {
            // Ne pas exécuter ce script pour le joueur
            enabled = false;
        }
    }

    private void Update()
    {
        // Logique de mouvement pour les bots ici
        // Utilisez les variables de "character" pour définir le comportement de mouvement des bots
        // Par exemple, vous pouvez utiliser character.maxSpeed pour définir la vitesse maximale du bot
        
        // Exemple de code :

        // Choisir la vitesse en fonction de l'environnement (sol ou air)
        float speed = character.isGrounded ? character.maxSpeed : character.maxFlySpeed;
        
        // Définir la vitesse du bot dans le script BotNavMesh
        //botNavMesh.SetSpeed(speed);
        
        // Détection du saut
        if (character.isGrounded && character.jumpCooldown <= 0)
        {
            // Appliquer une force de saut au personnage
            rigidbody.AddForce(Vector3.up * character.jumpForce, ForceMode.Impulse);
            character.jumpCooldown = 1f;
        }
        
        // Gestion des cooldowns
        if (character.jumpCooldown > 0)
        {
            character.jumpCooldown -= Time.deltaTime;
        }
    }
}
