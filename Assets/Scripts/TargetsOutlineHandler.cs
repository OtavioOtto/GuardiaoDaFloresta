using UnityEngine;

public class TargetsOutlineHandler : MonoBehaviour
{
    [SerializeField] private GameObject avisoTxt;
    private GameObject[] targets;
    private Outline outline;

    private TargetHit hit;
    private TargetsPuzzleHandler handler;
    void Start()
    {
        handler = gameObject.GetComponent<TargetsPuzzleHandler>();

        int children = transform.childCount;
        targets = new GameObject[children];
        for (int i = 0; i < targets.Length; i++) 
        {
            targets[i] = transform.GetChild(i).gameObject;
            if (targets[i] != null)
            {
                outline = targets[i].GetComponent<Outline>();
                outline.enabled = false;
            }
        }
        
    }

    private void Update()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            outline = targets[i].GetComponent<Outline>();
            TargetHit hitVerifier = targets[i].GetComponentInChildren<TargetHit>();
            if (outline.enabled && hitVerifier.hit)
                outline.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !handler.puzzleComplete) 
        {
            for (int i = 0; i < targets.Length; i++)
            {
                Debug.Log("a");
                outline = targets[i].GetComponent<Outline>();
                outline.enabled = true;
            }
            avisoTxt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < targets.Length; i++)
            {
                outline = targets[i].GetComponent<Outline>();
                outline.enabled = false;
            }
            avisoTxt.SetActive(false);
        }
    }
}
