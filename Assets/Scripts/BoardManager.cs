using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager: MonoBehaviour
{
    private static BoardManager boardInstance;
    public static BoardManager BoardInstance { get { return boardInstance; } }

    public OverlayTile gameSpaces;
    public int rows;
    public int columns;

    public Dictionary<Vector2Int, OverlayTile> theMap;

    private List<OverlayTile> AllTiles = new();
    // Start is called before the first frame update

    private void Awake()
    {
        if(boardInstance != null && boardInstance != this)
        {
            Destroy(this.gameObject);

        }
        else 
        {
            boardInstance = this;
        }
    }
    void Start()
    {
        theMap = new Dictionary<Vector2Int, OverlayTile>();
        GenerateBoardSpaces();
    }

    // Update is called once per frame
    public void GenerateBoardSpaces()
    { 
        for(int y = 0; y< rows; y++)
        {
            for(int x = 0; x<columns;x++)
            {
                var tileKey = new Vector2Int(x, y);
                var overlayTile = Instantiate(gameSpaces, new Vector3(transform.position.x+x,transform.position.y+y,1), Quaternion.identity);
                theMap.Add(tileKey, overlayTile);
                overlayTile.gridLocation = tileKey;
                overlayTile.transform.SetParent(this.transform);
                AllTiles.Add(overlayTile);
            } 
        }
    }

    public void UpdateBoardCollsions()
    {
        foreach(var tile in AllTiles)
        {
            tile.CheckOccupied();
        }
    }
    public List<OverlayTile> GetNeighborTiles(OverlayTile curremtOverLayTile)
    {
        
        List<OverlayTile> neighbors = new List<OverlayTile>();

        //top
        Vector2Int locationToCheck = new Vector2Int(
            curremtOverLayTile.gridLocation.x,
            curremtOverLayTile.gridLocation.y + 1
            );
        if (theMap.ContainsKey(locationToCheck))
        {
            neighbors.Add(theMap[locationToCheck]);
        }
        //bottom
        locationToCheck = new Vector2Int(
            curremtOverLayTile.gridLocation.x,
            curremtOverLayTile.gridLocation.y - 1
            );
        if (theMap.ContainsKey(locationToCheck))
        {
            neighbors.Add(theMap[locationToCheck]);
        }
        //right
        locationToCheck = new Vector2Int(
            curremtOverLayTile.gridLocation.x + 1,
            curremtOverLayTile.gridLocation.y
            );
        if (theMap.ContainsKey(locationToCheck))
        {
            neighbors.Add(theMap[locationToCheck]);
        }
        //left
        locationToCheck = new Vector2Int(
            curremtOverLayTile.gridLocation.x - 1,
            curremtOverLayTile.gridLocation.y
            );
        if (theMap.ContainsKey(locationToCheck))
        {
            neighbors.Add(theMap[locationToCheck]);
        }
        return neighbors;
    }
}
