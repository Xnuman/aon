using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{

    [SerializeField] private GameObject characterObject;
    [SerializeField] private string spawnGameObjectTag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCharacter()
    {
        GameObject go = Instantiate(characterObject);
        go.transform.position = gameObject.transform.position;
        go.tag = spawnGameObjectTag;
    }
}
