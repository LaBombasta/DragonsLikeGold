using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacter : MonoBehaviour
{
    [SerializeField]
    protected float traversalSpeed = 5f;
    [SerializeField]
    protected Transform traversalPoint;
    [SerializeField]
    protected LayerMask WhatStopsMovement;
    [SerializeField]
    protected bool isCurrentlyActive;

    // Start is called before the first frame update
    void Start()
    {
        traversalPoint.parent = null;
    }

    // Update is called once per frame
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

    public void SetCharacterActive(bool isActive)
    {
        isCurrentlyActive = isActive;
    }
}
    

