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
    
    
    private void Start()
    {
        int[] nums = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5};
        Shuffle(nums);
        print(string.Join(',', nums));
        
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
}
