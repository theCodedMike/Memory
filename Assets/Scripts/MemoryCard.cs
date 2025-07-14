using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    private GameObject _cardBack;
    private SceneController _sceneController;
    private int _id;
    public int ID => _id;
    private SpriteRenderer _spriteRenderer;
    private bool _matchSuccess; // 匹配成功

    private void Start()
    {
        _cardBack = transform.GetChild(0).gameObject;
        _sceneController = FindFirstObjectByType<SceneController>();
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
        // 如果已匹配成功过，则无法再被点击
        if (_matchSuccess)
            return;
        
        // 点击2张不同的卡片后，不能再点击其他卡片
        if (_sceneController.CanReveal)
        {
            _cardBack.SetActive(false);
            _sceneController.CardReveal(this);
        }
    }

    // 翻回卡片
    public void Cover()
    {
        _cardBack.SetActive(true);
    }
    
    // 匹配成功
    public void MatchSuccess()
    {
        _matchSuccess = true;
    }
}
