using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのデータを持つクラス
/// </summary>
public class PlayerData : MonoBehaviour, IHandCollection, ILifeChange
{
    /// <summary>
    /// プレイヤーのライフの読み取り専用プロパティ
    /// </summary>
    public int Life => _life;
    /// <summary>
    /// プレイヤーのシールドの読み取り専用プロパティ
    /// </summary>
    public int Shild => _shield;
    /// <summary>
    /// プレイヤーの手札の読み取り専用プロパティ
    /// キャストして変更しようとする奴は殺すので要注意
    /// </summary>
    public IReadOnlyList<PlayerHand> PlayerHands => _playerHands;
    /// <summary>
    /// プレイヤーのリザーブの読み取り専用プロパティ
    /// キャストして変更しようとする奴は殺すので要注意
    /// </summary>
    public IReadOnlyList<PlayerHand> PlayerReserve => _playerReserve;
    /// <summary>
    /// 場にセットするカードの読み取り専用プロパティ
    /// </summary>
    public PlayerHand PlayerSetHand => _playerSetHand;

    /// <summary>
    /// プレイヤーのライフ
    /// </summary>
    private int _life;
    /// <summary>
    /// プレイヤーのシールド
    /// </summary>
    private int _shield;

    /// <summary>
    /// プレイヤーの手札
    /// 上限が決まっているため、要素数を指定して少し軽くした
    /// </summary>
    private List<PlayerHand> _playerHands = new List<PlayerHand>(5);
    /// <summary>
    /// プレイヤーのリザーブ
    /// 上限が決まっているため、要素数を指定して少し軽くした
    /// </summary>
    private List<PlayerHand> _playerReserve = new List<PlayerHand>(5);

    /// <summary>
    /// 場にセットするカード
    /// </summary>
    private PlayerHand _playerSetHand;

    private void Start()
    {
        Init();
    }

    private void Init()// 初期化する関数
    {
        _life = ConstParameter.LIFE_DEFAULT;
        _shield = ConstParameter.ZERO;
    }

    public void SetHand(PlayerHand playerHand)
    {
        _playerHands.Remove(playerHand);// 手札のListからカードを削除する
        _playerSetHand = playerHand;// 場にカードをセット
    }

    public void SetCardOnReserve()
    {
        _playerReserve.Add(_playerSetHand);// セットされているカードをリザーブに送る
        _playerSetHand = null;// セットされていたカードを消す
    }

    public void AddHand(PlayerHand playerHand)
    {
        _playerHands.Add(playerHand);// 手札にカードを加える
    }

    public void OnReserveHand(PlayerHand playerHand)
    {
        _playerHands.Remove(playerHand);// 手札からカードを削除
        _playerReserve.Add(playerHand);// リザーブにカードを追加
    }

    public void HealLife(int heal)
    {
        _life += heal;// ライフを回復 上限がないため余計な処理はない
    }

    public void ReceiveDamage(int damage)
    {
        if(_shield > ConstParameter.ZERO)// シールドがあるかどうか判定
        {
            if(_shield - damage >= ConstParameter.ZERO)// シールドの数がダメージより大きかったら
            {
                _shield -= damage;// シールドを削る
                return;
            }
            else// シールドの数がダメージより少なかったら
            {
                damage -= _shield;// ダメージをシールドの数減らす
                _shield = ConstParameter.ZERO;// シールドを0にする
            }
        }
        _life -= damage;// ライフを減らす
    }

    public void GetShield(int num)
    {
        _shield += num;// シールドを追加 上限がないため余計な処理はない
    }
}
