using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;
using UnityEditor.ShaderGraph.Internal;

public class FightLevelController : MonoBehaviour
{
    public static FightLevelController instance = null;

    // --> 0 - free slot
    // --> 1 - slot is busy with "ally"
    // --> 2 - slot is busy with "enemy"
    // --> 3 - slot is busy with everything else
    private List<int> _mapSlots;
    public List<int> GetMap => _mapSlots;

    private List<GameObject> _allyUnits;
    private List<GameObject> _enemyUnits;

    [SerializeField] private uint maxAllyUnits;
    [SerializeField] private uint maxEnemyUnits;

    [SerializeField] private GameObject leftCastle;
    [SerializeField] private GameObject rightCastle;
    [SerializeField] private GameObject ground;

    [SerializeField] private CharacterSpawner _allySpawner;
    [SerializeField] private CharacterSpawner _enemySpawner;

    public float GetLeftCastleX => leftCastle.transform.position.x;
    public float GetRightCastleX => rightCastle.transform.position.x;
    public float GetMapDistance => GetRightCastleX - GetLeftCastleX;

    [SerializeField] private int UnitsSlotsCount;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        Assert.IsTrue(UnitsSlotsCount > 0);

        _mapSlots = new List<int>
        {
            Capacity = UnitsSlotsCount
        };

        _allyUnits = new List<GameObject>();
        _enemyUnits = new List<GameObject>();

        _allyUnits.Capacity = (int)maxAllyUnits;
        _enemyUnits.Capacity = (int)maxEnemyUnits;

        while ( _mapSlots.Count < UnitsSlotsCount)
        {
            _mapSlots.Add(0);
        }
    }

    public void Update()
    {
        foreach(GameObject unit in _allyUnits)
        {
            NPC npcComponent = unit.GetComponent<NPC>();
            npcComponent.UpdateCharacter();

            int characterIndex = GetMapSlotByPosition(unit.transform.position.x);

            if(characterIndex != npcComponent.myIndexInUnitsPositions)
            {
                _mapSlots[npcComponent.myIndexInUnitsPositions] = 0;
                npcComponent.myIndexInUnitsPositions = characterIndex;
                _mapSlots[characterIndex] = 1;
            }

            int nextIndex = characterIndex + 1;

            float cellSize = GetMapDistance / (float)_mapSlots.Count;

            if (npcComponent.transform.position.x < (GetLeftCastleX + characterIndex * cellSize + 0.5f * cellSize))
            {
                continue;
            }

            if(nextIndex >= UnitsSlotsCount || nextIndex < 0)
            {
                npcComponent.SetCharacterState(NPC.CharacterState.Idle);
            }
            if(_mapSlots[nextIndex] == 2)
            {
                npcComponent.SetCharacterState(NPC.CharacterState.Fighting);
            }
            if (_mapSlots[nextIndex] == 1)
            {
                npcComponent.SetCharacterState(NPC.CharacterState.Idle);
            }
            if (_mapSlots[nextIndex] == 0)
            {
                npcComponent.SetCharacterState(NPC.CharacterState.Moving);
            }
        }

        foreach(GameObject unit in _enemyUnits)
        {
            NPC npcComponent = unit.GetComponent<NPC>();
            npcComponent.UpdateCharacter();

            int characterIndex = GetMapSlotByPosition(unit.transform.position.x);
            if (characterIndex != npcComponent.myIndexInUnitsPositions)
            {
                _mapSlots[npcComponent.myIndexInUnitsPositions] = 0;
                npcComponent.myIndexInUnitsPositions = characterIndex;
                _mapSlots[characterIndex] = 2;
            }

            int nextIndex = characterIndex - 1;

            float cellSize = GetMapDistance / (float)_mapSlots.Count;

            if (npcComponent.transform.position.x > (GetLeftCastleX + characterIndex * cellSize + 0.5f * cellSize))
            {
                continue;
            }

            if (nextIndex >= UnitsSlotsCount || nextIndex < 0)
            {
                npcComponent.SetCharacterState(NPC.CharacterState.Idle);
                continue;
            }
            if (_mapSlots[nextIndex] == 1)
            {
                npcComponent.SetCharacterState(NPC.CharacterState.Fighting);
                continue;
            }
            if (_mapSlots[nextIndex] == 2)
            {
                npcComponent.SetCharacterState(NPC.CharacterState.Idle);
                continue;
            }
            if (_mapSlots[nextIndex] == 0)
            {
                npcComponent.SetCharacterState(NPC.CharacterState.Moving);
                continue;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        if (_mapSlots == null)
        {
            //Debug.Log("Map slots is bull");
            return;
        }

        for(int i = 0; i <= _mapSlots.Count; i++)
        {
            float t = (float)i / (float)_mapSlots.Count;

            Vector3 partBorderPoint = Vector3.Lerp(leftCastle.transform.position, rightCastle.transform.position, t);
            Vector3 bottomPoint = partBorderPoint;
            bottomPoint.y = GetGroundLevel();
            Vector3 topPoint = partBorderPoint + Vector3.up * 5.0f;

            Gizmos.DrawLine(bottomPoint, topPoint);
        }
    }

    public bool UnitPositionIndexValid(int index)
    {
        return (index >=0 && index < _mapSlots.Count);
    }

    public int GetMapSlotByPosition(float position)
    {
        float relativePosition = position - GetLeftCastleX;
        float cellSize = GetMapDistance / (float)_mapSlots.Count;

        float indexF = relativePosition / cellSize;

        return Mathf.FloorToInt(Mathf.Clamp(indexF, 0.0f, _mapSlots.Count - 1));

        //return Mathf.FloorToInt((position - GetLeftCastleX) / (GetMapDistance / _mapSlots.Count));
    }

    public void SpawnAlly()
    {
        if(_allyUnits.Count < maxAllyUnits)
        {
            Vector2 position = new(GetLeftCastleX, GetGroundLevel() + 0.1f * (12.88896f / 2.0f));
            _allyUnits.Add(SpawnAlly(position));
            _mapSlots[GetMapSlotByPosition(position.x)] = 1;
        }
    }

    public void SpawnEnemy()
    {
        if (_enemyUnits.Count < maxEnemyUnits)
        {
            Vector2 position = new(GetRightCastleX, GetGroundLevel() + 0.1f * (12.88896f / 2.0f));
            _enemyUnits.Add(SpawnEnemy(position));
            _mapSlots[GetMapSlotByPosition(position.x)] = 2;
        }
    }
    private GameObject SpawnAlly(Vector2 transform)
    {
        return SpawnCharacter(_allySpawner, transform);
    }

    private GameObject SpawnEnemy(Vector2 transform)
    {
        return SpawnCharacter(_enemySpawner, transform);
    }

    private GameObject SpawnCharacter(CharacterSpawner spawner, Vector2 position)
    {
        if (!spawner)
            return null;

        GameObject character = spawner.SpawnCharacter(position);
        NPC npcComponent = character.GetComponent<NPC>();

        npcComponent.InitCharacter();

        return character;
    }

    public float GetGroundLevel()
    {
        BoxCollider2D groundCollider = ground.GetComponent<BoxCollider2D>();
        Bounds bounds = groundCollider.bounds;
        return bounds.center.y + bounds.extents.y;
    }
}