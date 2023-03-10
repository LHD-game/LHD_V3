using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInfoManager : MonoBehaviour
{
    //경험치를 갱신 메소드
    public static void GetExp(float exp)
    {
        float now_exp = PlayerPrefs.GetFloat("NowExp");
        float max_exp = PlayerPrefs.GetFloat("MaxExp");
        int level = PlayerPrefs.GetInt("Level");

        //현재 경험치 += 얻은 경험치
        now_exp += exp;

        // over_exp = 최대 경험치 - 현재 경험치
        float over_exp = max_exp - now_exp;

        //if (over_exp <= 0) --> 현재 레벨 += 1, 현재 경험치 = over_exp, 최대 경험치 += 20
        if (over_exp <= 0)
        {
            level += 1;
            now_exp = over_exp;
            max_exp += 20;      // 레벨이 오르면 최대 경험치가 20 증가한다.
            //todo: 레벨이 5, 10일때 각각 배지 획득
            if(level == 5)
            {
                BadgeManager.GetBadge("B1");
            }
            else if(level == 10)
            {
                BadgeManager.GetBadge("B2");
            }
            //todo: 레벨업 시 이펙트 효과 팝업 실행되도록
        }
        //prefs 갱신
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetFloat("NowExp", now_exp);
        PlayerPrefs.SetFloat("MaxExp", max_exp);

        SavePlayInfo();
    }

    //재화 갱신 메소드
    public static void GetCoin(int coin)  //상점에서 아이템 구매시, coin은 음수. 미니게임/퀘스트 등으로 코인을 얻는 경우 coin은 양수
    {
        int wallet = PlayerPrefs.GetInt("Wallet");

        wallet += coin;

        PlayerPrefs.SetInt("Wallet", wallet);

        SavePlayInfo();
    }

    //hp 갱신 메소드
    public static void GetHP(int hp)
    {
        int now_hp = PlayerPrefs.GetInt("HP");
        now_hp += hp;
        PlayerPrefs.SetInt("HP", now_hp);
        SavePlayInfo();
    }

    public static void GetQuestPreg()
    {
        Debug.Log("퀘스트 완료" + PlayerPrefs.GetString("QuestPreg"));
        SavePlayInfo();
    }

    //서버 상 play_info에 prefs저장하는 메소드
    static void SavePlayInfo()
    {
        int new_wallet = PlayerPrefs.GetInt("Wallet");
        int new_level = PlayerPrefs.GetInt("Level");
        float new_now_exp = PlayerPrefs.GetFloat("NowExp");
        float new_max_exp = PlayerPrefs.GetFloat("MaxExp");
        string new_quest_preg = PlayerPrefs.GetString("QuestPreg");
        int new_last_q_time = PlayerPrefs.GetInt("LastQTime");
        int new_hp = PlayerPrefs.GetInt("HP");
        int new_last_hp_time = PlayerPrefs.GetInt("LastHPTime");
        int new_house_lv = PlayerPrefs.GetInt("HouseLv");
        string new_weekly_quest_preg = PlayerPrefs.GetString("WeeklyQuestPreg");

        Param param = new Param();
        param.Add("Wallet", new_wallet);
        param.Add("Level", new_level);
        param.Add("NowExp", new_now_exp);
        param.Add("MaxExp", new_max_exp);
        param.Add("QuestPreg", new_quest_preg);
        param.Add("LastQTime", new_last_q_time);
        param.Add("HP", new_hp);
        param.Add("LastHPTime", new_last_hp_time);
        param.Add("HouseLv", new_house_lv);
        param.Add("WeeklyQuestPreg", new_weekly_quest_preg);

        //유저 현재 row 검색
        var bro = Backend.GameData.Get("PLAY_INFO", new Where());
        string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

        //해당 row의 값을 update
        var bro2 = Backend.GameData.UpdateV2("PLAY_INFO", rowIndate, Backend.UserInDate, param);

        if (bro2.IsSuccess())
        {
            Debug.Log("SavePlayInfo 성공. PLAY_INFO가 업데이트 되었습니다.");
        }
        else
        {
            Debug.Log("SavePlayInfo 실패.");
        }

        
    }
}
