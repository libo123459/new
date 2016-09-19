using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class social : MonoBehaviour {
    public GameObject person;
    public UImanage uimanage;
	public UIGrid grid;
    private string[] clan = new string[] { "Smith", "Jones", "Williams", "Taylor", "Brown", "Wilson"};
    private string[] MaleNames = new string[] { "Byron","Dick","Evan","James","Joseph","Alex", "Kim", "Bill", "Daniel", "David", "Paul", "Joe", "Peter", "Tom" };
    private string[] FemaleNames = new string[] { "Mary", "Jenny", "Mila", "Gianna", "Elissa", "Tiffany", "Sarah", "Jessica", "Lisa", "Jennifer", "Eva", "Alice", "Anne", "Judy" };
    public List<string> Clan = new List<string>();
    public List<Person> Popu = new List<Person>();
    
    private List<List<Person>> AllFam = new List<List<Person>>();
    private int FamNum = 0;

    // Use this for initialization
    void Start () {
       
        intiTheClan();
        intiThePopu();
        makeFamily();
        evolution();
        giveTheNumAndPrintName();
        printInfo();
        uimanage.DisplayThePerson();
	}

    void intiTheClan()                                                                  ///初始化家族
    {
        for (int i = 0; i < clan.Length; i++)
        {
            Clan.Add(clan[i]);
        }
    }

    void intiThePopu()                                                                  ///初始化人口数量
    {
        for (int i = 0; i < Clan.Count; i++)  
        {
            for (int j = 0; j < 2; j++)                                                 ///给每个家族安排两个人
            {
				
				GameObject newperson = Instantiate(person.gameObject);
                newperson.SetActive(false);
				newperson.transform.parent = this.transform;
								
				newperson.GetComponent<Person>().PersonNum = i;
		        newperson.GetComponent<Person>().single = true;                                                ///是否单身
                newperson.GetComponent<Person>().sex = Mathf.Abs(1-j);                                         ///1为男性，0为女性

                giveTheName(newperson.GetComponent<Person>(), Clan[i]);
                newperson.GetComponent<Person>().ClanTag = Clan[i];
                Popu.Add(newperson.GetComponent<Person>());                                                    ///整个人口的List
            }
        }

        for (int i = 0; i < Popu.Count; i++)                                            ///初始人口的brolist
        {
            Person theperson = Popu[i];

            for (int j = 0; j < Popu.Count; j++)
            {
                if (i != j)
                {
                    if (theperson.ClanTag == Popu[j].ClanTag)
                    {
                        theperson.BroSis.Add(Popu[j]);
                    }
                } else
                {
                    continue;
                }
            }
        }
        print(Popu.Count);
    }

    void makeFamily()                                                                   ///组建家庭
    {
        for (int i = 0; i < Popu.Count; i++)
        {
            Person thePerson = Popu[i];                                                 ///从头开始遍历
            if (Popu[i].single == true)
            {
                for (int j = 0; j < Popu.Count; j++)
                {
                    int married = Random.Range(0, 100);                                     ///Marry的几率
                    if (married <= 20)
                    {
                        Person other = Popu[j];                                             ///目标对象
                        if (i != j)                                                         ///在不是自身的情况下
                        {
                            bool check = CheckIfCanMarry(thePerson,other);
                            if (thePerson.sex != other.sex && other.single == true && check == true)       ///两人单身且异性
                            {
                                thePerson.single = false;
                                other.single = false;
                                List<Person> family = new List<Person>();
                                family.Add(thePerson);
                                family.Add(other);                                          ///两人组成一个家庭
                                AllFam.Add(family);                                 ///第famnum号家庭
                                FamNum++;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    bool CheckIfCanMarry(Person thePerson,Person other)
    {
        bool NoSameGra = false;///爷爷辈没有同一个人
        bool NoGraUncle = false;///爷爷辈不是兄弟姐们
        bool NoSamePar = false;///父母没有同一个人
        bool NoUncle = false;///父母不是兄弟姐妹
        bool NoParInGra = false; ///父母不是爷爷辈
        bool NoGraInPar = false;///爷爷辈不是父母

        for (int i = 0; i < thePerson.Grand.Count; i++)                                 
        {
            for (int j = 0; j < other.Grand.Count; j++)                                   ///爷爷辈没有同一个人
            {
                if (thePerson.Grand[i] != other.Grand[j])
                {
                    continue;
                }
                else {
                    NoSameGra = true;
                }
            }

            for (int j = 0; j < thePerson.Grand[i].BroSis.Count; j++)                    ///爷爷辈不是兄弟姐们
            {
                for (int k = 0; k < other.Grand.Count; k++)
                {
                    if (thePerson.Grand[i].BroSis[j] != other.Grand[k])
                    {
                        continue;
                    }
                    else
                    {
                        NoGraUncle = true;
                    }
                }
            }

            for (int j = 0; j < other.Parent.Count; j++)                                ///父母不是爷爷辈                     
            {
                if (thePerson.Grand[i] != other.Parent[j])
                {
                    continue;
                }
                else
                {
                    NoParInGra = true;
                }
            }
        }


        for (int i = 0; i < thePerson.Parent.Count; i++)                               ///父母没有同一个人
        {
            for (int j = 0; j < other.Parent.Count; j++)
            {
                if (thePerson.Parent[i] != other.Parent[j])
                {
                    continue;
                }
                else
                {
                    NoSamePar = true;
                }
            }

            for (int j = 0; j < thePerson.Parent[i].BroSis.Count; j++)                 ///父母不是兄弟姐妹
            {
                for (int k = 0; k < other.Parent.Count; k++)
                {
                    if (thePerson.Parent[i].BroSis[j] != other.Parent[k])
                    {
                        continue;
                    }
                    else
                    {
                        NoUncle = true;
                    }
                }
            }

            for (int j = 0; j < other.Grand.Count; j++)                                 ///爷爷辈不是父母
            {
                if (thePerson.Parent[i] != other.Grand[j])
                {
                    continue;
                }
                else
                {
                    NoGraInPar = true;
                }
            }
        }

        if (NoSameGra == true || NoGraUncle == true || NoSamePar == true || NoUncle == true || NoParInGra == true || NoGraInPar == true)
        {
            return false;
        }
        else {
            return true;
        }
    }

    void giveTheName(Person thePerson,string lastname)                             ///生成姓名
    {
        if (thePerson.sex == 0)
        {
            thePerson.firstname = FemaleNames[Random.Range(0, FemaleNames.Length)];
            thePerson.lastname = lastname;
        }
        else {
            thePerson.firstname = MaleNames[Random.Range(0, MaleNames.Length)];
            thePerson.lastname = lastname;
        }
    }

    void evolution()                                         ///时间推演(生孩子)      
    {
        for (int i = 0; i < AllFam.Count; i++)
        {
			GameObject newperson = Instantiate(person);
            newperson.SetActive(false);
            newperson.transform.parent = this.transform;
                        
            newperson.GetComponent<Person>().single = true;                                               
            newperson.GetComponent<Person>().sex = Random.Range(-1, 1);;                                         
           
            for (int j = 0; j < AllFam[i].Count; j++)
            {
                newperson.GetComponent<Person>().ClanTag = AllFam[i][j].ClanTag;
                AllFam[i][j].Children.Add(newperson.GetComponent<Person>());                              ///孩子加入父母的children数组
                newperson.GetComponent<Person>().Parent.Add(AllFam[i][j]);                                ///孩子的父母加入孩子的parent数组
                for (int k = 0; k < AllFam[i][j].Grand.Count; k++)
                {
                    newperson.GetComponent<Person>().Grand.Add(AllFam[i][j].Parent[k]);                   ///父母的父母加入孩子的Grand数组
                }
                
                if (AllFam[i][j].sex == 1)
                {
                    giveTheName(newperson.GetComponent<Person>(), AllFam[i][j].lastname);
                }
            }
            AllFam[i].Add(newperson.GetComponent<Person>());
            Popu.Add(newperson.GetComponent<Person>());
        }
    }


    void printInfo()                                         ///输出信息
    {
        print("The families count is" + AllFam.Count);
        
        for (int i = 0; i < AllFam.Count; i++)
        {
            print("The Num" + i + " :" + AllFam[i][0].firstname + AllFam[i][0].lastname + " and " + AllFam[i][1].firstname + AllFam[i][1].lastname + " Kid : " + AllFam[i][2].firstname + AllFam[i][2].lastname);
        }
    }

	string GetTheName(int num)
	{
		string text = Popu[num].firstname + Popu[num].lastname;
		return text;
	}

    void giveTheNumAndPrintName()
    {
        for (int i = 0; i < Popu.Count; i++)
        {
            
            Person theperson = Popu[i];
            theperson.PersonNum = i;
            //theperson.text.text = GetTheName(i);
        }
    }

    
	// Update is called once per frame
	void Update () {
	
	}
}
