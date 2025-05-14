using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsMain : MonoBehaviour
{
    public Animation zoneGreen;
    public bool isInventoryOpen = false;
    public Animation uiInventory;
    public Animation imageUI;
    public Animation textConversation;
    public Animation panelConversation;
    public DialogosFarm dialogosFarm;
    public Animation UiTextCorn;
    public Animation UiTextWood;

    public Animation sumChest1;

   // public Animation wareHousePanel;
    public bool paso2= true;
    public BoxCollider2D boxPlanting;
    // Start is called before the first frame update
    void Start()
    {
        zoneGreen.GetComponent<Animation>();
        uiInventory.GetComponent<Animation>();
        imageUI.GetComponent<Animation>();
        textConversation.GetComponent<Animation>();
        panelConversation.GetComponent<Animation>();
       // wareHousePanel.GetComponent<Animation>();

        
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void openInventario()
    {
        // Si el inventario está cerrado (false), lo abrimos
        if (!isInventoryOpen)
        {
            uiInventory.Play("InventarioOpen"); // Llama la animación para abrir el inventario
            isInventoryOpen = true; // Cambiamos el estado a "abierto"
            boxPlanting.enabled = true;
            imageUI.Play("SeedTransparencyOn");
            zoneGreen.Play("GreenTransparentZone");
            if(paso2==true){
                dialogosFarm.contador=1;
            dialogosFarm.SecuenciaDialogos();

            }
            
            
        }
        else
        {
            uiInventory.Play("InventarioClose"); // Llama la animación para cerrar el inventario
            isInventoryOpen = false; // Cambiamos el estado a "cerrado"
            imageUI.Play("SeedTransparencyOff");
            paso2=false;
            boxPlanting.enabled = false;
            zoneGreen.Play("GreenColorZone");

            
        }
    }

    public void panelDialogos()
    {
        textConversation.Play("TextTransparencyOff");
        panelConversation.Play("PanelTransparencyOff");
    }


    public IEnumerator fadedUpPanel()
    {
        yield return new WaitForSeconds(8);
        panelConversation.Play("PanelTransparencyOn");
        textConversation.Play("TextTransparencyOn");


    }

     public void fadedUpPanelinstant()
    {
        
        panelConversation.Play("PanelTransparencyOn");
        textConversation.Play("TextTransparencyOn");


    }

    public IEnumerator MoveOffUiTexts()
    {
        yield return new WaitForSeconds(3);
        UiTextCorn.Play("TexFadedCorn");
        UiTextWood.Play("TextFadedWood");
    }

    public void sumPlay()
    {
        sumChest1.Play("+1_Sum");
    }

}
