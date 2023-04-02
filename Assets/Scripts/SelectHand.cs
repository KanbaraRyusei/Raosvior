using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectHand : MonoBehaviour
{ 
    [SerializeField]
    [Header("カードの画像")]
    Image[]  _cards;

    [Header("使用するカードのリスト")]
    private List<Image> _selectCards;

    List<Image> SelectCard
    {
        get { return _selectCards; }
        set { _selectCards = value; }
     }

    [SerializeField]
    [Header("現在選んでいるカードの数")]
    int _count;

    [SerializeField]
    [Header("使用するカードの現在の選択数")]
    int[] _selectCount;

    [SerializeField]
    [Header("カードごとの現在えらんでいる枚数を表示するテキスト")]
    TMP_Text [] _presentCount;

    [SerializeField]
    [Header("合計選択中のカード枚数を表示するテキスト")]
    TMP_Text _text;

    private void Awake()
    {
        SelectCard = new List<Image>();
    }

    public void PulseCllick(int i)
    {
        if (_count < 5)
        {
            if (_selectCount[i] < 2)
            { 
                _count++;
                _selectCount[i]++;
                _text.text = _count + "/5";
                _presentCount[i].text = "ー " + _selectCount[i] + "/";
                _selectCards.Add(_cards[i]);
            }
        }
    }

    public void OneEachCards(int i)
    {
        if (_count < 5)
        {
            if (_selectCount[i] < 1)
            {
                _count++;
                _selectCount[i]++;
                _text.text = _count + "/5";
                _presentCount[i].text = "ー " + _selectCount[i] + "/";
                _selectCards.Add(_cards[i]);
            }
        }
    }

    public void MinusClick(int i)
    {
        if (_count <= 5 && _count > 0)
        {
            if (_selectCount[i] > 0)
            {             
                _count--;
                _selectCount[i]--;
                _text.text = _count + "/5";
                _presentCount[i].text = "ー " + _selectCount[i] + "/";
                _selectCards.Remove(_cards[i]);
            }
        }
    }

}
