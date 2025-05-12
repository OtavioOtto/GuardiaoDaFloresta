using UnityEngine;

public class SpearShooter : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private Transform cam;
    [SerializeField] private Transform curvePoint;
    [SerializeField] private GameObject spear;
    [SerializeField] private GameObject trail;
    [SerializeField] private Rigidbody spearRB;
    [SerializeField] private CapsuleCollider spearCol;
    [SerializeField] private Transform rightHand;

    [Header("Values")]
    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;
    [SerializeField] private Vector3 old_pos;

    [Header("Settings")]
    public bool isReturning;
    public bool playerHasSpear;
    [SerializeField] private float time;

    public SpearAddOns addOns;

    private void Start()
    {
        spearRB = spear.GetComponent<Rigidbody>();
        trail.GetComponent<TrailRenderer>().enabled = false;
        playerHasSpear = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && !playerHasSpear && Time.timeScale != 0)
            ReturningMethod();
        if (isReturning) {
            if (time < 1.0f)
            {
                spear.transform.position = ReturnCalculus(time, old_pos, curvePoint.position, rightHand.position);
                time += Time.deltaTime;
            }
            else
                ResetSpear();
        }

        if (playerHasSpear)
            spearCol.enabled = false;
            
    }

    public void ShootingMethod() 
    {
        playerHasSpear = false;
        spearCol.enabled = true;
        isReturning = false;
        trail.GetComponent<TrailRenderer>().enabled = true;
        spear.transform.parent = null;  
        spearRB.isKinematic = false;
        spearRB.AddForce(Camera.main.transform.TransformDirection(Vector3.forward)*throwForce, ForceMode.Impulse);
    }

    void ReturningMethod() 
    {
        spearRB.isKinematic = false;
        spearCol.enabled = false;
        trail.GetComponent<TrailRenderer>().enabled = false;
        time = 0.0f;
        old_pos = spear.transform.position;
        isReturning = true;
        spearRB.linearVelocity = Vector3.zero;
        
    }
    void ResetSpear()
    {
        isReturning = false;
        spear.transform.parent = rightHand;
        spear.transform.localPosition = new Vector3(0,0,0);
        spear.transform.rotation = rightHand.rotation;
        spearRB.isKinematic = true;
        spearCol.enabled = true;
        trail.GetComponent<TrailRenderer>().enabled = false;
        playerHasSpear = true;
        addOns.isHit = false;
    }
    Vector3 ReturnCalculus(float t, Vector3 p0, Vector3 p1, Vector3 p2) {

        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + tt * p2;
        return p;


    }
}
