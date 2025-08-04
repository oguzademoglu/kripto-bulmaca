using System.Collections.Generic;
using UnityEngine;

public class CryptogramEngine : MonoBehaviour
{
    [TextArea]
    public string originalSentence = "BİLGİ GÜÇTÜR";
    private Dictionary<char, int> letterToNumber = new();
    private Dictionary<int, char> numberToLetter = new();
    void Start()
    {
        GenerateMapping();
        string encoded = EncodeSentece(originalSentence);
        Debug.Log("Orijinal Cümle: " + originalSentence);
        Debug.Log("Şifreli cümle: " + encoded);
        PrintMapping();
    }


    void GenerateMapping()
    {
        HashSet<char> usedChars = new();
        List<int> availableNumbers = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        System.Random rng = new();

        foreach (char c in originalSentence)
        {
            char upperChar = char.ToUpper(c);
            if (!char.IsLetter(upperChar) || usedChars.Contains(upperChar))
                continue;

            int randomIndex = rng.Next(availableNumbers.Count);
            int chosenNumber = availableNumbers[randomIndex];

            letterToNumber[upperChar] = chosenNumber;
            numberToLetter[chosenNumber] = upperChar;

            usedChars.Add(upperChar);
            availableNumbers.RemoveAt(randomIndex);

            if (availableNumbers.Count == 0) break;
        }
    }

    string EncodeSentece(string sentence)
    {
        string result = "";
        foreach (char c in sentence.ToUpper())
        {
            if (letterToNumber.ContainsKey(c))
            {
                result += letterToNumber[c];
            }
            else
            {
                result += c;
            }
        }
        return result;
    }
    void PrintMapping()
    {
        foreach (var pair in letterToNumber)
        {
            Debug.Log($"{pair.Key} = {pair.Value}");
        }
    }
}
