using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SceneController : MonoBehaviour
{
    public int gridRows = 3;
    public int gridCols = 4;
    public float offsetX = 2f;
    public float offsetY = 2.5f;
    public float originalX = -3f;
    public float originalY = -2.5f;
    public GameObject cardPrefab;
    public Sprite[] images;

    private MemoryCard _first; // 第1次点击的卡片
    private MemoryCard _second; // 第2次点击的卡片

    public bool CanReveal => _second == null;

    private void Start()
    {
        int[] nums = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5};
        Shuffle(nums);
        
        for (int i = 0; i < gridRows; i++)
            for (int j = 0; j < gridCols; j++)
        {
            GameObject cardObj = Instantiate(cardPrefab, new Vector3(originalX + j * offsetX, originalY + i * offsetY, 1), Quaternion.identity);
            int id = nums[i * gridCols + j];
            cardObj.GetComponent<MemoryCard>().SetCard(id, images[id]);
        }
    }

    // 打乱数组
    private void Shuffle(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            int idx = Random.Range(i, nums.Length);
            (nums[i], nums[idx]) = (nums[idx], nums[i]);
        }
    }

    // 点击卡片
    public void CardReveal(MemoryCard card)
    {
        if (_first == null)
        {
            _first = card;
            return;
        }

        // 如果第1次和第2次点击了同一张卡片
        if (_first == card)
        {
            _first.Cover();
            _first = null;
            return;
        }
        
        _second = card;
        StartCoroutine(CheckMatch());
    }

    private IEnumerator CheckMatch()
    {
        if (_first.ID == _second.ID)
        {
            // 匹配成功
            _first.MatchSuccess();
            _second.MatchSuccess();
        }
        else
        {
            // 匹配失败，1.5秒后2张牌都翻转
            yield return new WaitForSeconds(1.5f);
            _first.Cover();
            _second.Cover();
        }

        _first = null;
        _second = null;
    }
}
