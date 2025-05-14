using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raven : MonoBehaviour
{

    private float speed= 2f;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        flightRaven();
        
        
    }

    public void flightRaven()
    {
        transform.Translate(Vector2.right * speed* Time.deltaTime);

       if(transform.position.x> 3.89f)
       {
        Destroy(gameObject);
       }
    }
}
