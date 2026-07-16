using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
    public GameObject gameSpaces;
    public int rows;
    public int columns;
    // Start is called before the first frame update
    void Start()
    {
        GenerateBoardSpaces();
    }

    // Update is called once per frame
    public void GenerateBoardSpaces()
    {
        float rowStart = 0;
        float columnStart = 0;
        for(int r = 0; r< rows; r++)
        {
            columnStart = 0;
            for(int c = 0; c<columns;c++)
            {
                Instantiate(gameSpaces, new Vector3(transform.position.x+columnStart,transform.position.y+rowStart,0f), Quaternion.identity);
                columnStart += 1;
            }
            rowStart -= 1;
        }
    }
}
