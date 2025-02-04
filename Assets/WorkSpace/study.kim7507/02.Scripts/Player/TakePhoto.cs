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

        // "LibraryGhost"�� "OneCorriDorGhost" �±װ� ���� ��� ������Ʈ�� ã�� ����
        GameObject[] libraryGhosts = GameObject.FindGameObjectsWithTag("LibraryGhost");
        GameObject[] oneCorriDorGhosts = GameObject.FindGameObjectsWithTag("OneCorridorGhost");

        // �� �迭�� ���ļ� ghostObjects�� ����
        ghostObjects = new GameObject[libraryGhosts.Length + oneCorriDorGhosts.Length];
        libraryGhosts.CopyTo(ghostObjects, 0);
        oneCorriDorGhosts.CopyTo(ghostObjects, libraryGhosts.Length);
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
        
        bool isInGhost = CheckForGhostInPhoto();

        ownerPlayer.playerUI.PlayerTakePhoto();                      // ������� ȿ�� (��½��)
        takePhotoAudioSource.Play();

        GameObject go = Instantiate(photoPrefab);
        go.transform.Find("Front").Find("Image").gameObject.GetComponent<MeshRenderer>().material.mainTexture = photo;

        // ���� ������ �κ��丮�� �ֱ�
        if (go.TryGetComponent<Pickable>(out Pickable pickable))
        {
            pickable.itemName = "����";
            if (isInGhost) pickable.itemDescription = ownerPlayer.playerUI.timer.text + "�� ���� �����̴�. \n���Ϸ��� �ͽ��� �����ִ�. \n�𸶿� ����� �� ���� �� ����.";
            else pickable.itemDescription = ownerPlayer.playerUI.timer.text + "�� ���� �����̴�. \n���Ϸ��� �ͽ��� ���������� ���� �� ����.";
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
            
            // ������Ʈ�� ī�޶��� ����Ʈ ���� �ִ��� Ȯ��
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                // ī�޶�� �ͽ� ������Ʈ ���� ���� ����
                Vector3 directionToGhost = ghostObjects[i].transform.position - Camera.main.transform.position;

                // Raycast�� ����Ͽ� ���������� Ȯ��
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