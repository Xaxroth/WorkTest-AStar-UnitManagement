using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Grid grid; 

    public List<Unit> allUnits = new List<Unit>();
    public List<Node> allTiles = new List<Node>();
    public List<Node> unoccupiedTiles = new List<Node>();
    public List<Node> occupiedTiles = new List<Node>();

    public List<Unit> playerTeam = new List<Unit>();
    public List<Unit> enemyTeam = new List<Unit>();

    public GameObject Tile;

    public List<Node> currentPath = new List<Node>();

    [Header("Class Data")]
    public UnitClassData archerData;
    public UnitClassData cavalierData;
    public UnitClassData dragonRiderData;
    public UnitClassData fighterData;
    public UnitClassData mageData;
    public UnitClassData priestData;
    public UnitClassData warlockData;

    [Header("Promoted Class Data")]
    public UnitClassData sniperData;
    public UnitClassData paladinData;
    public UnitClassData dragonKnightData;
    public UnitClassData heroData;
    public UnitClassData sageData;
    public UnitClassData bishopData;
    public UnitClassData demonData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            unoccupiedTiles = allTiles;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        for (int i = 0; i < currentPath.Count; i++)
        {
            currentPath[i].GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
    }
}
