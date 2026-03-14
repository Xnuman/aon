using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject characterObject;
    [SerializeField] private string spawnGameObjectTag;
    public GameObject SpawnCharacter(Vector2 position)
    {
        GameObject newCharacter = Instantiate(characterObject, new Vector3(position.x, position.y, 0), new Quaternion());
        newCharacter.tag = spawnGameObjectTag;
        return newCharacter;
    }
    public bool isAlly(string tag)
    {
        return (string.Compare(tag, "Ally") == 0);
    }
}