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
        var b = new bool[512, 512];
        if (eachHole.Equals("Hole1"))
        {
            for (var x = 0; x < 512; x++)
                for (var y = 0; y < 512; y++)
                    b[x, y] = !(x > 245 && x < 251 && y > 394 && y < 398);
            terrain.terrainData.SetHoles(0,0,b);

            firstHole.SetActive(false);
            firstHoleLimits.SetActive(false);
            firstStone.SetActive(false);

            //colocar particula de terra

            secondHole.SetActive(true);
            secondHoleLimits.SetActive(true);
            secondStone.SetActive(true);
        }
        else if (eachHole.Equals("Hole2"))
        {
            for (var x = 0; x < 512; x++)
                for (var y = 0; y < 512; y++)
                    b[x, y] = !(x > 224 && x < 229 && y > 387 && y < 393);
            terrain.terrainData.SetHoles(0, 0, b);

            secondHole.SetActive(false);
            secondHoleLimits.SetActive(false);
            secondStone.SetActive(false);

            //colocar particula de terra

            thirdHole.SetActive(true);
            thirdHoleLimits.SetActive(true);
            thirdStone.SetActive(true);
        }
        else
        {
            thirdHole.SetActive(false);
            thirdHoleLimits.SetActive(false);
            thirdStone.SetActive(false);

            //colocar particula de terra

            for (var x = 0; x < 512; x++)
                for (var y = 0; y < 512; y++)
                    b[x, y] = !(x > 512 && x < 0 && y > 512 && y < 0);
            terrain.terrainData.SetHoles(0, 0, b);

            cageTransf.position = new Vector3(cageT.position.x, cageT.position.y + 0.8f, cageT.position.z);
            Destroy(cage);
            GameObject deerGO = Instantiate(deer, cageTransf.position, cageTransf.rotation);
            deerGO.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        }
        
    }
}
