using System;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    private GameObject _cardBack;
    
    private int _id;
    public int ID => _id;
    private SpriteRenderer _spriteRenderer;


    private void Start()
    {
        _cardBack = transform.GetChild(0).gameObject;
    }

    public void SetCard(int id, Sprite image)
    {
        if(_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
        _id = id;
        _spriteRenderer.sprite = image;
    }

    // 点击卡片
    private void OnMouseDown()
    {
        _cardBack.SetActive(false);
    }

    // 翻回卡片
    public void Cover()
    {
        _cardBack.SetActive(true);
    }
}
