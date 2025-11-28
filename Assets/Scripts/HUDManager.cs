using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _gaugeFill;
    
    [SerializeField] private BoxesManager _boxesManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SetCounter(_boxesManager.Boxes.Count, _boxesManager.MaxBoxes);
    }

    public void SetCounter(int count, int maxCount)
    {
        _text.text = count.ToString("D2") + "/" + maxCount.ToString("D2");
        _gaugeFill.fillAmount = (float)count / maxCount;
    }
    
    
}
