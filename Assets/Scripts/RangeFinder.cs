using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeFinder : MonoBehaviour
{
   public List<OverlayTile> GetTilesinRange(OverlayTile startingTile, int range)
    {
        var inRangeTiles = new List<OverlayTile>();
        int stepCount = 0;

        inRangeTiles.Add(startingTile);

        var tileForPrevStep = new List<OverlayTile>();
        tileForPrevStep.Add(startingTile);
        
        while (stepCount < range)
        {
            Debug.Log(stepCount);
            var surroundTiles = new List<OverlayTile>();
            
            foreach(var item in tileForPrevStep)
            {
                surroundTiles.AddRange(BoardManager.BoardInstance.GetNeighborTiles(item));
            }
            inRangeTiles.AddRange(surroundTiles);
            tileForPrevStep = surroundTiles.Distinct().ToList();
            stepCount++;

        }
        return inRangeTiles.Distinct().ToList();
    }
}
