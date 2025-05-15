using UnityEngine;
using UnityEngine.UI;

public class DeerReleaser : MonoBehaviour
{
    public GameObject cage;
    public Transform cageT;
    private Transform cageTransf;
    public GameObject deer;
    [SerializeField] private Terrain terrain;
    [SerializeField] private string eachHole;
    [Header("First Hole")]
    [SerializeField] private GameObject firstHoleLimits;
    [SerializeField] private GameObject firstHole;
    [SerializeField] private GameObject firstStone;
    [Header("Second Hole")]
    [SerializeField] private GameObject secondHole;
    [SerializeField] private GameObject secondHoleLimits;
    [SerializeField] private GameObject secondStone;
    [Header("Third Hole")]
    [SerializeField] private GameObject thirdHole;
    [SerializeField] private GameObject thirdHoleLimits;
    [SerializeField] private GameObject thirdStone;
    
    private void Start()
    {
        cageTransf = cageT;
        eachHole = gameObject.transform.name;


        var b = new bool[512, 512];
        for (var x = 0; x < 512; x++)
            for (var y = 0; y < 512; y++)
                b[x, y] = !(x > 251 && x < 255 && y > 367 && y < 371);
        terrain.terrainData.SetHoles(0, 0, b);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stone"))
        {
            CodeVerifier();
        }
    }

    private void CodeVerifier() 
    {

        if (eachHole.Equals("Hole1"))
        {

            firstHoleLimits.SetActive(false);
            firstStone.GetComponent<BoxCollider>().enabled = false;

            //colocar particula de terra

            secondHoleLimits.SetActive(true);
            secondStone.SetActive(true);
        }
        else if (eachHole.Equals("Hole2"))
        {

            secondHoleLimits.SetActive(false);
            secondStone.GetComponent<BoxCollider>().enabled = false;

            //colocar particula de terra

            thirdHoleLimits.SetActive(true);
            thirdStone.SetActive(true);
        }
        else
        {
            thirdHoleLimits.SetActive(false);
            thirdStone.GetComponent<BoxCollider>().enabled = false;

            //colocar particula de terra

            cageTransf.position = new Vector3(cageT.position.x, cageT.position.y + 0.8f, cageT.position.z);
            Destroy(cage);
            GameObject deerGO = Instantiate(deer, cageTransf.position, cageTransf.rotation);
            deerGO.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        }
        
    }
}
