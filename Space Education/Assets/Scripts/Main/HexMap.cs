using System;
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
        GenerateNodes();
        GenerateMap();
        SpawnUnitAt(this.selectedUnit, 9,26);
        // MoveSelectedUnitTo(9,26);
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

    private Dictionary<HexTileBase, GameObject> hexToGameObjectMap;

    public void GenerateNodes() {
        // Samuel: populate empy array of nodes the size of our map
        graph = new Node[numColumns, numRows];
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                // Node n = new Node(column,row);
                // Debug.Log(string.Format("adding node at: {0},{1} ",n.Q, n.R));
                graph[column, row] = new Node(column, row);
                // graph[column, row].Q = column;
                // graph[column, row].R = row;
                // graph[column, row] = n;
            }
        }
        // Debug.Log(string.Format("The Rank of the graph: {0}", graph.Rank));
        // Debug.Log(string.Format("The length of the graph: {0}", graph.GetLength(0)));
        // Node n = graph[4,24];
        // Debug.Log(string.Format("a random node: {0},{1}", n.Q, n.R));

    }
    public Node GetNodeAt(int q, int r)
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
        // Debug.Log(string.Format("getting node from {0}, {1}",q,r));

        Node n = graph[q, r];
        // Debug.Log(string.Format("node at {0}, {1}",n.Q,n.R));
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
        // Using GetHexAt, and passing graph- so that we can be consistant
        // with wrapping
        // Node[,] graph


        // GetNodeAt(q+0,r+1)

        // Node n = GetNodeAt(q+0,r+1);
        // Debug.Log(string.Format("getting node at {0}, {1}",n.Q,n.R));
        // Debug.Log({n.Q, n.R});

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
                SpaceTile h = new SpaceTile(this, column, row);
                // Debug.Log(string.Format("Creating Hex Tile at {0},{1}",h.Q, h.R));
                //h.tiletype = -1;
                hexes[column, row] = h;

                // Samuel: inplace editing of node in graph (ref is two way,
                // meaning  that it will not remove pre-existing node edges)
                // graph[column, row]
                AddNodeEdges(column, row);

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

    public class Node
    {
        /**
        * Author: Samuel Overington
        * Class for storing graph data (nodes and edges), and all the different
        * connections between each neighbouring tile.
        *
        * Each tile: a node
        * Each neighbouring (connected) tile: an edge.
        **/

        public List<Node> edges;
        public int Q;
        public int R;

        public Node(int q,int r) {
            // Default Constructor
            edges = new List<Node>();
            Q = q;
            R = r;
        }
    }

    public void SpawnUnitAt(GameObject unitPrefab, int q=0, int r=0)
    {

        // hexToGameObjectMap[GetHexAt(x, y)] = ;
        HexTileBase h = GetHexAt(q, r);

        selectedUnit = Instantiate(unitPrefab, h.Position(), Quaternion.identity);
    }
    public void MoveSelectedUnitTo(int q, int r) {
        /**
        * Author: Samuel Overington
        * Function to connect to component ClickableTile.cs
        * NB Currently unit (player), is permanently selected
        * (see GameObject selectedUnit in the head of this file)
        **/

		// selectedUnit.GetComponent<Unit>().tileQ = x;
		// selectedUnit.GetComponent<Unit>().tileR = y;

        HexTileBase h = GetHexAt(q,r);

        // movement here
		// selectedUnit.GetComponent<Unit>().destination = h.Position();

        // Debug.Log(string.Format("Moving selectedUnit from {0} {1} to {2}, {1}", h.Q, h.R, q, r));
        // Debug.Log(selectedUnit);

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();
        List<Node> unvisited = new List<Node>();

        Node source = graph[h.Q, h.R];

    }

}
