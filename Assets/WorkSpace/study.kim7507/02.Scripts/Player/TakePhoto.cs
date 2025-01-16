using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour
{
    [SerializeField] GameObject photoPrefab;
    private Texture2D photo;

    // 캡처할 카메라
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void Capture()
    {
        // 현재 프레임이 끝난 후에 캡처하도록 대기
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        // 프레임이 끝날 때까지 대기
        yield return new WaitForEndOfFrame();

        // RenderTexture 생성
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        mainCamera.targetTexture = renderTexture;
        mainCamera.Render();

        // RenderTexture를 Texture2D로 변환
        RenderTexture.active = renderTexture;

        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // RenderTexture 해제
        mainCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // 캡처한 사진을 저장
        photo = screenshot;

        GameObject go = Instantiate(photoPrefab);
        go.GetComponentInChildren<RawImage>().texture = photo;

        // 찍은 사진을 인벤토리에 넣기
        go.GetComponent<IPickable>().ItemName = "Image";
        go.GetComponent<IPickable>().ItemDescription = "Just Image";
        go.GetComponent<IPickable>().ItemImage = photoPrefab.GetComponent<IPickable>().ItemImage;
        go.GetComponent<IPickable>().ItemObjectPrefab = photoPrefab.GetComponent<IPickable>().ItemObjectPrefab;
        go.GetComponent<Photo>().SetCapturedImageUsingTexture2D(photo);

        gameObject.GetComponent<PlayerController>().inventory.AddToInventory(go);
    }
}
