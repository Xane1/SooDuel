using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class StagePreviews : MonoBehaviour
{
    Image image;

    public Sprite present;
    public Sprite medieval;
    public Sprite ancient;


    public GameObject presentButton;
    public GameObject medievalButton;
    public GameObject ancientButton;
    
    public GameObject presentInfo;
    public GameObject medievalInfo;
    public GameObject ancientInfo;
    void Start()
    {
        image = GetComponent<Image>();
    }

    public void ShowSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void ShowInfo(GameObject info)
    {
        presentInfo.SetActive(false);
        medievalInfo.SetActive(false);
        ancientInfo.SetActive(false);

        info.SetActive(true);
    }
    
}
