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

        ownerPlayer.playerUI.PlayerTakePhoto();                      // 사진찍는 효과 (번쩍임)
        takePhotoAudioSource.Play();

        GameObject go = Instantiate(photoPrefab);
        go.transform.Find("Front").Find("Image").gameObject.GetComponent<MeshRenderer>().material.mainTexture = photo;

        // 찍은 사진을 인벤토리에 넣기
        if (go.TryGetComponent<Pickable>(out Pickable pickable))
        {
            pickable.itemName = "사진";
            pickable.itemDescription = ownerPlayer.playerUI.timer.text + "에 찍은 사진이다. \n무엇이 찍혔는지 자세히 확인해보자.";
            go.GetComponent<Photo>().SetPhotoImage(photo);
            pickable.itemImage = go.GetComponent<Photo>().CapturePhotoObjectAsSprite();
            pickable.itemObjectPrefab = photoPrefab.GetComponent<Pickable>().itemObjectPrefab;
        }


        ownerPlayer.inventory.AddToInventory(go);
    }
}
