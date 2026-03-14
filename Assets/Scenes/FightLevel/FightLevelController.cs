using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

public class FightLevelController : MonoBehaviour
{
    public static FightLevelController instance = null;

    public enum MapSlot { Free, BusyWithAlly, BusyWithEnemy, Busy};

    // --> 0 - free slot
    // --> 1 - slot is busy with "ally"
    // --> 2 - slot is busy with "enemy"
    // --> 3 - slot is busy with everything else
    private List<int> _mapSlots;
    private List<int> _mapSlotInstanceIDs;
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

    private List<int> _allyUnitsToDestroy;
    private List<int> _enemyUnitsToDestroy;

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
        _mapSlotInstanceIDs = new List<int>
        {
            Capacity = UnitsSlotsCount
        };

        _allyUnits = new List<GameObject>();
        _enemyUnits = new List<GameObject>();

        _allyUnitsToDestroy = new List<int>();
        _enemyUnitsToDestroy = new List<int>();

        _allyUnits.Capacity = (int)maxAllyUnits;
        _enemyUnits.Capacity = (int)maxEnemyUnits;

        while ( _mapSlots.Count < UnitsSlotsCount)
        {
            _mapSlots.Add(0);
            _mapSlotInstanceIDs.Add(-1);
        }
    }

    public void Update()
    {
        int maxUnits = Mathf.Max(_allyUnits.Count, _enemyUnits.Count);

        for(int i = 0; i < maxUnits; ++i)
        {
            if (i < _allyUnits.Count)
                UpdateUnit(_allyUnits[i], i);
            if (i < _enemyUnits.Count)
                UpdateUnit(_enemyUnits[i], i);
        }

        for(int i = 0; i < _allyUnitsToDestroy.Count; i++)
        {
            GameObject go = _allyUnits[i];
            _allyUnits.RemoveAt(i);
            Destroy(go);
        }

        for (int i = 0; i < _enemyUnitsToDestroy.Count; i++)
        {
            GameObject go = _enemyUnits[i];
            _enemyUnits.RemoveAt(i);
            Destroy(go);
        }

        _allyUnitsToDestroy.Clear();
        _enemyUnitsToDestroy.Clear();
    }

    public void UpdateUnit(GameObject unit, int indexInContainer)
    {
        NPC npcComponent = unit.GetComponent<NPC>();

        int characterIndex = GetMapSlotByPosition(unit.transform.position.x);

        if (npcComponent.IsDead())
        {
            List<int> deadList = unit.CompareTag("Ally") ? _allyUnitsToDestroy : _enemyUnitsToDestroy;

            deadList.Add(indexInContainer);
            _mapSlots[characterIndex] = 0;
            _mapSlotInstanceIDs[characterIndex] = -1;
            return;
        }

        npcComponent.UpdateCharacter();

        UpdateCharacterIndexInMap(npcComponent, characterIndex);

        if (!ReachedTheCellCenter(npcComponent, characterIndex))
            return;

        UpdateUnitState(npcComponent, characterIndex);
    }

    public void UpdateCharacterIndexInMap(NPC npc, int characterIndex)
    {
        int occupancyValue = npc.gameObject.CompareTag("Ally") ? 1 : 2;

        if(characterIndex != npc.myIndexInUnitsPositions)
        {
            _mapSlots[npc.myIndexInUnitsPositions] = 0;

            npc.myIndexInUnitsPositions = characterIndex;
            _mapSlots[characterIndex] = occupancyValue;
            _mapSlotInstanceIDs[characterIndex] = -1;
        }
    }

    public bool ReachedTheCellCenter(NPC npc, int characterIndex)
    {
        float cellSize = GetMapDistance / (float)_mapSlots.Count;

        if (npc.CompareTag("Ally"))
        {
            return !(npc.transform.position.x < (GetLeftCastleX + characterIndex * cellSize + 0.5f * cellSize));
        }
        else
        {
            return !(npc.transform.position.x > (GetLeftCastleX + characterIndex * cellSize + 0.5f * cellSize));
        }
    }

    public void UpdateUnitState(NPC npc, int characterIndex)
    {
        // TODO: One string comparison instead of three
        int occupancyValue      = npc.gameObject.CompareTag("Ally") ? (int)MapSlot.BusyWithAlly : (int)MapSlot.BusyWithEnemy;
        int enemyOccupancyValue = npc.gameObject.CompareTag("Ally") ? (int)MapSlot.BusyWithEnemy : (int)MapSlot.BusyWithAlly;
        int nextIndex           = npc.gameObject.CompareTag("Ally") ? characterIndex + 1 : characterIndex - 1;

        int objectID = npc.gameObject.GetInstanceID();

        if (nextIndex >= UnitsSlotsCount || nextIndex < 0)
        {
            npc.SetCharacterState(NPC.CharacterState.Idle);
            return;
        }

        if(_mapSlots[nextIndex] == enemyOccupancyValue)
        {
            npc.SetCharacterState(NPC.CharacterState.Fighting);
            return;
        }
        if (_mapSlots[nextIndex] == occupancyValue)
        {
            npc.SetCharacterState(NPC.CharacterState.Idle);
            return;
        }
        if (_mapSlots[nextIndex] == 0)
        {
            _mapSlots[nextIndex] = (int)MapSlot.Busy;
            _mapSlotInstanceIDs[nextIndex] = objectID;

            npc.SetCharacterState(NPC.CharacterState.Moving);
            return;
        }
        if (_mapSlots[nextIndex] == (int)MapSlot.Busy)
        {
            if (_mapSlotInstanceIDs[nextIndex] == objectID)
            {
                npc.SetCharacterState(NPC.CharacterState.Moving);
            }
            else
            {
                npc.SetCharacterState(NPC.CharacterState.Idle);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        if (_mapSlots == null)
        {
            return;
        }

        for(int i = 0; i < _mapSlots.Count; ++i)
        {
            float t0 = (float)(i) / (float)(_mapSlots.Count);
            float t1 = (float)(i + 1) / (float)_mapSlots.Count;

            float d0 = GetLeftCastleX + t0 * GetMapDistance;
            float d1 = GetLeftCastleX + t1 * GetMapDistance;

            float centerX = (d1 + d0) / 2.0f;

            if (_mapSlots[i] == 0)
            {
                Gizmos.color = Color.white;
            }
            if (_mapSlots[i] == 1)
            {
                Gizmos.color = Color.green;
            }
            if (_mapSlots[i] == 2)
            {
                Gizmos.color = Color.red;
            }
            if (_mapSlots[i] == 3)
            {
                Gizmos.color = Color.yellow;
            }

            Gizmos.DrawWireCube(new Vector3(centerX, -3.0f), new Vector3(d1 - d0, 2.0f));
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
            if (_mapSlots[0] != 0)
                return;

            Vector2 position = new(GetLeftCastleX, GetGroundLevel() + 0.1f * (12.88896f / 2.0f));
            _allyUnits.Add(SpawnAlly(position));
            _mapSlots[GetMapSlotByPosition(position.x)] = 1;
        }
    }

    public void SpawnEnemy()
    {
        if (_mapSlots[UnitsSlotsCount - 1] != 0)
            return;

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