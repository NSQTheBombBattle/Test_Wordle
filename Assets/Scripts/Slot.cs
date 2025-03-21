using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public TMP_Text textObject;
    public char currentChar;

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

    public void ChangeSlotColour(Color colourToChange)
    {
        spriteRenderer.color = colourToChange;
    }

    public void ResetSlot()
    {
        UpdateText('\0');
        ChangeSlotColour(Color.white);
    }
}
