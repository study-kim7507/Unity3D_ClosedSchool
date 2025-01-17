using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour
{
    [SerializeField] GameObject photoPrefab;
    private Texture2D photo;

    [SerializeField] InventorySystem targetInventory;

    public void Capture()
    {
        // ���� �������� ���� �Ŀ� ĸó�ϵ��� ���
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        // TODO: �÷��̾��� ȭ����� ������ ���� �ذ� �ʿ�

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

        
        // �κ��丮�� ������� ��쿡�� ������ ������ �� �ֵ���
        if (targetInventory.HasEmptySlot())
        {
            // TODO: ȭ�� ��½�� ȿ�� �߰�



            GameObject go = Instantiate(photoPrefab);
            go.GetComponentInChildren<RawImage>().texture = photo;

            // ���� ������ �κ��丮�� �ֱ�
            go.GetComponent<Pickable>().itemName = "Image";
            go.GetComponent<Pickable>().itemDescription = "Just Image";
            go.GetComponent<Photo>().SetCapturedImageUsingTexture2D(photo);
            go.GetComponent<Pickable>().itemImage = go.GetComponent<Photo>().CaptureObjectAsSprite();
            go.GetComponent<Pickable>().itemObjectPrefab = photoPrefab.GetComponent<Pickable>().itemObjectPrefab;
            

            gameObject.GetComponent<PlayerController>().inventory.AddToInventory(go);
        }
    }
}
