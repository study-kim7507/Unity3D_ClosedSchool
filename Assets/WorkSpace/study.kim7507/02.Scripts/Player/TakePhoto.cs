using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour
{
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

        // Image ������Ʈ�� ��ũ������ ǥ��
        // inventory.ReturnFreeSlot().itemImage.sprite = Sprite.Create(photo, new Rect(0, 0, photo.width, photo.height), new Vector2(0.5f, 0.5f));
    }
}
