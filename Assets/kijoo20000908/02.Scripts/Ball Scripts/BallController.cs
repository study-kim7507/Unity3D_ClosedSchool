using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    public Camera playerCamera;
    public float throwForce = 18f; // ?˜ì§ˆ ??
    public float spinForce = 5f; // ?Œì „ ??
    private bool isHolding = false; // ê³µì„ ?¤ê³  ?ˆëŠ”ì§€ ?•ì¸
    private static BallController selectedBall = null; // ?„ì¬ ? íƒ??ê³?(?˜ë‚˜ë§??œì„±??

    private bool isAlreadySelected = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void Update()
    {
        // ë§ˆìš°?¤ë¡œ ?¹ì • ê³?? íƒ
        if (Input.GetMouseButtonDown(0)) // ?¼ìª½ ?´ë¦­?¼ë¡œ ê³?? íƒ
        {
            SelectBall();
        }

        if (selectedBall == this) // ?„ì¬ ? íƒ??ê³µë§Œ ?˜ì§ˆ ???ˆìŒ
        {
            if (!isAlreadySelected)
            {
                PlayerUI.instance.DisplayInteractionDescription("EÅ°¸¦ ´­·¯ °ñ´ë¸¦ ÇâÇØ ½÷¼­ °ñÀ» ³Ö¾îº¸ÀÚ");
                isAlreadySelected = true;
            }
            if (Input.GetKeyDown(KeyCode.E)) // E ?¤ë¡œ ?˜ì?ê¸?
            {
                ThrowBall();
                selectedBall = null; // ? íƒ ?´ì œ
            }
        }
    }

    private void SelectBall()
    {
        // ë§ˆìš°?¤ë¡œ ?´ë¦­??ê³µë§Œ ? íƒ (Raycast ?¬ìš©)
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject) // ?´ë¦­??ê³µë§Œ ? íƒ
            {
                selectedBall = this;
            }
        }
    }

    private void ThrowBall()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        Destroy(GetComponent<Draggable>());

        Vector3 throwDirection = (playerCamera.transform.forward + Vector3.up * 0.1f).normalized;

        transform.position += throwDirection * 0.3f;
        rb.linearVelocity = throwDirection * throwForce;

        rb.angularVelocity = Random.insideUnitSphere * spinForce;

        StartCoroutine(ReattachDraggableAfterDelay(0.5f));
    }

    private IEnumerator ReattachDraggableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Draggable ì»´í¬?ŒíŠ¸ë¥??¤ì‹œ ì¶”ê?
        gameObject.AddComponent<Draggable>();
    }
}
