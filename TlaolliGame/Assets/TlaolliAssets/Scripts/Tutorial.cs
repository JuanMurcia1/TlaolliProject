using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Animator animator;
    public Image panel;

    private Animation panelConversation;
    private Animation buttonAvanza;
    private Animation textDialogo;
    // Start is called before the first frame update
    void Start()
    {
        animator.GetComponent<Animator>();
        ChangeColor("#8A8A8A");

        GameObject panelConversationObject = GameObject.Find("ConversationPanel");
        GameObject ButtonAvanzaObject = GameObject.Find("NextButton");
        GameObject textDialogoObject = GameObject.Find("TextDialogo");
        

        panelConversation = panelConversationObject.GetComponent<Animation>();
        buttonAvanza = ButtonAvanzaObject.GetComponent<Animation>();
        textDialogo= textDialogoObject.GetComponent<Animation>();
        PlayMoveRight();
        PlayMoveRightButton();
        PlayMoveRightTextDialogo();


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FrankLeft()
    {
        animator.SetBool("NextPass", true);
    }

   

    public void ChangeColor(string hexColor)
    {
        Color newColor;
        if (ColorUtility.TryParseHtmlString(hexColor, out newColor))
        {
            panel.color = newColor;
        }
        
    }

    public void PlayMoveLeftPanel()
    {
        
        panelConversation.Play("MoveLeftPanel");
        
    }
    public void PlayMoveRight()
    {
        panelConversation.Play("PanelDark");
    }

    public void PlayMoveRightButton()
    {
        buttonAvanza.Play("ButtonNext Movement");
    }

    public void PlayMoveLeftButton()
    {
        buttonAvanza.Play("MoveLeftButton");
    }

    public void PlayMoveLeftTextDialogo()
    {
        textDialogo.Play("MoveLeftPanel");
    }
     public void PlayMoveRightTextDialogo()
    {
        textDialogo.Play("PanelDark");
    }
}
