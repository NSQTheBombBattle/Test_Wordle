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
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c)) // Check if the input is a letter
            {
                inputCharList.Add(c);
            }
        }
    }

    private void PickRandomWord()
    {
        string targetString = wordSamples[Random.Range(0, wordSamples.Count)];
        for (int i = 0; i < targetString.Length; i++)
        {
            char character = targetString[i];
            slotList[i].UpdateText(character);
            charList.Add(character);
        }
    }
}
