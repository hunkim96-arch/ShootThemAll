using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class RankingManager
{

    private static List<int> rankingList = new List<int>();
    private const int maxRankCount = 20;

    public static void AddScore(int score)
    {
        rankingList.Add(score);
        rankingList = rankingList.OrderByDescending(s => s).Take(maxRankCount).ToList();
    }

    public static List<int> GetRanking()
    {
        return rankingList;
    }


    public static string GetRankingText()
    {
        StringBuilder rsb = new StringBuilder();
        for (int i = 0; i < rankingList.Count; i++)
        {
            rsb.AppendLine($"{i + 1}. {rankingList[i]}");
        }
        return rsb.ToString();
    }




}
