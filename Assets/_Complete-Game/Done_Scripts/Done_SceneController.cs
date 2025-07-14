using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

/// <summary>
/// 游戏控制脚本
/// </summary>
public class Done_SceneController : MonoBehaviour {
	
	public const int gridRows = 3;
	public const int gridCols = 4;
	public const float offsetX = 2f;
	public const float offsetY = 2.5f;

    public const float originalX = -3;
    public const float originalY = 0;

    //[SerializeField] private GameObject winning;
    //[SerializeField] private MemoryCard originalCard;
    //[SerializeField] private Sprite[] images;
    //[SerializeField] private TextMesh scoreLabel;
    //[SerializeField] private TextMesh stepLabel;

    public  GameObject winning;
    public  Done_MemoryCard originalCard;
    public  Sprite[] images;
    public  TextMesh scoreLabel;
    public  TextMesh stepLabel;


    //建立两个卡片对象，在点击判断时使用
    private Done_MemoryCard _firstRevealed;
	private Done_MemoryCard _secondRevealed;

	private int _score = 0;
    private int _step = 0;

    /// <summary>
    /// 将图片打乱随机分给不同的卡片
    /// </summary>
	void Start () {

        //设置win图像显示为false
		winning.SetActive(false);

		//Vector3 startPos = originalCard.transform.position;

        //设置数组并打乱
		int[] numbers = {0,0,1,1,2,2,3,3,4,4,5,5};
		numbers = ShuffleArray (numbers);


		for (int i = 0; i < gridCols; i++) {
			for (int j = 0; j < gridRows; j++) {
				Done_MemoryCard card;

                card = Instantiate(originalCard) as Done_MemoryCard;

                //按顺序给牌定义数字位置下标，赋予id，显示图片
                int index = j * gridCols + i;
				int id = numbers [index];
				card.SetCard (id, images [id]);

                float posX = (offsetX * i) + originalX;
                float posY = (offsetY * j) + originalY;

                card.transform.position = new Vector3(posX, posY, 1);
            }
		}
	}

    /// <summary>
    /// 打乱数组函数
    /// </summary>
    /// <param name="numbers"></param>
    /// <returns></returns>
	private int[] ShuffleArray(int[] numbers) {
        //复制数组
		int[] newArray = numbers.Clone () as int[];

		for (int i=0; i < newArray.Length; i++) {
			int tmp = newArray [i];
			int r = Random.Range(i, newArray.Length);
			newArray [i] = newArray [r];
			newArray [r] = tmp;
		}

		return newArray;
	}
	
	

	void Update () {
	
	}

    
    /// <summary>
    /// 可以点击状态，即判断是否第二张卡片点击状态被改变
    /// </summary>
	public bool canReveal {
		get {return _secondRevealed == null;}
	}

    /// <summary>
    /// 点击卡片，如果第一次点击则翻开第一张卡片，反之翻开第二张，开启协程
    /// </summary>
    public void CardRevealed(Done_MemoryCard card) {
		if (_firstRevealed == null) {
			_firstRevealed = card;

		} else {
			_secondRevealed = card;

            _step++;
            stepLabel.text = "Step: " + _step;

            StartCoroutine (CheckMatch ());
		}
	}


    /// <summary>
    /// 配对函数
    /// </summary>
    /// <returns></returns>
    
    private IEnumerator CheckMatch() {
        //如果两张卡片的id相同，分数增加，如果分数增加到了一定值，判断胜利
        //如果不相同，等待0.5秒将卡片翻转
        //清空两个点击的状态
        if (_firstRevealed.id == _secondRevealed.id) {

			_score++;
			scoreLabel.text = "Score: " + _score;

            if (_score == ((gridRows * gridCols) / 2)) {
				winning.SetActive(true);
			}
		} else {
			yield return new WaitForSeconds (1.5f);

            _firstRevealed.Unreveal ();
			_secondRevealed.Unreveal ();
		}

		_firstRevealed = null;
		_secondRevealed = null;
	}

    
    /// <summary>
    /// 加载关卡
    /// </summary>
	public void Restart() {
        //Application.LoadLevel ("Memory");
        EditorSceneManager.LoadScene("Done_Memory");
	}
}
