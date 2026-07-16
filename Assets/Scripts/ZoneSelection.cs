using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSelection : MonoBehaviour
{
    public Transform characterAnchor;
    private SpriteRenderer zoneSprite;
    private bool isOccupied;
    
    public LayerMask imOccupied;
    public Collider2D myCollider;
    ContactFilter2D filter = new ContactFilter2D();
    private void Awake()
    {
        zoneSprite = GetComponent<SpriteRenderer>();

    }
    // Start is called before the first frame update
    void Start()
    {
        zoneSprite.color = new Color(1f, 1f, 1f, 0f);
        filter.useLayerMask = true;
        filter.SetLayerMask(imOccupied);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if(!CheckOccupied())
        {
            zoneSprite.color = new Color(1f, 1f, 1f, .25f);
        }
    }

    private void OnMouseExit()
    {
        zoneSprite.color = new Color(1f, 1f, 1f, 0f);
    }

    public bool CheckOccupied()
    {
        Collider2D[] results = new Collider2D[1];
        
        int overlaps = myCollider.OverlapCollider(filter, results);

        return overlaps > 0;
    }
}
