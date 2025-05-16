using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BridgeInteractionHandler : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject questionTxt;
    [SerializeField] private GameObject player;
    [Header("Cam Move Values")]
    [SerializeField] private Vector3 camStartingPos;
    [SerializeField] private Quaternion camStartingRot;
    [SerializeField] private Transform camTarget;
    [SerializeField] private float speed = 5;
    [Header("Verifiers")]
    [SerializeField] private bool playerInside;
    [SerializeField] private bool camIsMoving;

    private void Update()
    {
        InteractionHandler();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            questionTxt.SetActive(true);
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questionTxt.SetActive(false);
            playerInside = false;
        }
    }

    void InteractionHandler() 
    {
        if(playerInside && Input.GetKeyDown(KeyCode.E) && questionTxt.activeSelf) 
        {
            questionTxt.SetActive(false);
            player.GetComponent<PlayerMovement>().enabled = false;
            camStartingPos = Camera.main.transform.localPosition;
            camStartingRot = Camera.main.transform.localRotation;
            StartCoroutine(MoveAndRoateCamera());
            camIsMoving = true;
            
        }

        else if (playerInside && Input.GetKeyDown(KeyCode.E) && !questionTxt.activeSelf)
        {
            questionTxt.SetActive(true);
            player.GetComponent<PlayerMovement>().enabled = true;
            StartCoroutine(MoveAndRoateCamera());
            camIsMoving = true;
        }
    }

    IEnumerator MoveAndRoateCamera() 
    {
        //calcular a rotacao da camera igual ja tem a posicao
        if (!questionTxt.activeSelf) 
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, camTarget.position, speed * Time.deltaTime);
            yield return new WaitUntil(() => isItAtThePostion(camTarget.position) && isItAtTheRotation(camTarget.rotation));
        }

        else 
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, camStartingPos, speed * Time.deltaTime);
            yield return new WaitUntil(() => isItAtThePostion(camStartingPos) && isItAtTheRotation(camStartingRot));

        }

        
    }

    bool isItAtThePostion(Vector3 pos) 
    {
        return Vector3.Distance(Camera.main.transform.position, pos) < 0.01f;
    }

    bool isItAtTheRotation(Quaternion rot)
    {
        return Quaternion.Angle(Camera.main.transform.rotation, rot) < 0.01f;
    }
}
