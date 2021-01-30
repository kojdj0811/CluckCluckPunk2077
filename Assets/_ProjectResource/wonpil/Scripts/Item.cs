using System.Collections.Generic;
using UnityEngine;

public enum itemTypes
{
    Recovery,
    Poison,
    Shield,
}
public enum itemEffects
{
    effectRecovery,
    effectPoison,
    effectShield,
}

[CreateAssetMenu(fileName = "Item", menuName = "ItemAsset/Item", order = 0)]
public class Item : ScriptableObject
{
    [SerializeField]
    private Sprite _previewImage;    
    
    [SerializeField]
    private itemTypes _itemType;
    
    [SerializeField]
    private itemEffects _itemEffect;
    
    [SerializeField]
    private string _description;
    
    [SerializeField]
    private GameObject _prefab;
    
    [SerializeField]
    private List<int> _testValueList = new List<int>();
    
    public Sprite PreviewImage => _previewImage;
    public string Description => _description;
    public itemTypes ItemType => _itemType;
    public itemEffects ItemEffect => _itemEffect;
    public GameObject Prefab => _prefab;
    public List<int> TestValueList => _testValueList;
}