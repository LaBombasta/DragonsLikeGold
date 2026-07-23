using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public BasicCharacter currentCharacter;
    public Cursor mouseCursor;
    
   

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        mouseCursor = FindAnyObjectByType<Cursor>();
        mouseCursor.AssignCurrentCharacter(currentCharacter);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AssignCurrentCharacter(BasicCharacter newCharacter)
    {
        if(currentCharacter != null)
        {
            currentCharacter.SetCharacterActive(false);
        }
        currentCharacter = newCharacter;
        currentCharacter.SetCharacterActive(true);
    }
}
