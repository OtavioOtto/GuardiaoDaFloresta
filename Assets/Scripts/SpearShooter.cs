using UnityEngine;

public class SpearShooter : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private Transform cam;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject spear;

    [Header("Values")]
    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;

    [Header("Settings")]
    [SerializeField] private float throwCooldown;
    [SerializeField] private bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && readyToThrow) 
            ShootingMethod();
    }

    void ShootingMethod() 
    {
        readyToThrow = false;
        GameObject newSpear = Instantiate(spear, attackPoint.position, Quaternion.Euler(cam.eulerAngles.x + 90, cam.eulerAngles.y,cam.eulerAngles.z));
        Rigidbody rb = newSpear.GetComponent<Rigidbody>();
        Vector3 forceDirection = cam.forward;
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 10f))
        {}
        
        else if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
            forceDirection = (hit.point - attackPoint.position).normalized;

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        rb.AddForce(forceToAdd, ForceMode.Impulse);
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
