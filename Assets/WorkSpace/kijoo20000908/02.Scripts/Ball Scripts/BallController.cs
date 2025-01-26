using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb; // �󱸰��� Rigidbody
    private static BallController selectedBall; // ���� ���õ� ���� �����ϴ� static ����
    private bool isCharging = false; // ���� ���� Ȯ��
    private float chargePower = 0f; // ������ ��
    private float maxPower = 15f; // �ִ� ��
    private float chargeRate = 10f; // �� ���� �ӵ�
    private bool isReleased = false; // ���� ���������� Ȯ��

    public Camera playerCamera; // �÷��̾ ����ϴ� ī�޶�

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // ī�޶� �������� ���� ��� �⺻������ ���� ī�޶� ���
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void Update()
    {
        // ������ ���콺 ��ư���� �� ����
        if (Input.GetMouseButtonDown(1))
        {
            SelectBall();
        }

        // ���õ� ���� ���� �� ������ ����
        if (selectedBall == this)
        {
            if (Input.GetMouseButton(1) && isCharging)
            {
                ChargePower();
            }

            if (Input.GetMouseButtonUp(1) && isCharging && !isReleased)
            {
                ReleaseBall();
            }
        }
    }

    private void SelectBall()
    {
        // ���콺 Ŭ���� ��ġ���� Raycast�� ���� ���� ����
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject) // Ŭ���� ������Ʈ�� ���� ������ Ȯ��
            {
                selectedBall = this; // �� ���� ����
                isCharging = true;   // ���� �������Ƿ� ���� ����
                isReleased = false; // ���� ���� �������� ���� ����
                Debug.Log($"{gameObject.name} ���� ���õǾ����ϴ�.");
            }
        }
    }

    private void ChargePower()
    {
        chargePower += chargeRate * Time.deltaTime;
        chargePower = Mathf.Clamp(chargePower, 0f, maxPower);
        Debug.Log($"�� ���� ��: {chargePower}");
    }

    private void ReleaseBall()
    {
        if (chargePower <= 0f)
        {
            Debug.LogWarning("������ ���� �����Ͽ� ���� �������� �ʽ��ϴ�.");
            return;
        }

        isCharging = false;
        isReleased = true;

        // ī�޶� �ٶ󺸴� ������ ���
        Vector3 cameraForward = playerCamera.transform.forward; // ī�޶��� ���� ����
        Vector3 launchDirection = (cameraForward + Vector3.up * 0.1f).normalized; // �ణ ���� ������

        // �� ����
        rb.AddForce(launchDirection * chargePower, ForceMode.Impulse);

        Debug.Log($"{gameObject.name} ���� ����: �� = {chargePower}, ���� = {launchDirection}");
        chargePower = 0f;

        // ���� ���� �ʱ�ȭ
        selectedBall = null;
    }
}
