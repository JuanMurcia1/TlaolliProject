using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start()
        {
            SceneManager.LoadScene("MainGame");
        }

    public void GoFarm()
    {
        SceneManager.LoadScene("Farm");
    }

    public void LoadSceneWithDelay()
    {
        Invoke("GoFarm", 1.5f); 
    }
    
}
