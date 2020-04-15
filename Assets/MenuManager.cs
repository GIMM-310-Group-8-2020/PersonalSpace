using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public RawImage targetPicture;
    public Texture grabbedTexture;

    public void GetPicture()
    {
        targetPicture.texture = grabbedTexture;
    }
}
