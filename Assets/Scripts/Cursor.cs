using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cursor : MonoBehaviour
{
    public float propSpeed = 10;
    public BasicCharacter currentChar;
    private PathFinder pathFinder;
    private RangeFinder rangeFinder;
    private List<OverlayTile> path = new();
    private List<OverlayTile> inRangeTiles = new();

    // Start is called before the first frame update
    void Start()
    {
        pathFinder = gameObject.AddComponent<PathFinder>();
        rangeFinder = gameObject.AddComponent<RangeFinder>();
    }
    

    // Update is called once per frame
    void LateUpdate()
    {
        
        var focusedTileHit = GetFocusedOnTile(); 
        if(focusedTileHit.HasValue)
        {
            OverlayTile overlayTile = focusedTileHit.Value.collider.gameObject.GetComponent<OverlayTile>();
            transform.position = overlayTile.transform.position;
            if(Input.GetMouseButtonDown(0))
            {
                BoardManager.BoardInstance.UpdateBoardCollsions();
                path = pathFinder.FindPath(currentChar.activeTile, overlayTile);
            }
            GetInRangeTiles();
        }
        if(path.Count>0)
        {
            //MoveAlongPath();
            currentChar.GetComponent<BasicCharacter>().MoveAlongPath(path);
        }
    }

    public void GetInRangeTiles()
    {
        foreach (var item in inRangeTiles)
        {
            item.HideTile();
        }
        
        inRangeTiles = rangeFinder.GetTilesinRange(currentChar.activeTile, currentChar.movementDistance);
        Debug.Log(inRangeTiles);
        foreach (var item in inRangeTiles)
        {
            if(!item.isOccupied)
            {
                item.ShowTile();
            }
            
        }
 
    }

    private void MoveAlongPath()
    {
        Debug.Log("I should be firing");
        var step = propSpeed * Time.deltaTime;

        currentChar.transform.position = Vector2.MoveTowards(currentChar.transform.position, path[0].transform.position, step);
        Debug.Log(Vector2.MoveTowards(currentChar.transform.position, path[0].transform.position, step));
        if (Vector2.Distance(currentChar.transform.position, path[0].transform.position) < 0.05f)
        {
            PositionCharacterOnTile(path[0]);
            path.RemoveAt(0);
            
        }
    }

    public RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);
        
        if(hits.Length> 0)
        {
            return hits.OrderByDescending(i=>i.collider.transform.position.z).First(); 
        }
        return null;
    }

    private void PositionCharacterOnTile(OverlayTile tile)
    {
        currentChar.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, 0f);
        currentChar.activeTile = tile;
    }
    
    public void AssignCurrentCharacter(BasicCharacter newCharacter)
    {
        currentChar = newCharacter;
    }
}
