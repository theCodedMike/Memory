using UnityEngine;
using System.Collections;

//记忆卡片类
public class Done_MemoryCard : MonoBehaviour {
	
	//[SerializeField] private SceneController controller;
	//[SerializeField] private GameObject cardBack;

    //public Done_SceneController controller;
    public GameObject cardBack;

    private int _id;
	public int id {
		get { return _id; }
	}

	public void SetCard(int id, Sprite image) {
		_id = id;
		Debug.Log ("Setting card");
		Debug.Log (id);
		GetComponent<SpriteRenderer>().sprite = image;
	}

    //显示且可以被点击，将卡片背面的显示状态显示为false,将此时点击的卡片值传入
	public void OnMouseDown() {
		if (cardBack.activeSelf && FindObjectOfType<Done_SceneController>().canReveal == true) {
			cardBack.SetActive (false);
            FindObjectOfType<Done_SceneController>().CardRevealed(this);
        }
	}

    //未被点击状态，显示背面
	public void Unreveal() {
		cardBack.SetActive (true);
	}

}
