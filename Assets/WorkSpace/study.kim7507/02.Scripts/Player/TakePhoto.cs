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
        // 현재 프레임이 끝난 후에 캡처하도록 대기
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        // TODO: 플레이어의 화면까지 찍히는 문제 해결 필요

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

        
        // 인벤토리가 비어있을 경우에만 사진을 저장할 수 있도록
        if (targetInventory.HasEmptySlot())
        {
            // TODO: 화면 번쩍임 효과 추가



            GameObject go = Instantiate(photoPrefab);
            go.GetComponentInChildren<RawImage>().texture = photo;

            // 찍은 사진을 인벤토리에 넣기
            go.GetComponent<Pickable>().itemName = "Image";
            go.GetComponent<Pickable>().itemDescription = "Just Image";
            go.GetComponent<Photo>().SetCapturedImageUsingTexture2D(photo);
            go.GetComponent<Pickable>().itemImage = go.GetComponent<Photo>().CaptureObjectAsSprite();
            go.GetComponent<Pickable>().itemObjectPrefab = photoPrefab.GetComponent<Pickable>().itemObjectPrefab;
            

            gameObject.GetComponent<PlayerController>().inventory.AddToInventory(go);
        }
    }
}
