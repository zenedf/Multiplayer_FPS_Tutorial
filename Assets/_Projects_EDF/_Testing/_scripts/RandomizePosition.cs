using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePosition : MonoBehaviour
{
    int time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    void FixedUpdate()
    {
        time++;

        if (time % 100 == 0)
        {
            ChangePlaces();
        }

    }

    void ChangePlaces()
    {
        transform.position += new Vector3(
            Random.Range(-2f, 2f),
            Random.Range(-2f, 2f), 0f);
    }

}
