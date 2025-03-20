using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    [SerializeField] private List<string> wordSamples;
    [SerializeField] private List<Slot> slotList;

    // Start is called before the first frame update
    void Start()
    {
        PickRandomWord();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PickRandomWord()
    {
        string targetString = wordSamples[Random.Range(0, wordSamples.Count)];
        for (int i = 0; i < targetString.Length; i++)
        {
            char character = targetString[i];
            slotList[i].textObject.text = character.ToString();
        }
    }
}
