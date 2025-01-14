using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour
{
    public Image image;
    private Texture2D photo;


    // TODO: ���� ������ �κ��丮�� ����ǵ��� ���� ���� �ʿ�

    public void Capture()
    {
        // ���� �������� ���� �Ŀ� ĸó�ϵ��� ���
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        // �������� ���� ������ ���
        yield return new WaitForEndOfFrame();

        // ��ũ������ ���� Texture2D ����
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        // ���� ȭ���� �ȼ��� �о� Texture2D�� ����
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // ĸó�� ������ ����
        photo = screenshot;

        // Image ������Ʈ�� ��ũ������ ǥ��
        image.sprite = Sprite.Create(photo, new Rect(0, 0, photo.width, photo.height), new Vector2(0.5f, 0.5f));
    }
}
