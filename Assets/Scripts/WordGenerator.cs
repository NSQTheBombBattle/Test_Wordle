using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordGenerator : MonoBehaviour
{
    [SerializeField] private List<string> wordSamples;
    [SerializeField] private List<Slot> slotList;
    [SerializeField] private GameObject uiHintPrefab;
    [SerializeField] private GameObject uiParentPrefab;
    [SerializeField] private Transform hintParent;
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
            //ResetSlots();
        }
    }

    private void PickRandomWord()
    {
        string targetString = wordSamples[Random.Range(0, wordSamples.Count)];
        for (int i = 0; i < targetString.Length; i++)
        {
            char character = targetString[i];
            charList.Add(char.ToLower(character));
        }
    }

    private void CheckInput()
    {
        foreach (char c in Input.inputString)
        {
            if (!char.IsLetter(c))
                continue;
            if (inputCharList.Contains(c))
                continue;
            slotList[inputCharList.Count].UpdateText(c);
            inputCharList.Add(c);
            if (inputCharList.Count >= 5)
            {
                ProcessInput();
            }
        }
    }

    private void ProcessInput()
    {
        int correctCharCount = 0;
        GameObject currentUIParent = Instantiate(uiParentPrefab, hintParent);
        for (int i = 0; i < inputCharList.Count; i++)
        {
            GameObject hintInstance = Instantiate(uiHintPrefab, currentUIParent.transform);
            hintInstance.GetComponent<UISlotHint>().hintText.text = inputCharList[i].ToString();
            if (inputCharList[i] == charList[i])
            {
                hintInstance.GetComponent<Image>().color = Color.green;
                correctCharCount += 1;
            }
            else if (charList.Contains(inputCharList[i]))
            {
                hintInstance.GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                hintInstance.GetComponent<Image>().color = Color.red;
            }
        }
        if(correctCharCount == charList.Count)
        {
            Win();
        }
        inputCharList.Clear();
        ResetSlots();
    }

    private void ResetSlots()
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            slotList[i].ResetSlot();
        }
    }

    private void Win()
    {
        Debug.Log("You found the word!");
    }
}
