using UnityEngine;
using System.Collections;

public class StoneGravityActivater : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject stoneOne;
    [SerializeField] private GameObject stoneTwo;
    [SerializeField] private GameObject stoneThree;
    [Header("Rigidbodys")]
    [SerializeField] private Rigidbody stoneOneRB;
    [SerializeField] private Rigidbody stoneTwoRB;
    [SerializeField] private Rigidbody stoneThreeRB;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stone"))
        {
            StartCoroutine(DisableConstraints());
        }
       
    }

    IEnumerator DisableConstraints()
    {
        yield return new WaitForSeconds(.5f);
        if(stoneOne.activeSelf)
            stoneOneRB.constraints = RigidbodyConstraints.None;
        if (stoneTwo.activeSelf)
            stoneTwoRB.constraints = RigidbodyConstraints.None;
        if (stoneThree.activeSelf)
            stoneThreeRB.constraints = RigidbodyConstraints.None;
    }

}
