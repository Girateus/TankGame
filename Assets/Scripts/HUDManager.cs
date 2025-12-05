using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _gaugeFill;
    
    [SerializeField] private Image _healthBarFill; 
    [SerializeField] private DamageTaker _playerDamageTaker; 
    
    [SerializeField] private BoxesManager _boxesManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_playerDamageTaker != null)
        {
            _playerDamageTaker.OnHealthChanged.AddListener(UpdateHealthBar);
            UpdateHealthBar(_playerDamageTaker._hp); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetCounter(_boxesManager.BoxesCount, _boxesManager.MaxBoxes);
    }

    public void SetCounter(int count, int maxCount)
    {
        _text.text = count.ToString("D2") + "/" + maxCount.ToString("D2");
        _gaugeFill.fillAmount = (float)count / maxCount;
    }
    
    public void UpdateHealthBar(float currentHealth)
    {
        if (_healthBarFill != null && _playerDamageTaker != null)
        {
            float maxHealth = _playerDamageTaker.HpMax;
            
            float healthRatio = currentHealth / maxHealth;
            
            _healthBarFill.fillAmount = healthRatio;
        }
    }
    
    
}
