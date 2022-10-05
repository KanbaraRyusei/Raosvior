using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    [Header("ライフのテキスト")]
    Text _lifeText;

    [SerializeField]
    [Header("シールドのテキスト")]
    Text _shieldText;

    [SerializeField]
    [Header("墓地の枚数のテキスト")]
    Text _playerReserve;

    [SerializeField]
    [Header("セットするカードの画像")]
    Image _playerSetHand;

    [SerializeField]
    [Header("手札の画像")]
    Image[] _playerHands;

    public void ChangeLifeText(int value)//テキストに引数を代入
    {
        _lifeText.text = value.ToString();
    }

    public void ChangeShieldText(int value)//テキストに引数を代入
    {
        _shieldText.text = value.ToString();
    }

    public void ChangeReserveText(int value)//テキストに引数を代入
    {
        _playerReserve.text = value.ToString();
    }

    public void ChangeHandsImage(Image[] img)//Imageに引数を代入
    {
        for (int i = 0; i < ConstParameter.FIVE; i++)
        {
            _playerHands[i] = img[i];
        }

    }

    public void ChangeSetHandImage(Image img)//Imageに引数を代入
    {
        _playerSetHand = img;
    }

}
