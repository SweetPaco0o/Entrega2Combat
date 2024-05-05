using UnityEngine;

public class InstanciarPatron : MonoBehaviour
{
    public GameObject cubePrefab;
    public LayerMask patronLayer; 

    void OnTriggerEnter(Collider other)
    {
        
        if (((1 << other.gameObject.layer) & patronLayer) != 0)
        {
            ShowCube();
        }
    }

    void ShowCube()
    {
        GameObject cubeInstance = Instantiate(cubePrefab, transform.position, Quaternion.identity);
        Destroy(cubeInstance, 5f);
    }
}
