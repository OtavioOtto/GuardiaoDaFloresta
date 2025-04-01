using UnityEngine;
using UnityEngine.UI;

public class DeerReleaser : MonoBehaviour
{
    [SerializeField] private GameObject interactTxt;
    [SerializeField] private GameObject buttons;
    [SerializeField] private bool playerInside = false;
    public GameObject triangle;
    public GameObject square;
    public GameObject circle;
    public Button triangleButton;
    public Button squareButton;
    public Button circleButton;
    public GameObject cage;
    public Transform cageT;
    private Transform cageTransf;
    public GameObject deer;
    private bool isFree = false;
    private void Start()
    {
        cageTransf = cageT;
    }
    private void Update()
    {
        if (interactTxt.activeSelf && Input.GetKey(KeyCode.E))
        {
            interactTxt.SetActive(false);
            buttons.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }

        CodeVerifier();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !buttons.activeSelf)
        {
            interactTxt.SetActive(true);
            playerInside = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !buttons.activeSelf)
        {
            interactTxt.SetActive(true);
            playerInside = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactTxt.SetActive(false);
            playerInside = false;
        }
    }

    private void CodeVerifier() 
    {
        if (playerInside)
        {
            if (triangle.activeSelf)
            {
                if (square.activeSelf)
                {
                    if (circle.activeSelf && !isFree)
                    {
                        cageTransf.position = new Vector3(cageT.position.x, cageT.position.y + 0.8f, cageT.position.z);
                        Destroy(cage);
                        this.gameObject.GetComponent<BoxCollider>().enabled = false;
                        GameObject deerGO = Instantiate(deer, cageTransf.position, cageTransf.rotation);
                        deerGO.transform.localScale = new Vector3(1.8f,1.8f,1.8f);
                        isFree = true;
                    }
                }
            }
            if ((square.activeSelf && !triangle.activeSelf) || (circle.activeSelf && !triangle.activeSelf))
            {
                triangleButton.interactable = true;
                squareButton.interactable = true;
                circleButton.interactable = true;
                square.SetActive(false);
                triangle.SetActive(false);
                circle.SetActive(false);
            }
            if (circle.activeSelf && !square.activeSelf)
            {
                triangleButton.interactable = true;
                squareButton.interactable = true;
                circleButton.interactable = true;
                square.SetActive(false);
                triangle.SetActive(false);
                circle.SetActive(false);
            }
        }
    }
}
