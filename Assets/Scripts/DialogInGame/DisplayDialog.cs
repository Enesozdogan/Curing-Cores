using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDialog : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI txt_content;
    [SerializeField]
    TextMeshProUGUI txt_speakerName;
    [SerializeField]
    PlayerInputController playerInputController;
    [SerializeField]
    Transform choiceBaseTransform;
    [SerializeField]
    GameObject choiceBtnPrefab;
    [SerializeField]
    GameObject NPC_Dialog;
    [SerializeField]
    AudioClip dialogTxtSFX,skipSFX;
    [SerializeField]
    AudioSource audioSource;
    bool nextSound=false;
    DialogConnector connector;

    private void Awake()
    {
        connector = FindObjectOfType<DialogConnector>();
       
        playerInputController.OnDialogAction += GetNextText;
        playerInputController.OnDialogAction += PlayNextSound;
        connector.OnUpdateRequired += CreateUI;
    }
    private void Start()
    {
        CreateUI();
        
    }
    public void GetNextText()
    {
        if (connector.CanProgress())
        {
         
            StopAllCoroutines();
            connector.ProgressDialog();
            
        }
        else
        {
          
            gameObject.SetActive(false);
        }
       
    }
    public void PlayNextSound()
    {
        if (nextSound && connector.CanProgress())
        {
            audioSource.PlayOneShot(skipSFX);
            
        }
        nextSound = false;
    }
   

    private void CreateUI()
    {
        gameObject.SetActive(connector.ShowDialog());
        if (connector.ShowDialog() == false)
        {
            return;
        }
        choiceBaseTransform.gameObject.SetActive(connector.IsPlayerTalking());
        NPC_Dialog.SetActive(!connector.IsPlayerTalking());
        if (connector.IsPlayerTalking())
        {
            
            DestroyButtons();
            CreateButtons();
           
            
        }
        else
        {
            
            txt_speakerName.text = connector.speakerName;
            StartCoroutine(AnimateText(connector.GetDialogText()));
            
        }
        
    }
    private void DestroyButtons()
    {
        foreach(Transform choice in choiceBaseTransform)
        {
            Destroy(choice.gameObject);
        }
    }
    private void CreateButtons()
    {
        foreach(DialogeDataNode node in connector.FetchChoiceList())
        {
            GameObject btn = Instantiate(choiceBtnPrefab);
            btn.transform.SetParent(choiceBaseTransform);
            btn.GetComponentInChildren<TextMeshProUGUI>().text= node.GetText();
            Button  selectedBtn=btn.GetComponentInChildren<Button>();
            selectedBtn.onClick.AddListener(() =>
            {
                connector.ChooseDialog(node);
                
            });
        }
    }
    IEnumerator AnimateText(string text)
    {
        txt_content.text = "";
        nextSound = true;
        
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(AudioTimer(text.Length));
        for (int i=0; i<text.Length; i++)
        {
            
            txt_content.text += text[i];

          
            yield return new WaitForSeconds(0.02f);
        }
        
    }
    IEnumerator AudioTimer(int len)
    {
        int i = 0;
        while(i<len/4)
        {
            audioSource.PlayOneShot(dialogTxtSFX);
            yield return new WaitForSeconds(0.1f);
            i++;
        }
    }
  
}
