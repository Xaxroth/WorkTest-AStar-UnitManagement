using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitMovement : MonoBehaviour
{
    private Unit UnitData;
    void Start()
    {
        UnitData = GetComponent<Unit>();

        SetStartPosition();
    }

    void SetStartPosition()
    {
        Node tile = GameManager.Instance.unoccupiedTiles[Random.Range(0, GameManager.Instance.unoccupiedTiles.Count)];

        gameObject.transform.position = tile.transform.position;

        tile.SetAsOccupied(gameObject);
    }
}
