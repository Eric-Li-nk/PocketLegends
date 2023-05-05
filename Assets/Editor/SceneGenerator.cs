using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class CreateMap : MonoBehaviour
{
    [MenuItem("Tools/Create Map")]
    static void CreateMapObject()
    {
        // Créer le cube et set ses propriétés
        GameObject mapObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mapObject.name = "Map";
        mapObject.transform.localScale = new Vector3(100.0f, 0.1f, 100.0f); // Changer la valeur pour la taille 
        mapObject.transform.position = Vector3.zero;

        // Ajouter un MeshCollider
        MeshCollider meshCollider = mapObject.AddComponent<MeshCollider>();
        meshCollider.convex = false;
        meshCollider.isTrigger = false;

        // Ajouter un Rigidbody
        Rigidbody rigidbody = mapObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;

        // Permettre Undo/Redo pour la map
        Undo.RegisterCreatedObjectUndo(mapObject, "Create Map Object");

        // Déclarer l'objet player
        GameObject playerObject = null;

        // Charger le prefab du player
        GameObject playerPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Characters/Player.prefab", typeof(GameObject));

        if (playerPrefab != null)
        {
            // Instancier le prefab du player
            playerObject = PrefabUtility.InstantiatePrefab(playerPrefab) as GameObject;
            playerObject.name = "Player";

            // Positionner le player au-dessus de la map
            playerObject.transform.position = mapObject.transform.position + Vector3.up * 0.5f;

            // Permettre Undo/Redo pour le player
            Undo.RegisterCreatedObjectUndo(playerObject, "Create Player Object");
        }
        else
        {
            Debug.LogError("Le prefab du player n'a pas été trouvé !");
        }

        // Etat dirty pour les deux objets
        EditorUtility.SetDirty(mapObject);
        if (playerObject != null)
        {
            EditorUtility.SetDirty(playerObject);
        }

        // Sauvegarde automatique de la scène
        //EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

        // Message de succès
        Debug.Log("Objets 'Map' et 'Player' bien créés !");
    }
}
