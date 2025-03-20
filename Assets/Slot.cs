using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour
{
    public TMP_Text textObject;
    private char currentChar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(char textToUpdate)
    {
        currentChar = textToUpdate;
        textObject.text = textToUpdate.ToString();
    }
}
