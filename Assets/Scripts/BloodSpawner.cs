using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpawner : MonoBehaviour
{
    public GameObject Prefab;

    public int nCubes = 10;

    public float fillProbability = 0.9f;

    public Gradient CubeGradient;

    public float ColorSpeed = 1f;

    public Transform Parent;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnInPlane();
        }
    }

    void SpawnInPlane()
    {
        for (int i = 0; i < nCubes; i++)
        {
            for (int j = 0; j < nCubes; j++)
            {
                Vector3 pos = transform.position;
                pos.x = Random.Range(-1, 1);
                pos.z = Random.Range(-1, 1);

                Quaternion rot = Random.rotation;
                rot = Quaternion.Euler(0, Random.Range(0, 360), 0);

                SpawnWithRot(pos, rot);
            }

        }
    }

    void SpawnOne(Vector3 pos)
    {
        Instantiate(Prefab, pos, transform.rotation);
    }

    void SpawnWithRot(Vector3 pos, Quaternion rot)
    {
        GameObject go = Instantiate(Prefab, pos, rot);
        go.transform.localScale *= Random.Range(0.1f, 1f);


        Color color = new Color(Random.value, Random.value, Random.value);

        //float rdm = Random.value;
        float rdm = Mathf.Sin(Time.time * ColorSpeed) / 2 + 0.5f;
        //color = new Color(rdm, rdm, rdm);
        color = CubeGradient.Evaluate(rdm);
        go.GetComponent<MeshRenderer>().material.color = color;

        go.transform.SetParent(Parent);
    }
}
