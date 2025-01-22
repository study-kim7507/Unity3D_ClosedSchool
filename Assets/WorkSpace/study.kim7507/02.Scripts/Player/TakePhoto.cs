using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour
{
    [SerializeField] GameObject photoPrefab;
    private Texture2D photo;

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
        Camera.main.targetTexture = renderTexture;
        Camera.main.Render();

        // RenderTexture�� Texture2D�� ��ȯ
        RenderTexture.active = renderTexture;

        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // RenderTexture ����
        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // ĸó�� ������ ����
        photo = screenshot;

        gameObject.GetComponent<PlayerController>().playerUI.PlayerTakePhoto();                      // ������� ȿ�� (��½��)

        GameObject go = Instantiate(photoPrefab);
        go.transform.Find("Front").Find("Image").gameObject.GetComponent<MeshRenderer>().material.mainTexture = photo;

        // ���� ������ �κ��丮�� �ֱ�
        go.GetComponent<Pickable>().itemName = "����";
        go.GetComponent<Pickable>().itemDescription = gameObject.GetComponent<PlayerController>().playerUI.timer.text + "�� ���� �����̴�. \n������ �������� �ڼ��� Ȯ���غ���.";
        go.GetComponent<Photo>().SetCapturedImageUsingTexture2D(photo);
        go.GetComponent<Pickable>().itemImage = go.GetComponent<Photo>().CaptureObjectAsSprite();
        go.GetComponent<Pickable>().itemObjectPrefab = photoPrefab.GetComponent<Pickable>().itemObjectPrefab;

        gameObject.GetComponent<PlayerController>().inventory.AddToInventory(go);
    }

    
}
