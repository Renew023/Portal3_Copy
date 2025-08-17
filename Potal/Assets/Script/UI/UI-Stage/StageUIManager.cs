using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StageUIManager : MonoBehaviour
{

    //public int CurStage { get { return curStage; } }
    public int DataCount { get { return dataCount; } }
   
    private List<StageButton> Buttons = new List<StageButton>();

    private const string curStageKey = "curstage";
    [SerializeField]
    private int curStage;
    [SerializeField]
    private StageSettingHelper settingHelper;



    private int dataCount;



    private void Awake()
    {
        Buttons = GetComponentsInChildren<StageButton>().ToList();

        //   curStage = PlayerPrefs.GetInt(curStageKey, 0);

    }
    private void Start()
    {

        settingHelper = GetComponent<StageSettingHelper>();
         dataCount = settingHelper.GetDatasNum();
        InitButton();
        //dataCount; //연결
    }


    private void OnEnable()
    {
        StageManager.OnClearStage += HandleStage;
      
    }

    private void OnDestory()
    {
        StageManager.OnClearStage -= HandleStage;
       
    }


    private void HandleStage()
    {

        Debug.Log("Handle호출");
        UpdateCurStage();
        InitButton();
        
    }
    public void UpdateCurStage() //버튼 인덱스
    {
        Debug.Log("점수 업데이트");
        if (Buttons[curStage].IsClear == true)
        {
            return;
        }
       
        curStage += 1;
        PlayerPrefs.SetInt(curStageKey,curStage);
        //해당 스테이지 클리어시 호출해줘야함
    }

    /*
    public void InitButtons()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            Buttons[i].InitButton(i, this);
            if (curStage > i)
            {
                Buttons[curStage].IsClear = true;
            }
           
            //버튼 인덱스 넣어주기 
        }
        
    }
     */

    public void InitButton()
    { 
      for (int i = 0; i < Buttons.Count; i++)
        {
            Buttons[i].InitButton(i, this);
            if (dataCount > i)
            {
                Buttons[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                Buttons[i].GetComponent<Button>().interactable = false;
            }
        }
    }






    public void OnSelectedClicked(int stage)
    {


        LoadSceneManager.Instance.LoadSceneAsync("TestStageScene", () => {settingHelper.FindmapIndex(stage);}); //이름 넣어주기

    }

        // 저희 각자맵 -> 선택 

        //선우님이 맵에디로 생성된 맵 -> 선택 =>

        //선우님 맵 표시 별개로 저희가 만든 맵 로드 

}