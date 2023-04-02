using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    #region Inspector Variables

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

    #endregion

    #region Public Methods

    public void ChangeLifeText(int value)// テキストに引数を代入
    {
        _lifeText.text = value.ToString();
    }

    public void ChangeShieldText(int value)// テキストに引数を代入
    {
        _shieldText.text = value.ToString();
    }

    public void ChangeReserveText(int value)// テキストに引数を代入
    {
        _playerReserve.text = value.ToString();
    }

    public void ChangeHandsImage(Image img, int index)// Imageに引数を代入
    {
        _playerHands[index] = img;
    }

    public void ChangeSetHandImage(Image img)// Imageに引数を代入
    {
        _playerSetHand = img;
    }

    #endregion
}
