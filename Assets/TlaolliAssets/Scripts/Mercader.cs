using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mercader : MonoBehaviour
{
    public GameObject panelMercader;
    public GameObject panelVenta;
    public GameController gameController;
    public bool venderFirstTime= false;
    public DialogosFarm dialogosFarm;

    private FogataController fogataController;
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
        fogataController= FindObjectOfType<FogataController>();
        if(!venderFirstTime){
        GameController.granitosDeOro=100;
        gameController.granitoDeOROT.text="" +GameController.granitosDeOro.ToString();
        venderFirstTime= true;
        dialogosFarm.contador= 17;
        dialogosFarm.SecuenciaDialogos();
        gameController.cornAsado -= gameController.cornAsado;
        gameController.UpdateCornGrilledText();
        
        }else
        {
            gameController.cornAsado -= gameController.cornAsado;
            gameController.UpdateCornGrilledText();
        }


        

    }



}
