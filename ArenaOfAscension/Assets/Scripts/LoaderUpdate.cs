using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonUp(1)|| Input.GetMouseButtonUp(0))
        {
            Loader.LoadTargetScene();
        }
        
    }
}
