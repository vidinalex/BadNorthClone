using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] private Transform camFollow;

    private PartySelection selectedSquad;
    private Camera m_Camera;

    private LayerMask groundLayer;
    private LayerMask allyLayer;

    private float cameraRotationSpeed;
    private string xAxisName;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        groundLayer = GameData.Default.groundLayer;
        allyLayer = GameData.Default.allyLayer;
        cameraRotationSpeed = GameData.Default.cameraRotationSpeed;
        xAxisName = GameData.Default.xAxisName;
        m_Camera = LevelManager.Default.mainCamera;
    }

    private void Update()
    {
        UnitControl();
        CameraControl();
    }

    private void UnitControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray camRay = m_Camera.ScreenPointToRay(Input.mousePosition);

            if (selectedSquad)
            {
                if (Physics.Raycast(camRay, out hit, 1000f, groundLayer))
                {
                    selectedSquad.SetDestination(hit.transform);

                }
                selectedSquad.SetSelectSprite(false);
                m_Camera.cullingMask -= groundLayer;
                selectedSquad = null;
            }
            else
            {
                if (Physics.Raycast(camRay, out hit, 1000f, allyLayer))
                {
                    selectedSquad = hit.transform.GetComponent<PartySelection>();
                    if (selectedSquad)
                    {
                        m_Camera.cullingMask += groundLayer;
                        selectedSquad.SetSelectSprite(true);
                    }
                }
            }
        }
    }

    private void CameraControl()
    {
        float mouseX = Input.GetAxis(xAxisName) * cameraRotationSpeed;

        if (Input.GetMouseButton(1))
        {
            camFollow.RotateAround(camFollow.position, transform.up, mouseX);
        }
    }
}
