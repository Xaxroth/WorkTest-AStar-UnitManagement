using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 worldPosition;

    public bool walkable;
    public bool occupied;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public int fCost => gCost + hCost;

    public Node parent;

    public GameObject occupant;

    private GameObject _gameObject;

    public void Awake()
    {
        if (GameManager.Instance != null) 
            GameManager.Instance.allTiles.Add(this);
    }

    // Returns a list of the node's neighbors
    public List<Node> GetNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        // Right
        AddNeighbor(gridX + 1, gridY, neighbors);
        // Left
        AddNeighbor(gridX - 1, gridY, neighbors);
        // Up
        AddNeighbor(gridX, gridY + 1, neighbors);
        // Down
        AddNeighbor(gridX, gridY - 1, neighbors);

        return neighbors;
    }

    // Once an offset is applied, this method will move from the node and add whatever node it finds at the specified offset (for example, gridX, gridY + 1) to add the node 1 space)
    private void AddNeighbor(int x, int y, List<Node> neighbors)
    {
        if (x >= 0 && x < Grid.Instance.gridSizeX && y >= 0 && y < Grid.Instance.gridSizeY)
        {
            Node neighbor = Grid.Instance.grid[x, y].GetComponent<Node>();
            if (neighbor.walkable)
            {
                neighbors.Add(neighbor);
            }
        }
    }

    public void SetAsOccupied(GameObject newOccupant)
    {
        occupant = newOccupant;

        walkable = false;
        occupied = true;

        GameManager.Instance.occupiedTiles.Add(this);

        if (GameManager.Instance.unoccupiedTiles.Contains(this))
        {
            GameManager.Instance.unoccupiedTiles.Remove(this);
        }
    }

    public void SetAsUnoccupied()
    {
        occupant = null;

        walkable = true;
        occupied = false;

        GameManager.Instance.unoccupiedTiles.Add(this);

        if (GameManager.Instance.occupiedTiles.Contains(this))
        {
            GameManager.Instance.occupiedTiles.Remove(this);
        }
    }

    public GameObject GetGameObject()
    {
        return _gameObject;
    }

    public void SetGameObject(GameObject obj)
    {
        _gameObject = obj;
    }
}
