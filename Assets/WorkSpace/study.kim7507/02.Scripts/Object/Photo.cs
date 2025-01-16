using UnityEngine;
using UnityEngine.UI;

public class Photo : MonoBehaviour
{
    public RawImage capturedImage;

    public void SetCapturedImageUsingTexture2D(Texture2D image)
    {
        capturedImage.texture = image;
    }
}
