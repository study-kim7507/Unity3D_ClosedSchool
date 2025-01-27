using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour
{
    [SerializeField] GameObject photoPrefab;
    private Texture2D photo;

    private PlayerController ownerPlayer;

    public AudioSource takePhotoAudioSource;
    private void Start()
    {
        ownerPlayer = gameObject.GetComponent<PlayerController>();
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

        ownerPlayer.playerUI.PlayerTakePhoto();                      // ������� ȿ�� (��½��)
        takePhotoAudioSource.Play();

        GameObject go = Instantiate(photoPrefab);
        go.transform.Find("Front").Find("Image").gameObject.GetComponent<MeshRenderer>().material.mainTexture = photo;

        // ���� ������ �κ��丮�� �ֱ�
        if (go.TryGetComponent<Pickable>(out Pickable pickable))
        {
            pickable.itemName = "����";
            pickable.itemDescription = ownerPlayer.playerUI.timer.text + "�� ���� �����̴�. \n������ �������� �ڼ��� Ȯ���غ���.";
            go.GetComponent<Photo>().SetPhotoImage(photo);
            pickable.itemImage = go.GetComponent<Photo>().CapturePhotoObjectAsSprite();
            pickable.itemObjectPrefab = photoPrefab.GetComponent<Pickable>().itemObjectPrefab;
        }


        ownerPlayer.inventory.AddToInventory(go);
    }
}
