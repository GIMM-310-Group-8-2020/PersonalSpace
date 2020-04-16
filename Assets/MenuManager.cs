using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public RawImage targetPicture;

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
}
