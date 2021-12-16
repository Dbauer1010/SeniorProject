using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card1 : MonoBehaviour
{
    

    public string value = "0";

    public string GetValueOfCard()
    {
        return value;
    }

    public void SetValue(string newValue)
    {
        value = newValue;
    }

    public string GetSpriteName()
    {
        return GetComponent<SpriteRenderer>().sprite.name;
    }

    public void SetSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    public void ResetCard()
    {
        Sprite back = GameObject.Find("Deck").GetComponent<Deck1>().GetCardBack();
        gameObject.GetComponent<SpriteRenderer>().sprite = back;
        value = "0";
    }
}
