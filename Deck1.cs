using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Deck1 : MonoBehaviour
{
    public Sprite[] cardFaces;
    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    int currentIndex = 0;
    string[] deck2 = new string[52];
    
    void Start()
    {
        GenerateDeck();
    }
 
    public void GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string s in suits)
        {
            foreach (string v in values)
            {
                newDeck.Add(s + v);
            }
        }

        string[] tem = newDeck.ToArray();
        deck2 = tem;
    }

    public void Shuffle()
    {
        // Standard array data swapping technique

        for (int i = cardFaces.Length - 1; i > 0; --i)
        {
            int j = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * cardFaces.Length - 1) + 1;
            Sprite face = cardFaces[i];
            cardFaces[i] = cardFaces[j];
            cardFaces[j] = face;

            string value = deck2[i];
            deck2[i] = deck2[j];
            deck2[j] = value;
        }
        currentIndex = 1;
    }

   

    public string[] GetList()
    {
       
        return deck2;
    }

    public string DealCard(Card1 cardScript)
    {
        cardScript.SetSprite(cardFaces[currentIndex]);
        currentIndex++;
        return cardScript.GetValueOfCard();
    }

    public Sprite GetCardBack()
    {
        return cardFaces[0];
    }
}
