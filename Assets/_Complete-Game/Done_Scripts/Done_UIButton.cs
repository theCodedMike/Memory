using UnityEngine;
using System.Collections;


/// <summary>
/// 重新开始函数
/// </summary>
public class Done_UIButton : MonoBehaviour {

	public GameObject targetObject;
	public string targetMessage = "Restart";

	public void OnMouseUp() {

		if (targetObject != null) {
			targetObject.SendMessage (targetMessage);
		}

	}
	
}
