using UnityEngine;
using UnityEngine.UI;

public class Photo : MonoBehaviour
{
    public RawImage capturedImage;
    public Camera renderCamera;

    private void Start()
    {
        renderCamera.gameObject.SetActive(false);
    }

    public void SetCapturedImageUsingTexture2D(Texture2D image)
    {
        capturedImage.texture = image;
    }

    public Sprite CaptureObjectAsSprite()
    {
        renderCamera.gameObject.SetActive(true);
        SetLayerRecursivly(gameObject, "UI");

        RenderTexture renderTexture = new RenderTexture(renderCamera.pixelWidth, renderCamera.pixelHeight, 24);
        renderCamera.targetTexture = renderTexture;

        
        renderCamera.Render();

        renderCamera.targetTexture = null;

        // RenderTexture를 Texture2D로 변환
        Texture2D texture2D = new Texture2D(renderCamera.pixelWidth, renderCamera.pixelHeight, TextureFormat.RGBA32, false);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderCamera.pixelWidth, renderCamera.pixelHeight), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;

        // Texture2D를 Sprite로 변환
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, renderCamera.pixelWidth, renderCamera.pixelHeight), new Vector2(0.5f, 0.5f));

        renderCamera.gameObject.SetActive(false);
        SetLayerRecursivly(gameObject, "Default");

        // 생성된 Sprite 반환
        return sprite;
    }


    private void SetLayerRecursivly(GameObject obj, string layerName)
    {
        obj.layer = LayerMask.NameToLayer(layerName);

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursivly(child.gameObject, layerName);
        }
    }
}
