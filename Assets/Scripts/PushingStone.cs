using UnityEngine;

public class PushingStone : MonoBehaviour
{ 
    [SerializeField] private bool playerInside;
    [SerializeField] private string direction;
    private Rigidbody stoneRB;
    void Start()
    {
        playerInside = false;
        direction = gameObject.name;
        stoneRB = transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && playerInside) 
        {
            switch (direction) {
                case "ColliderRight":
                    stoneRB.AddForce(new Vector3(-.5f,0,0), ForceMode.VelocityChange);
                    break;

                case "ColliderLeft":
                    stoneRB.AddForce(new Vector3(.5f, 0, 0), ForceMode.VelocityChange);
                    break;

                case "ColliderFront":
                    stoneRB.AddForce(new Vector3(0, 0, -.5f), ForceMode.VelocityChange);
                    break;

                case "ColliderBack":
                    stoneRB.AddForce(new Vector3(0, 0, .5f), ForceMode.VelocityChange);
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }
}
