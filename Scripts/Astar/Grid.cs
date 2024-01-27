using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance { get; private set; }
    public GameObject[,] grid;
    public GameObject selectedNode;

    public Vector2 gridSize;
    public float nodeDiameter;
    public float nodeSize;
    public int gridSizeX, gridSizeY;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        nodeDiameter = nodeSize * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid();

        int centerX = gridSizeX / 2;
        int centerY = gridSizeY / 2;

        if (centerX >= 0 && centerX < gridSizeX && centerY >= 0 && centerY < gridSizeY)
        {
            selectedNode = grid[centerX, centerY];
            SelectNodeGameObject(selectedNode);
        }
    }

    void Update()
    {
        if (grid == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedNode = hit.collider.gameObject;

                if (clickedNode.CompareTag("Node"))
                {
                    if (selectedNode != null)
                    {
                        List<Node> path = AStarPathfinding.FindPath(selectedNode, clickedNode, this);

                        DeselectNodeGameObject(selectedNode);

                        Vector3 mousePosition = Input.mousePosition;
                        mousePosition.z = Camera.main.transform.position.y;
                        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);


                        foreach (Node node in path)
                        {
                           
                        }
                    }

                    selectedNode = clickedNode;
                    SelectNodeGameObject(selectedNode);
                }
            }
        }
    }

    void OnMouseEnter()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hoveredNode = hit.collider.gameObject;

            if (hoveredNode.CompareTag("Node"))
            {
                hoveredNode.GetComponentInChildren<Renderer>().material.color = Color.yellow;
            }
        }
    }

    void OnMouseExit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject exitedNode = hit.collider.gameObject;

            if (exitedNode.CompareTag("Node"))
            {
                exitedNode.GetComponentInChildren<Renderer>().material.color = Color.white;
            }
        }
    }

    void CreateGrid()
    {
        grid = new GameObject[gridSizeX, gridSizeY];
        Vector3 gridStartPosition = transform.position - Vector3.right * gridSizeX / 2 - Vector3.forward * gridSizeY / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = gridStartPosition + Vector3.right * (x * nodeDiameter + nodeSize) + Vector3.forward * (y * nodeDiameter + nodeSize);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeSize));

                GameObject tile = Instantiate(GameManager.Instance.Tile, worldPoint, Quaternion.identity);
                Node node = tile.GetComponent<Node>();
                node.gridX = x;
                node.gridY = y;
                node.worldPosition = worldPoint;
                node.SetGrid(this);
                grid[x, y] = tile;
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = Mathf.Clamp01((worldPosition.x + gridSize.x / 2) / gridSize.x);
        float percentY = Mathf.Clamp01((worldPosition.z + gridSize.y / 2) / gridSize.y);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        x = Mathf.Clamp(x, 0, gridSizeX - 1);
        y = Mathf.Clamp(y, 0, gridSizeY - 1);

        if (x >= 0 && x < gridSizeX && y >= 0 && y < gridSizeY)
        {
            return grid[x, y].GetComponent<Node>();
        }
        else
        {
            return null;
        }
    }

    void SelectNodeGameObject(GameObject node)
    {
        node.GetComponentInChildren<Renderer>().material.color = Color.green;
    }

    void DeselectNodeGameObject(GameObject node)
    {
        node.GetComponentInChildren<Renderer>().material.color = Color.yellow;
    }
}
