using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WordGenerator : MonoBehaviour
{
    [SerializeField] private List<Slot> slotList;
    [SerializeField] private GameObject uiHintPrefab;
    [SerializeField] private GameObject uiParentPrefab;
    [SerializeField] private Transform hintParent;
    private const int CHAR_LENGTH = 5;
    private List<string> wordSamples = new List<string>();
    private List<char> charList = new List<char>();
    private List<char> inputCharList = new List<char>();

    // Start is called before the first frame update
    void Start()
    {
        ReadTextFile();
        PickRandomWord();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PickRandomWord();
        }
    }

    private void ReadTextFile()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Words");
        if (textAsset != null)
        {
            string[] lines = textAsset.text.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
            wordSamples = new List<string>(lines);
        }
        else
        {
            Debug.LogError("Words.txt not found in Resources folder!");
        }
    }

    private void PickRandomWord()
    {
        ResetSlots();
        charList.Clear();
        inputCharList.Clear();
        for (int i = hintParent.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(hintParent.GetChild(i).gameObject);
        }
        string targetString = wordSamples[Random.Range(0, wordSamples.Count)];
        for (int i = 0; i < CHAR_LENGTH; i++)
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
            if (inputCharList.Count >= CHAR_LENGTH)
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
        if(correctCharCount == CHAR_LENGTH)
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
