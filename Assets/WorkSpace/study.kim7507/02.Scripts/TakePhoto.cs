using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour
{
    public Image image;
    private Texture2D photo;


    // TODO: 찍은 사진이 인벤토리에 저장되도록 로직 구현 필요

    public void Capture()
    {
        // 현재 프레임이 끝난 후에 캡처하도록 대기
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        // 프레임이 끝날 때까지 대기
        yield return new WaitForEndOfFrame();

        // 스크린샷을 위한 Texture2D 생성
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        // 현재 화면의 픽셀을 읽어 Texture2D에 저장
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // 캡처한 사진을 저장
        photo = screenshot;

        // Image 컴포넌트에 스크린샷을 표시
        image.sprite = Sprite.Create(photo, new Rect(0, 0, photo.width, photo.height), new Vector2(0.5f, 0.5f));
    }
}
