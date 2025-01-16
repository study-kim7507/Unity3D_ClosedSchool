using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour
{
    [SerializeField] GameObject photoPrefab;
    private Texture2D photo;

    // ĸó�� ī�޶�
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void Capture()
    {
        // ���� �������� ���� �Ŀ� ĸó�ϵ��� ���
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        // �������� ���� ������ ���
        yield return new WaitForEndOfFrame();

        // RenderTexture ����
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        mainCamera.targetTexture = renderTexture;
        mainCamera.Render();

        // RenderTexture�� Texture2D�� ��ȯ
        RenderTexture.active = renderTexture;

        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // RenderTexture ����
        mainCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // ĸó�� ������ ����
        photo = screenshot;

        GameObject go = Instantiate(photoPrefab);
        go.GetComponentInChildren<RawImage>().texture = photo;

        // ���� ������ �κ��丮�� �ֱ�
        go.GetComponent<IPickable>().ItemName = "Image";
        go.GetComponent<IPickable>().ItemDescription = "Just Image";
        go.GetComponent<IPickable>().ItemImage = photoPrefab.GetComponent<IPickable>().ItemImage;
        go.GetComponent<IPickable>().ItemObjectPrefab = photoPrefab.GetComponent<IPickable>().ItemObjectPrefab;
        go.GetComponent<Photo>().SetCapturedImageUsingTexture2D(photo);

        gameObject.GetComponent<PlayerController>().inventory.AddToInventory(go);
    }
}
