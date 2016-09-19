using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UImanage : MonoBehaviour {
	public social social;
	public Person person;
	
	public GameObject GrandGrid;
	public GameObject ParentGrid;
	public GameObject BroSisGrid;
	public GameObject ChildrenGrid;

    List<GameObject> GrandList = new List<GameObject>();
    List<GameObject> ParentList = new List<GameObject>();
    List<GameObject> BroSisList = new List<GameObject>();
    List<GameObject> ChildList = new List<GameObject>();

    public GameObject BtnInInfo;
    
    public GameObject PersonInfo;

    public UILabel personInfoText;

    // Use this for initialization
    void Awake()
    {
        person.uimanage = this;
    }
    void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenThePersonInfo(int personNum)
	{
        PersonInfo.SetActive(true);
		//personInfo.text = GetTheName(personNum);
		for (int i = 0; i < social.Popu[personNum].Grand.Count; i++)
		{
			if(social.Popu[personNum].Grand[i] != null)
			{
				GameObject Grand = Instantiate(BtnInInfo);
                Grand.transform.SetParent(GrandGrid.transform);
                Grand.transform.localScale = new Vector3(1,1,1);
			}
		}

		for (int i = 0; i < social.Popu[personNum].Parent.Count; i++)
        {
			if(social.Popu[personNum].Parent[i] != null)
			{
				GameObject parent = Instantiate(BtnInInfo);
                parent.transform.SetParent(ParentGrid.transform);
                parent.transform.localScale = new Vector3(1,1,1);
			}
		}

		for (int i = 0; i < social.Popu[personNum].BroSis.Count; i++)
		{
			if(social.Popu[personNum].BroSis[i] != null)
			{
				GameObject BroSis = Instantiate(BtnInInfo);
                BroSis.transform.SetParent(BroSisGrid.transform);
                BroSis.transform.localScale = new Vector3(1,1,1);
			}
		}
        
		for (int i = 0; i < social.Popu[personNum].Children.Count; i++)
		{
			if(social.Popu[personNum].Children[i] != null)
			{
				GameObject Children = Instantiate(BtnInInfo);
                Children.transform.SetParent(ChildrenGrid.transform);
                Children.transform.localScale = new Vector3(1,1,1);
			}
		}
	}

    

    
}
