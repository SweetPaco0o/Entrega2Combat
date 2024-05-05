using UnityEngine;

public class InstanciarPatron : MonoBehaviour
{
    public GameObject cubePrefab; 

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E))
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
