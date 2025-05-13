using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class rankpage : MonoBehaviour
{
    [SerializeField] Transform contentRoot;
    [SerializeField] GameObject rowPrefabs;

    StageResultilist allData;

    // Start is called before the first frame update
    void Start()
    {
        allData = StageResultSaver.LoadRank();
        RefreshRankList();
    }

    void RefreshRankList()
    {
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }
        //이부분을 수정해야된다고 함
        var sortedData = allData.results.Where(r => r.stage ==1).OrderByDescending(x => x.score).ToList();
        

        for (int i = 0; i < sortedData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefabs, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}. {sortedData[i].playerName} - {sortedData[i].score} - {"Stage_1"}";
            
        }

        var sortedData1 = allData.results.Where(r => r.stage == 2).OrderByDescending(x => x.score).ToList();


        for (int i = 0; i < sortedData1.Count; i++)
        {
            GameObject row = Instantiate(rowPrefabs, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}. {sortedData1[i].playerName} - {sortedData1[i].score} - {"Stage_2"}";
        }

        var sortedData2 = allData.results.Where(r => r.stage == 3).OrderByDescending(x => x.score).ToList();


        for (int i = 0; i < sortedData2.Count; i++)
        {
            GameObject row = Instantiate(rowPrefabs, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}. {sortedData2[i].playerName} - {sortedData2[i].score} - {"Stage_3"}";
        }

        var sortedData3 = allData.results.Where(r => r.stage == 4).OrderByDescending(x => x.score).ToList();


        for (int i = 0; i < sortedData3.Count; i++)
        {
            GameObject row = Instantiate(rowPrefabs, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}. {sortedData3[i].playerName} - {sortedData3[i].score} - {"Stage_4"}";
        }

        var sortedData4 = allData.results.Where(r => r.stage == 5).OrderByDescending(x => x.score).ToList();


        for (int i = 0; i < sortedData4.Count; i++)
        {
            GameObject row = Instantiate(rowPrefabs, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}. {sortedData4[i].playerName} - {sortedData4[i].score} - {"Stage_5"}";
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
