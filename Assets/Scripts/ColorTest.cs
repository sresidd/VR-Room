using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTest : MonoBehaviour
{
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(color1);
        Debug.Log(color2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
