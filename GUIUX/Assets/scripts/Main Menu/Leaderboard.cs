using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public List<LeaderboardElement> elements;
    public GameObject elementPrefab;
    public Transform scrollViewContent;

    void Start()
    {
        foreach (var element in elements)
        {
            GameObject roomButton = Instantiate(elementPrefab, scrollViewContent);

            //set name and player count
            roomButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = element.position;
            roomButton.transform.GetChild(1).GetComponent<Image>().sprite = element.pfp;
            roomButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = element.name;
            roomButton.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = element.timing;
        }
    }
}
