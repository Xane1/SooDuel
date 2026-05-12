using UnityEngine;

public class StageSelectorScript : MonoBehaviour
{
    public GameObject PresentStage;
    public GameObject MedievalStage;
    public GameObject AncientStage;
    
    void Start()
    {
        if (MapManager.Instance != null)
        {
            if (MapManager.Instance.CurrentStage == MapManager.Stage.Ancient)
            {
              PresentStage.SetActive(false);
              AncientStage.SetActive(true);
            }

            if (MapManager.Instance.CurrentStage == MapManager.Stage.Medieval)
            {
                PresentStage.SetActive(false);
                MedievalStage.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
