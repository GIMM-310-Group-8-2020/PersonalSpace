using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomization : MonoBehaviour
{
    public Button HairButton;
    public Button SkinButton;
    public Button HandButton;
    public Button EyeButton;
    public Button OutfitButton;

    public RawImage[] DropDownMenus;


    public Sprite[] longHair;
    public Sprite[] shortHair;
    public Sprite[] eyes;
    public Sprite[] outfit;
    public Sprite[] skinColor;
    public Sprite[] handColor;


    public void ChangeHairColor(string hairColor)
    {
        Vector2 finalPos = new Vector2(0, 1920);
        DropDownMenus[1].transform.localPosition = finalPos;
 
        switch (hairColor)
        {
            case "Super Blonde Hair":
                HairButton.GetComponent<Image>().sprite = longHair[0];
                break;

            case "Blonde Hair":
                HairButton.GetComponent<Image>().sprite = longHair[1];
                break;

            case "Brown Hair":
                HairButton.GetComponent<Image>().sprite = longHair[2];
                break;

            case "Black Hair":
                HairButton.GetComponent<Image>().sprite = longHair[3];
                break;

            case "Red Hair":
                HairButton.GetComponent<Image>().sprite = longHair[4];
                break;

            case "Orange Hair":
                HairButton.GetComponent<Image>().sprite = longHair[5];
                break;
        }
        
    }

    public void ChangeEyeColor(string eyeColor)
    {
        Vector2 finalPos = new Vector2(0, 1920);
        DropDownMenus[2].transform.localPosition = finalPos;

        
        switch (eyeColor)
        {
            case "Blue Eyes":
                EyeButton.GetComponent<Image>().sprite = eyes[0];
                break;

            case "Brown Eyes":
                EyeButton.GetComponent<Image>().sprite = eyes[1];
                break;

            case "Green Eyes":
                EyeButton.GetComponent<Image>().sprite = eyes[2];
                break;

            case "Hazel Eyes":
                EyeButton.GetComponent<Image>().sprite = eyes[3];
                break;
        }
        
    }

    public void ChangeSkinColor(string skin)
    {
        Vector2 finalPos = new Vector2(0, 1920);
        DropDownMenus[0].transform.localPosition = finalPos;

        switch (skin)
        {
            case "Half Light":
                SkinButton.GetComponent<Image>().sprite = skinColor[0];
                HandButton.GetComponent<Image>().sprite = handColor[0];
                break;

            case "Light":
                SkinButton.GetComponent<Image>().sprite = skinColor[1];
                HandButton.GetComponent<Image>().sprite = handColor[1];
                break;

            case "Half Medium":
                SkinButton.GetComponent<Image>().sprite = skinColor[2];
                HandButton.GetComponent<Image>().sprite = handColor[2];
                break;

            case "Medium":
                SkinButton.GetComponent<Image>().sprite = skinColor[3];
                HandButton.GetComponent<Image>().sprite = handColor[3];
                break;

            case "Half Dark":
                SkinButton.GetComponent<Image>().sprite = skinColor[4];
                HandButton.GetComponent<Image>().sprite = handColor[4];
                break;

            case "Dark":
                SkinButton.GetComponent<Image>().sprite = skinColor[5];
                HandButton.GetComponent<Image>().sprite = handColor[5];
                break;
        }

    }

    public void ChangeOutfit(string outfitSelection)
    {
        Vector2 finalPos = new Vector2(0, 1920);
        DropDownMenus[3].transform.localPosition = finalPos;

        switch (outfitSelection)
        {
            case "Outfit 1":
                OutfitButton.GetComponent<Image>().sprite = outfit[0];
                break;

            case "Outfit 2":
                OutfitButton.GetComponent<Image>().sprite = outfit[1];
                break;

            case "Outfit 3":
                OutfitButton.GetComponent<Image>().sprite = outfit[2];
                break;

            case "Default":
                OutfitButton.GetComponent<Image>().sprite = outfit[3];
                break;

        }

    }
}
