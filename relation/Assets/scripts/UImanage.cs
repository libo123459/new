using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class UImanage : MonoBehaviour {
	public social social;
	public GameObject personBtn;

    public GameObject Grid;
	public GameObject GrandGrid;
	public GameObject ParentGrid;
	public GameObject BroSisGrid;
	public GameObject ChildrenGrid;

    List<GameObject> Popu = new List<GameObject>();
    public List<GameObject> GrandList = new List<GameObject>();
    public List<GameObject> ParentList = new List<GameObject>();
    public List<GameObject> BroSisList = new List<GameObject>();
    public List<GameObject> ChildList = new List<GameObject>();
    
    public GameObject PersonInfo;

    public UILabel personInfoText;

    // Use this for initialization
    void Awake()
    {
        personBtn.GetComponent<PersonBtn> ().uimanage = this;
    }
    void Start () 
	{
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayThePerson()
    {
        for (int i = 0; i < social.Popu.Count; i++)
        {
            GameObject newperson = Instantiate(personBtn);
            PersonBtn theperson = newperson.GetComponent<PersonBtn>();
            theperson.transform.localScale = new Vector3(1, 1, 1);
            theperson.transform.SetParent(Grid.transform);
            theperson.transform.localScale = new Vector3(1,1,1);
            theperson.text.text = social.Popu[i].lastname + " " + social.Popu[i].firstname;
            theperson.PersonNum = social.Popu[i].PersonNum;
            Popu.Add(newperson);           
        }
        Grid.GetComponent<UIGrid>().Reposition();
    }

	public void OpenThePersonInfo(int personNum)
	{
        PersonInfo.SetActive(true);
        if (familierNum(personNum) == 1)
        {
            remove();
        }
        personInfoText.text = social.Popu[personNum].lastname + "" + social.Popu[personNum].firstname;
              
        putTheFamiliersToList(social.Popu[personNum].Grand, GrandList, GrandGrid);
        putTheFamiliersToList(social.Popu[personNum].Parent, ParentList, ParentGrid);
        putTheFamiliersToList(social.Popu[personNum].BroSis, BroSisList, BroSisGrid);
        putTheFamiliersToList(social.Popu[personNum].Children, ChildList, ChildrenGrid);

        gridAllRepos();
    }

    void putTheFamiliersToList(List<Person> Familiers, List<GameObject> _FamList, GameObject thegrid)
    {
        for (int i = 0; i < Familiers.Count; i++)
        {
            if (Familiers[i] != null)
            {
                GameObject familier = Instantiate(personBtn);
                familier.transform.SetParent(thegrid.transform);
                
                familier.transform.localScale = new Vector3(1, 1, 1);

                PersonBtn theperson = familier.GetComponent<PersonBtn>();
                theperson.text.text = Familiers[i].lastname + " " + Familiers[i].firstname;
                theperson.PersonNum = Familiers[i].PersonNum;

                _FamList.Add(familier);
                
            }
        }
    }

    public void BackToMenu()
    {
        PersonInfo.SetActive(false);
        remove();
    }

    int familierNum(int personNum)
    {
        int grand = social.Popu[personNum].Grand.Count;
        int parent = social.Popu[personNum].Parent.Count;
        int BroSis = social.Popu[personNum].BroSis.Count;
        int children = social.Popu[personNum].Children.Count;

        if (grand + parent + BroSis + children == 0)
        {
            return 0;
        }
        else {
            return 1;
        }
    }

    void remove()
    {
        removeTheList(GrandList, GrandGrid);
        removeTheList(ParentList, ParentGrid);
        removeTheList(BroSisList, BroSisGrid);
        removeTheList(ChildList, ChildrenGrid);
    }

    public void gridAllRepos()
    {
        gridRepos(GrandGrid);
        gridRepos(ParentGrid);
        gridRepos(BroSisGrid);
        gridRepos(ChildrenGrid);
    }

    static void gridRepos(GameObject _grid)
    {
        _grid.GetComponent<UIGrid>().repositionNow = true;
        _grid.GetComponent<UIGrid>().Reposition();
    }

    void removeTheList(List<GameObject> _FamList,GameObject _grid)
    {
        foreach (Transform child in _grid.transform)
        {
            Destroy(child.gameObject);
        }
        _FamList.Clear();
    }

    
}
