using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;
    public GameObject pfItemWorldWoodSpawner;

    public Sprite heartSprite;
    public Sprite coinSprite;
    public Sprite gemSprite;
    public Sprite woodSprite;
    public Sprite appleSprite;
    public Sprite barricadeSprite;
    public GameObject barricadePrefab;
}
