using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mercader : MonoBehaviour
{
    public GameObject panelMercader;
    public GameObject panelVenta;
    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        if(gameObject.CompareTag("Mercader"))
        {
             panelMercader.SetActive(true);

        }
    }

    public void OpenPanelVenta()
    {
        panelVenta.SetActive(true);
    }

    public void closePanelMercaders()
    {
        panelMercader.SetActive(false);
    }

    public void closePanelVenta()
    {
        panelVenta.SetActive(false);
    }

    public void vender()
    {
        GameController.granitosDeOro=100;
        gameController.granitoDeOROT.text="" +GameController.granitosDeOro.ToString();

    }



}
