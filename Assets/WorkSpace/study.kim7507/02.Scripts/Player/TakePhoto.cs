using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour
{
    [SerializeField] GameObject photoPrefab;
    private Texture2D photo;

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
        Camera.main.targetTexture = renderTexture;
        Camera.main.Render();

        // RenderTexture를 Texture2D로 변환
        RenderTexture.active = renderTexture;

        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // RenderTexture 해제
        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // 캡처한 사진을 저장
        photo = screenshot;

        gameObject.GetComponent<PlayerController>().playerUI.PlayerTakePhoto();                      // 사진찍는 효과 (번쩍임)

        GameObject go = Instantiate(photoPrefab);
        go.transform.Find("Front").Find("Image").gameObject.GetComponent<MeshRenderer>().material.mainTexture = photo;

        // 찍은 사진을 인벤토리에 넣기
        go.GetComponent<Pickable>().itemName = "사진";
        go.GetComponent<Pickable>().itemDescription = gameObject.GetComponent<PlayerController>().playerUI.timer.text + "에 찍은 사진이다. \n무엇이 찍혔는지 자세히 확인해보자.";
        go.GetComponent<Photo>().SetCapturedImageUsingTexture2D(photo);
        go.GetComponent<Pickable>().itemImage = go.GetComponent<Photo>().CaptureObjectAsSprite();
        go.GetComponent<Pickable>().itemObjectPrefab = photoPrefab.GetComponent<Pickable>().itemObjectPrefab;

        gameObject.GetComponent<PlayerController>().inventory.AddToInventory(go);
    }

    
}
