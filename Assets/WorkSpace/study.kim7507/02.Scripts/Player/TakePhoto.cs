using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum GhostType
{
    None,
    LibraryGhost,
    OneCorridorGhost
};

public class TakePhoto : MonoBehaviour
{
    [SerializeField] GameObject photoPrefab;
    private Texture2D photo;

    private PlayerController ownerPlayer;
    private GameObject[] ghostObjects;
    [HideInInspector] public GhostType ghostType;

    public AudioSource takePhotoAudioSource;
    private void Start()
    {
        ownerPlayer = gameObject.GetComponent<PlayerController>();

        ownerPlayer = gameObject.GetComponent<PlayerController>();

        // "LibraryGhost"와 "OneCorriDorGhost" 태그가 붙은 모든 오브젝트를 찾고 저장
        GameObject[] libraryGhosts = GameObject.FindGameObjectsWithTag("LibraryGhost");
        GameObject[] oneCorriDorGhosts = GameObject.FindGameObjectsWithTag("OneCorridorGhost");

        // 두 배열을 합쳐서 ghostObjects에 저장
        ghostObjects = new GameObject[libraryGhosts.Length + oneCorriDorGhosts.Length];
        libraryGhosts.CopyTo(ghostObjects, 0);
        oneCorriDorGhosts.CopyTo(ghostObjects, libraryGhosts.Length);
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
        
        bool isInGhost = CheckForGhostInPhoto();

        ownerPlayer.playerUI.PlayerTakePhoto();                      // 사진찍는 효과 (번쩍임)
        takePhotoAudioSource.Play();

        GameObject go = Instantiate(photoPrefab);
        go.transform.Find("Front").Find("Image").gameObject.GetComponent<MeshRenderer>().material.mainTexture = photo;

        // 찍은 사진을 인벤토리에 넣기
        if (go.TryGetComponent<Pickable>(out Pickable pickable))
        {
            pickable.itemName = "사진";
            if (isInGhost) pickable.itemDescription = ownerPlayer.playerUI.timer.text + "에 찍은 사진이다. \n퇴마하려는 귀신이 찍혀있다. \n퇴마에 사용할 수 있을 것 같다.";
            else pickable.itemDescription = ownerPlayer.playerUI.timer.text + "에 찍은 사진이다. \n퇴마하려는 귀신이 찍혀있지는 않은 것 같다.";
            go.GetComponent<Photo>().SetPhotoImage(photo);
            go.GetComponent<Photo>().isInGhost = isInGhost;
            go.GetComponent<Photo>().ghostType = ghostType;
            pickable.itemImage = go.GetComponent<Photo>().CapturePhotoObjectAsSprite();
            pickable.itemObjectPrefab = photoPrefab.GetComponent<Pickable>().itemObjectPrefab;
        }


        ownerPlayer.inventory.AddToInventory(go);
    }

    private bool CheckForGhostInPhoto()
    {
        for (int i = 0; i < ghostObjects.Length; i++)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(ghostObjects[i].transform.position);
            
            // 오브젝트가 카메라의 뷰포트 내에 있는지 확인
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                // 카메라와 귀신 오브젝트 간의 방향 벡터
                Vector3 directionToGhost = ghostObjects[i].transform.position - Camera.main.transform.position;

                // Raycast를 사용하여 가려지는지 확인
                if (Physics.Raycast(Camera.main.transform.position, directionToGhost, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == ghostObjects[i])
                    {
                        if (ghostObjects[i].CompareTag("LibraryGhost")) ghostType = GhostType.LibraryGhost;
                        else if (ghostObjects[i].CompareTag("OneCorridorGhost")) ghostType = GhostType.OneCorridorGhost;
                        return true;
                    }
                }
            }
        }

        return false;
    }
}