using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    public int movementCost = 1;
    public Transform characterAnchor;
    
    private SpriteRenderer tileSprite;
    
    public LayerMask imOccupied;
    public Collider2D myCollider;
    ContactFilter2D filter = new ContactFilter2D();

    //PathFinding
    public int G;
    public int H;
    public int F { get { return G + H; } }

    public bool isOccupied;

    public OverlayTile previous;
    public Vector2Int gridLocation;
    private void Awake()
    {
        tileSprite = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        tileSprite.color = new Color(1f, 1f, 1f, 0f);
        filter.useLayerMask = true;
        filter.SetLayerMask(imOccupied);
        isOccupied = CheckOccupied();
        
    }

    public bool CheckOccupied()
    {
        Collider2D[] results = new Collider2D[1];
        
        int overlaps = myCollider.OverlapCollider(filter, results);
        if( overlaps > 0)
        {
            isOccupied = true;
            foreach (var theObject in results)
            {
                Debug.Log(theObject.gameObject.name + " " + gridLocation);

            }
        }else
        {
            isOccupied = false;
        }

        return overlaps > 0;
    }
    public void ShowTile()
    {
        tileSprite.color = new Color(1f, 1f, 1f, .25f);
    }
    public void HideTile()
    {
        tileSprite.color = new Color(1f, 1f, 1f, 0f);
    }

    
}
