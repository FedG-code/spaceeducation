using System;
using System.Linq;
// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class HexMap : MonoBehaviour
{

    // Samuel: Current player / unit. This has been manually sected, by dragging
    // the unit gameobject into this field
    public GameObject selectedUnit;

    public GameObject HexPrefab;


    // Start is called before the first frame update
    void Start()
    {

        GenerateMap();

        SpawnUnitAt(this.selectedUnit, 9,26);

        GenerateNodes();

    }

    /*
     * Fed: we don't need to concern ourselves with meshs as we don't have water
     * stuff, might be useful later on leaving it here commented
     * public Mesh MeshWater;
     * public Mesh MesFlat;
     * public Mesh MeshHill;
     * public Mesh MeshMountain;
    */
    //actually we might need 1 mesh
    public Mesh TileMesh;

    public Material MatSpace;
    public Material MatBlackHole;
    public Material MatStar;
    public Material MatAsteroid;
    public Material MatPlanet;

    public Material MatEvent;

    public Material MatBadPlanet;

    // public GameObject UnitResourceShipPrefab; // Changed to selectedUnit

    public int numRows = 40;
    public int numColumns = 40;

    public bool allowWrapEastWest = true;
    public bool allowWrapNorthSouth = false;

    public HexTileBase[,] hexes {get; set;}
    // Samuel: graph of hex tiles, representing each tile connections
    public Node[,] graph;
    // List<Node> currentPath = null;

    private Dictionary<HexTileBase, GameObject> hexToGameObjectMap;

    public void GenerateNodes() {
        // Samuel: populate empy array of nodes the size of our map
        graph = new Node[numColumns, numRows];
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                // Node n = new Node(column,row);
                graph[column, row] = new Node(column, row);
                // graph[column, row].Q = column;
                // graph[column, row].R = row;
                graph[column, row].map = this;
            }
        }

        //Generate tile objects for map
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                // Samuel: inplace editing of node in graph (ref is two way,
                // meaning  that it will not remove pre-existing node edges)
                // graph[column, row]
                AddNodeEdges(column, row);
            }
        }

    }
    public Node GetNodeAt(int q, int r)
    {
        if (graph == null)
        {
            Debug.LogError("Graph array not yet instantied.");
            return null;
        }

        if (allowWrapEastWest)
            q = q % numRows;
        if (allowWrapNorthSouth)
            r = r % numColumns;

        Node n = graph[q, r];
        return n;
    }
    public HexTileBase GetHexAt(int q, int r)
    {
        if (hexes == null)
        {
            Debug.LogError("Hexes array not yet instantied.");
            return null;
        }

        if (allowWrapEastWest)
            q = q % numRows;
        if (allowWrapNorthSouth)
            r = r % numColumns;

        return hexes[q, r];
    }
    //

    public void AddNodeEdges(int q, int r) {
        // Samuel: Generate Pathfinding Graph
        // nodes added clockwise from top left edge of hex tile
        // node = graph[column, row]
        // Using GetHexAt,
        // Node[,] graph



        if (allowWrapEastWest)
            q = q % numRows;
        if (allowWrapNorthSouth)
            r = r % numColumns;

        if (r < numRows-1) // Cant do the last tile at the end of row
            graph[q, r].edges.Add(GetNodeAt(q+0,r+1)); // NorthEast
        if (q < numColumns-1){
            graph[q, r].edges.Add(GetNodeAt(q+1,r+0)); // East
            if (r > 0) {
                graph[q, r].edges.Add(GetNodeAt(q+1,r-1)); // SE
            }
        }

        if (r > 0)
            graph[q, r].edges.Add(GetNodeAt(q+0,r-1)); // SW

        if (q > 0){
            graph[q, r].edges.Add(GetNodeAt(q-1,r+0)); // W
            if (r < numRows-1)
                graph[q, r].edges.Add(GetNodeAt(q-1,r+1)); // NW
        }
    }

    virtual public void GenerateMap()
    {
        hexes = new HexTileBase[numColumns, numRows];
        hexToGameObjectMap = new Dictionary<HexTileBase, GameObject>();

        //Generate tile objects for map
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                // Fed: omitted from tutorial 2 video, comments pointed me to
                // this. This definiton of pos is what makes the square map
                HexTileSpace h = new HexTileSpace(this, column, row);
                hexes[column, row] = h;


                Vector3 pos = h.PositionFromCamera(
                    Camera.main.transform.position,
                    numRows,
                    numColumns
                );

                GameObject hexGO = (GameObject)Instantiate(
                    HexPrefab,
                    //Fed: same omission from tutorial
                    pos,
                    Quaternion.identity,
                    this.transform
                );

                // Samuel: Get clickable tile component from hex prefab
                ClickableTile ct = hexGO.GetComponent<ClickableTile>();
                ct.Q = column;
                ct.R = row;
                ct.map = this;



                h.GameObject = hexGO;
                MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer>();
                mr.material = MatSpace;
                hexToGameObjectMap[h] = hexGO;
                hexGO.GetComponent<HexComponent>().Hex = h;
                hexGO.GetComponent<HexComponent>().HexMap = this;
                //hexGO.isStatic = true;
                hexGO.GetComponentInChildren<TextMesh>().text = string.Format("{0},{1}", column, row);

            }
        }

       // UpdateHexVisuals();

        //Fed: not useable for our purpose but we'll need something like this for our batching
        StaticBatchingUtility.Combine(this.gameObject);
    }

    public List<HexTileBase> GetHexesWithinRadiusOf(HexTileBase centerHex, int range)
    {
        List<HexTileBase> results = new List<HexTileBase>();
        for (int dx = -range; dx < range - 1; dx++)
        {
            for (int dy = Mathf.Max(-range + 1, -dx - range); dy < Mathf.Min(range, -dx + range - 1); dy++)
            {
                results.Add(hexes[centerHex.Q + dx, centerHex.R + dy]);
            }

        }
        return results;
    }


    public Vector3 HexCoordToWorldCoord(int Q, int R){

        HexTileBase h = GetHexAt(Q,R);
        return h.Position();
    }
    public void SpawnUnitAt(GameObject unitPrefab, int q=0, int r=0)
    {

        // hexToGameObjectMap[GetHexAt(x, y)] = ;
        HexTileBase h = GetHexAt(q, r);

        selectedUnit.GetComponent<Unit>().map = this;
        selectedUnit = Instantiate(unitPrefab, h.Position(), Quaternion.identity);
        selectedUnit.GetComponent<Unit>().Q  = q;
        selectedUnit.GetComponent<Unit>().R  = r;
        // selectedUnit.Q = q;
        // selectedUnit.R = r;
    }
    public float CostToEnterHex(int q, int r) {
        HexTileBase h = GetHexAt(q,r);
        return h.movementCost;

        }
    public void GeneratePathTo(int q, int r) {
        /**
        * Author: Samuel Overington
        * Function to connect to component ClickableTile.cs
        * NB Currently unit (player), is permanently selected
        * (see GameObject selectedUnit in the head of this file)
        **/



        // movement here
		// selectedUnit.GetComponent<Unit>().destination = h.Position();

        // Initialise current path to be empty at the beginning.
        selectedUnit.GetComponent<Unit>().currentPath = null;
        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        // List of nodes that we haven't checked out yet
        List<Node> unvisited = new List<Node>();



        Node source = graph[
    		selectedUnit.GetComponent<Unit>().Q,
    		selectedUnit.GetComponent<Unit>().R
        ];

        Node target = graph[
            q,
            r
        ];

        // set initial values for pathfinding
        dist[source] = 0;
        prev[source] = null;

        foreach (Node n in graph){
            if (n != source){
                /**
                initialise nodes to have infinity distance, since we don't
                know any better at this point. Also its possible that some
                nodes can't be reached, which would make infinity a
                reasonable value
                **/
                dist[n] = Mathf.Infinity;
                prev[n] = null; // We don't know the previous here
            }
            unvisited.Add(n);
        }
        while (unvisited.Count > 0) {
            // All of the nodes inside of unvisited, we are going to order based on distance. This is a short fast method, and could be optimised at a later date. A possible solution: consider having a prioraty queue or some other self sorting, optimized data structure.
            // Node u = unvisited.OrderBy(n => dist[n]).First();
            Node u = null;
            foreach (Node possibleU in unvisited) {
                if (u == null || dist[possibleU] < dist[u])
                    u = possibleU;

            }


            // Break out of while loop, as we have found our shortest distance to target.
            if (u == target) {
                break;
            }

            unvisited.Remove(u);

            foreach (Node n in u.edges) {
                float alt = dist[u] + CostToEnterHex(n.Q, n.R);
                if (alt < dist[n]) {
                    dist[n] = alt;
                    prev[n] = u;
                }
            }
        }
        // Options: we have found shortest rout, or there is no route to target

        // Check if there is no route
        if (prev[target] == null) {
            // no possible route from target to source
            return;
        }
        List<Node> currentPath = new List<Node>();
        Node curr = target;

        // Stepping through the "prev" chain, adding each node to our pathfinding
        while (curr != null) {
            // while there is still a previous Node to step backwards through
            currentPath.Add(curr);
            curr = prev[curr];

        }
        // right now, currentPath describes a step by step
        // node path from our target to our source, so we
        // need to invert it.
        currentPath.Reverse();

        selectedUnit.GetComponent<Unit>().currentPath = currentPath;

    }

}
