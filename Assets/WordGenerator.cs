using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    [SerializeField] private List<string> wordSamples;
    [SerializeField] private List<Slot> slotList;
    private List<char> charList = new List<char>();
    private List<char> inputCharList = new List<char>();

    // Start is called before the first frame update
    void Start()
    {
        PickRandomWord();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < slotList.Count; i++)
            {
                slotList[i].ResetSlot();
            }
        }
    }

    private void PickRandomWord()
    {
        string targetString = wordSamples[Random.Range(0, wordSamples.Count)];
        for (int i = 0; i < targetString.Length; i++)
        {
            char character = targetString[i];
            charList.Add(character);
        }
    }

    private void CheckInput()
    {
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c)) // Check if the input is a letter
            {
                if (inputCharList.Contains(c))
                    return;
                slotList[inputCharList.Count].UpdateText(c);
                inputCharList.Add(c);
                if (inputCharList.Count >= 5)
                {
                    ProcessInput();
                }
            }
        }
    }

    private void ProcessInput()
    {
        for(int i = 0; i < inputCharList.Count; i++)
        {
            if(inputCharList[i] == charList[i])
            {
                slotList[i].ChangeSlotColour(Color.green);
            }
            else if (charList.Contains(inputCharList[i]))
            {
                slotList[i].ChangeSlotColour(Color.yellow);
            }
            else
            {
                slotList[i].ChangeSlotColour(Color.red);
            }
        }
        inputCharList.Clear();
    }
}
