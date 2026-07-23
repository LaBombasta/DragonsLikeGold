using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacter : MonoBehaviour
{
    public int actionPoints = 5;
    public int maxActionPoints = 5;
    public int movementDistance =3;
    [SerializeField]
    protected float traversalSpeed = 5f;
    [SerializeField]
    protected Transform traversalPoint;
    [SerializeField]
    protected LayerMask WhatStopsMovement;
    [SerializeField]
    protected LayerMask WalkableTiles;
    [SerializeField]
    protected bool isCurrentlyActive;

    public OverlayTile activeTile;

    // Start is called before the first frame update
    void Start()
    {
        //traversalPoint.parent = null;
        AssignStartTile();
        
    }

    // Update is called once per frame
    /*
    void Update()
    {
        CharacterMove();
    }

    protected void CharacterMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, traversalPoint.position, traversalSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, traversalPoint.position) <= .05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(traversalPoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, WhatStopsMovement))
                {
                    traversalPoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
                //else { check if interactable, as in any interactions that trigger based off of being moved into, ie. basic dialogue, buy and selling in the overworld}

            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(traversalPoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, WhatStopsMovement))
                {
                    traversalPoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }
    }
    */
    public void MoveAlongPath(List<OverlayTile> path)
    {
        
        var step = traversalSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, path[0].transform.position, step);
        if (Vector2.Distance(transform.position, path[0].transform.position) < 0.0001f)
        {
            
            AssignTile(path[0]);
            path.RemoveAt(0);

        }
    }

    public void SetCharacterActive(bool isActive)
    {
        isCurrentlyActive = isActive;
    }

    public void AssignTile(OverlayTile tile)
    {
        activeTile = tile;
        transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, 0f);
    }
    public void AssignStartTile()
    {
        if (activeTile == null)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, WalkableTiles);
            if (hit.collider != null&&hit.collider.gameObject.GetComponent<OverlayTile>())
            {
                
                OverlayTile myTile = hit.collider.gameObject.GetComponent<OverlayTile>();
                activeTile = myTile;
                transform.position = new Vector3(myTile.transform.position.x, myTile.transform.position.y, 0f);
            }
        }
    }
    
}
    

