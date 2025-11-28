using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxesManager : MonoBehaviour
{
    
    //public List<DestroyableBox> _boxes { get; private set;}
    
    private List<DestroyableBox> _boxes;
    public List<DestroyableBox> Boxes => _boxes;
    public int BoxesCount => _boxes.Count;
    public int MaxBoxes;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boxes = GetComponentsInChildren<DestroyableBox>().ToList();

        foreach (DestroyableBox box in _boxes)
        {
            box.OnBoxDestroyed += RemoveBox;
        }
        
        MaxBoxes = _boxes.Count;
    }

    void RemoveBox(DestroyableBox box)
    {
        _boxes.Remove(box);
    }
}
