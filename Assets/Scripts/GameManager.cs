using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    BasicCharacter currentCharacter; 

    // Start is called before the first frame update
    void Start()
    {
        
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
