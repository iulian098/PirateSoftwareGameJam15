using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<int> a = new List<int>();
        a.Add(1);
        a.Add(2);
        a.Add(3);
        a.Add(4);
        a.Add(5);

        List<int> b = new List<int>();
        b = a;
        b[0] = 10;
        Debug.Log(a[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
