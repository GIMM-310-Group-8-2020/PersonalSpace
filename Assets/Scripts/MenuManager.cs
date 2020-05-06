﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DataBank;
using UnityEditor;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public RawImage targetPicture;
    public RawImage backgroundImage;
    public RawImage[] dropDownMenus;

    public float dropdownSpeed = 1f;

    private Transform backgroundPos;
    private int characterTypeVal;

    //private GameObject[] characters;


    private void Start()
    {
        //Make MenuManager accessible from any script
        Instance = this;

        backgroundPos = backgroundImage.GetComponent<Transform>();

        /*CharacterDB mCharacterDB = new CharacterDB();
        mCharacterDB.deleteAllData();
        mCharacterDB.close();*/

        //characters = Resources.LoadAll<GameObject>("Characters/");
    }

    public void GetPicture()
    {
		NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
		{
			Debug.Log("Image path: " + path);
			if (path != null)
			{
				// Create Texture from selected image
				Texture2D texture = NativeGallery.LoadImageAtPath(path, 1028);
				if (texture == null)
				{
					Debug.Log("Couldn't load texture from " + path);
					return;
				}

				targetPicture.texture = texture;
			}
		}, "Select a PNG image", "image/png");

		Debug.Log("Permission result: " + permission);

        Debug.Log("Hello");
	}

    public void TakePicture(int maxSize)
    {
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create a Texture2D from the captured image
                Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                // Assign texture to a temporary quad and destroy it after 5 seconds
                GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                quad.transform.forward = Camera.main.transform.forward;
                quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

                Material material = quad.GetComponent<Renderer>().material;
                if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                    material.shader = Shader.Find("Legacy Shaders/Diffuse");

                material.mainTexture = texture;

                Destroy(quad, 5f);

                // If a procedural texture is not destroyed manually, 
                // it will only be freed after a scene change
                Destroy(texture, 5f);
            }
        }, maxSize);

        Debug.Log("Permission result: " + permission + ", Taking Photo");
    }

    public void Screenshot()
    {
        Debug.Log("Are you taking a screenshot?");

       //NativeGallery.SaveImageToGallery(byte[] mediaBytes, string album, string filename, MediaSaveCallback callback = null);

        StartCoroutine(TakeScreenshotAndSave());
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        // Save the screenshot to Gallery/Photos
        Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, "GalleryTest", "Image.png"));

        // To avoid memory leaks
        Destroy(ss);
    }

    public void MenuDrop(string type)
    {
       switch(type)
        {
            case "Hair":
                //Get current transform of the Drop Down Menu
                Debug.Log("Clicking hair");
                Transform dropmenuPos = dropDownMenus[1].GetComponent<Transform>();

                //Get starting position for the Drop Down Menu and position of the background
                Vector2 startPos = dropmenuPos.localPosition;
                Vector2 finalPos = backgroundPos.localPosition;

                Debug.Log("Starting Position: " + startPos + "/n Final Position: " + finalPos);

                dropDownMenus[1].transform.localPosition = finalPos;
                break;

            case "Outfit":
                //Get current transform of the Drop Down Menu
                Debug.Log("Clicking outfit");

                //check if female or male is selected
                if(DropdownMenu.Instance.genVal == 0)
                {
                    Debug.Log("Female outfits selected");

                    //Get current transform of the drop down menu
                    Transform femaleOutfitMenuPos = dropDownMenus[4].GetComponent<Transform>();

                    //Get starting position for the Drop Down Menu and position of the background
                    Vector2 firstPos = femaleOutfitMenuPos.localPosition;
                    Vector2 lastPos = backgroundPos.localPosition;

                    Debug.Log("Starting Position: " + firstPos + "/n Final Position: " + lastPos);

                    dropDownMenus[4].transform.localPosition = lastPos;
                }
                
                else if(DropdownMenu.Instance.genVal == 1)
                {
                    Debug.Log("Male outfits selected");

                    //Get current transform of the drop down menu
                    Transform maleOutfitMenuPos = dropDownMenus[5].GetComponent<Transform>();

                    //Get starting position for the Drop Down Menu and position of the background
                    Vector2 firstPos = maleOutfitMenuPos.localPosition;
                    Vector2 lastPos = backgroundPos.localPosition;

                    Debug.Log("Starting Position: " + firstPos + "/n Final Position: " + lastPos);

                    dropDownMenus[5].transform.localPosition = lastPos;
                }
                break;

            case "Body":
                //Get current transform of the Drop Down Menu
                Debug.Log("Clicking body");
                Transform hairMenuPos = dropDownMenus[0].GetComponent<Transform>();

                //Get starting position for the Drop Down Menu and position of the background
                Vector2 starPos = hairMenuPos.localPosition;
                Vector2 finaPos = backgroundPos.localPosition;

                Debug.Log("Starting Position: " + starPos + "/n Final Position: " + finaPos);

                dropDownMenus[0].transform.localPosition = finaPos;
                break;

            case "Eyes":
                //Get current transform of the Drop Down Menu
                Debug.Log("Clicking eyes");
                Transform eyeMenuPos = dropDownMenus[2].GetComponent<Transform>();

                //Get starting position for the Drop Down Menu and position of the background
                Vector2 startingPos = eyeMenuPos.localPosition;
                Vector2 finalPosition = backgroundPos.localPosition;

                Debug.Log("Starting Position: " + startingPos + "/n Final Position: " + finalPosition);

                dropDownMenus[2].transform.localPosition = finalPosition;
                break;


        }

    }

    //Create input handler for character type drop down menu
    public void CharacterTypeInput(int val)
    {
        //store the value from the character type drop down menu
        //so it can be accessed anywhere
        characterTypeVal = val;

        if(val == 0)
        {
            Debug.Log("Character type: Family");
        }
        else if(val == 1)
        {
            Debug.Log("Character type: Friend");
        }
    }

    public void NextScene(string name)
    {
        //Check which scene to load
        switch (name)
        {
            case "Menu":
                SceneManager.LoadScene("Menu");
                break;

            case "ARSection":
                SceneManager.LoadScene("ARSection");
                break;

            case "Upload Pic":
                SceneManager.LoadScene("UploadPicOrNotSection");
                break;

            case "Character Customization":
                SceneManager.LoadScene("CharacterCustomization");
                Debug.Log("Character Customization Button Clicked");
                break;

            case "ColorPicker":
                SceneManager.LoadScene("ColorPicker");
                Debug.Log("Color Picker Button Clicked");
                break;
        }
    }

    public void SaveCharacter()
    {
        GameObject character = new GameObject();
        /*if (DropdownMenu.Instance.genVal == 0)
        {
            try
            {
                character = GameObject.Find("Canvas/FemaleCharacter");
                Debug.Log("Female Character successfully found");
            }
            catch
            {
                Debug.Log("Female Character not found");
            }
        }
        else if (DropdownMenu.Instance.genVal == 1)
        {
            try
            {
                character = GameObject.Find("Canvas/MaleCharacter");
                Debug.Log("Male Character successfully found");
            }
            catch
            {
                Debug.Log("Male Character not found");
            }

        }*/
        character = GameObject.Find("cube");
        CharacterSave save = new CharacterSave();
        save.character = character;
        save.distance = DropdownMenu.Instance.GetDist();
        FileStream file = File.Create(Application.persistentDataPath + "/charactersave.save");

        //serialize to xml
        DataContractSerializer bf = new DataContractSerializer(save.GetType());
        MemoryStream streamer = new MemoryStream();

        //serialize the file
        bf.WriteObject(streamer, save);
        streamer.Seek(0, SeekOrigin.Begin);

        //save to disk
        file.Write(streamer.GetBuffer(), 0, streamer.GetBuffer().Length);
        file.Close();




        /*CharacterDB mCharacterDB = new CharacterDB();
        //mCharacterDB.deleteAllData();
        mCharacterDB.addData(new CharacterData("0", DropdownMenu.Instance.GetDist(), character));
        mCharacterDB.close();*/

        //NextScene("ARSection");
            SceneManager.LoadScene("TestScene");
        
    }

    /*public void NextScene()
    {
        SceneManager.LoadScene("ARSection");
    }*/
    
}
