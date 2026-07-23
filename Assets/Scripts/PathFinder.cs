using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
   public List<OverlayTile> FindPath(OverlayTile start, OverlayTile end)
   {
        List<OverlayTile> openList = new();
        List<OverlayTile> closedList = new();

        openList.Add(start);
        //Debug.Log(openList);

        while(openList.Count> 0)
        {
            
            openList.OrderBy(x => x.F).First();
            OverlayTile currentOverlayTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentOverlayTile);
            closedList.Add(currentOverlayTile);

            if(currentOverlayTile == end)
            {
                return GethFinishedList(start, end);
                
            }
            var neighborTiles = BoardManager.BoardInstance.GetNeighborTiles(currentOverlayTile);

            foreach (var neighbor in neighborTiles)
            {
                if (neighbor.isOccupied || closedList.Contains(neighbor))
                {
                    continue;
                }
                neighbor.G = GetManhattenDistance(start, neighbor);
                neighbor.H = GetManhattenDistance(end, neighbor);

                neighbor.previous = currentOverlayTile;
                if (!openList.Contains(neighbor))
                {
                    openList.Add(neighbor);
                }
            }
        }
        return new List<OverlayTile>();
    }

    private List<OverlayTile> GethFinishedList(OverlayTile start, OverlayTile end)
    {
        List<OverlayTile> finishedList = new List<OverlayTile>();

        OverlayTile currentTile = end;

        while(currentTile != start)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.previous;
        }
        finishedList.Reverse();
        return finishedList;
    }

    private int GetManhattenDistance(OverlayTile start, OverlayTile neighbor)
    {
        return Mathf.Abs(start.gridLocation.x - neighbor.gridLocation.x) + Mathf.Abs(start.gridLocation.x - neighbor.gridLocation.x);
    }

    private List<OverlayTile> GetNeighborTiles(OverlayTile curremtOverLayTile)
    {
        var map = BoardManager.BoardInstance.theMap;
        List<OverlayTile> neighbors = new List<OverlayTile>();

        //top
        Vector2Int locationToCheck = new Vector2Int(
            curremtOverLayTile.gridLocation.x, 
            curremtOverLayTile.gridLocation.y +1
            );
        if (map.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck]);
        }
        //bottom
        locationToCheck = new Vector2Int(
            curremtOverLayTile.gridLocation.x,
            curremtOverLayTile.gridLocation.y - 1
            );
        if (map.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck]);
        }
        //right
        locationToCheck = new Vector2Int(
            curremtOverLayTile.gridLocation.x + 1,
            curremtOverLayTile.gridLocation.y 
            );
        if (map.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck]);
        }
        //left
        locationToCheck = new Vector2Int(
            curremtOverLayTile.gridLocation.x - 1,
            curremtOverLayTile.gridLocation.y
            );
        if (map.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck]);
        }
        return neighbors;
    }    
}
