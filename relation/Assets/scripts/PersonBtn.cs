using UnityEngine;
using System.Collections;
using System.Threading;

public class PersonBtn : MonoBehaviour {
    public UImanage uimanage;
    public UILabel text;
    public int PersonNum = 0;
    // Use this for initialization
    void Start () {
	
	}

    void OnClick()
    {
        uimanage.OpenThePersonInfo(this.PersonNum);
    }

    
    // Update is called once per frame
    void Update () {
	
	}
}
