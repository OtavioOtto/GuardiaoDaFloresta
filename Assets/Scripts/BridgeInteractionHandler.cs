using System.Collections;
using UnityEngine;

public class BridgeInteractionHandler : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject questionTxt;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject stats;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject animator;
    [SerializeField] private Animator anim;
    [Header("Cam Move Values")]
    [SerializeField] private Vector3 camStartingPos;
    [SerializeField] private Quaternion camStartingRot;
    [SerializeField] private Transform camTarget;
    [SerializeField] private float transitionDuration = 1f;
    [Header("Verifiers")]
    [SerializeField] private bool playerInside;
    [SerializeField] private bool isItTransitioning;

    private Coroutine cameraMovementCoroutine;

    private void Start()
    {
        isItTransitioning = false;
    }
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
        if (playerInside && Input.GetKeyDown(KeyCode.E) && !isItTransitioning)
        {
            if (questionTxt.activeSelf)
            {
                Camera.main.transform.parent = null;
                questionTxt.SetActive(false);
                crosshair.SetActive(false);
                stats.SetActive(false);
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<SpearShooter>().enabled = false;
                player.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, 0);
                anim.SetBool("andando", false);
                anim.SetBool("lancou", false);
                animator.GetComponent<CurupiraAnimations>().enabled = false;
                
                camStartingPos = Camera.main.transform.localPosition;
                camStartingRot = Camera.main.transform.localRotation;

                if (cameraMovementCoroutine != null)
                    StopCoroutine(cameraMovementCoroutine);
                isItTransitioning=true;
                cameraMovementCoroutine = StartCoroutine(MoveCamera(camTarget.position, camTarget.rotation, false));
            }
            else
            {
                Camera.main.transform.parent = player.transform;
                questionTxt.SetActive(true);
                crosshair.SetActive(true);
                stats.SetActive(true);

                if (cameraMovementCoroutine != null)
                    StopCoroutine(cameraMovementCoroutine);
                isItTransitioning = true;
                cameraMovementCoroutine = StartCoroutine(MoveCamera(camStartingPos, camStartingRot, true));
                
            }
        }
    }

    IEnumerator MoveCamera(Vector3 targetPosition, Quaternion targetRotation, bool isItReturning)
    {

        Transform cameraTransform = Camera.main.transform;
        Vector3 startPos = cameraTransform.position;
        Quaternion startRot = cameraTransform.rotation;
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionDuration;
            cameraTransform.position = Vector3.Lerp(startPos, targetPosition, t);
            cameraTransform.rotation = Quaternion.Slerp(startRot, targetRotation, t);
            yield return null;
        }   

        cameraTransform.position = targetPosition;
        cameraTransform.rotation = targetRotation;
        isItTransitioning = false;        
        if (isItReturning)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<SpearShooter>().enabled = true;
            animator.GetComponent<CurupiraAnimations>().enabled = true;

        }
    }
}