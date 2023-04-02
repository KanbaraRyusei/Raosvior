using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

/// <summary>
/// プレイヤーのデータを持つクラス
/// </summary>
public class PlayerData : IPlayerParameter,IHandCollection, IChangeableLife
{
    #region Properties

    /// <summary>
    /// プレイヤーのライフの読み取り専用プロパティ
    /// </summary>
    public int Life => _life;

    /// <summary>
    /// プレイヤーのシールドの読み取り専用プロパティ
    /// </summary>
    public int Shield => _shield;

    /// <summary>
    /// プレイヤーのリーダーカードの読み取り専用プロパティ
    /// </summary>
    public LeaderPlayerHand LeaderHand => _leaderHand;

    /// <summary>
    /// プレイヤーの手札の読み取り専用プロパティ
    /// キャストして変更しようとする奴は殺すので要注意
    /// </summary>
    public IReadOnlyList<RSPPlayerHand> PlayerHands => _playerHands;

    /// <summary>
    /// プレイヤーのリザーブの読み取り専用プロパティ
    /// キャストして変更しようとする奴は殺すので要注意
    /// </summary>
    public IReadOnlyList<RSPPlayerHand> PlayerReserve => _playerReserve;

    /// <summary>
    /// 場にセットするカードの読み取り専用プロパティ
    /// </summary>
    public RSPPlayerHand PlayerSetHand => _playerSetHand;

    #endregion

    #region Member Variables

    /// <summary>
    /// プレイヤーのライフ
    /// </summary>
    private int _life = 5;

    /// <summary>
    /// プレイヤーのシールド
    /// </summary>
    private int _shield = 0;

    /// <summary>
    /// プレイヤーのリーダーカード
    /// </summary>
    private LeaderPlayerHand _leaderHand = null;

    /// <summary>
    /// プレイヤーの手札
    /// 上限が決まっているため、要素数を指定して少し軽くした
    /// </summary>
    private List<RSPPlayerHand> _playerHands = new(5);

    /// <summary>
    /// プレイヤーのリザーブ
    /// 上限が決まっているため、要素数を指定して少し軽くした
    /// </summary>
    private List<RSPPlayerHand> _playerReserve = new(5);

    /// <summary>
    /// シールドトークン化した敵のカード
    /// </summary>
    private Stack<RSPPlayerHand> _rspShildToken = new();

    /// <summary>
    /// 場にセットするカード
    /// </summary>
    private RSPPlayerHand _playerSetHand = null;

    #endregion

    #region Constants

    private const int LIFE_DEFAULT = 5;

    #endregion

    #region Constructor

    public PlayerData()
    {
        Init();
    }

    #endregion

    #region Private Methods

    private void Init()// 初期化する関数
    {
        _life = LIFE_DEFAULT;
        _shield = 0;
    }

    #endregion

    #region IHandCollection Interface

    public void SetHand(RSPPlayerHand playerHand)
    {
        _playerHands.Remove(playerHand);// 手札のListからカードを削除する
        _playerSetHand = playerHand;// 場にカードをセット
    }

    public void SetCardOnReserve()
    {
        if (_playerSetHand != null)
        {
            _playerReserve.Add(_playerSetHand);// セットされているカードをリザーブに送る
            _playerSetHand = null;// セットされていたカードを消す
        }
    }

    public void AddHand(RSPPlayerHand playerHand)
    {
        _playerHands.Add(playerHand);// 手札にカードを加える
    }

    public void RemoveHand(RSPPlayerHand playerHand)
    {
        _playerHands.Remove(playerHand);// 手札のカードを消す
    }

    public void OnReserveHand(RSPPlayerHand playerHand)
    {
        _playerHands.Remove(playerHand);// 手札からカードを削除
        _playerReserve.Add(playerHand);// リザーブにカードを追加
    }

    public void CardBack()
    {
        _playerHands.Add(_playerSetHand);
        _playerSetHand = null;// セットされていたカードを消す
    }

    public void SetLeaderHand(LeaderPlayerHand leader)
    {
        _leaderHand = leader;
    }

    public void ResetHand()
    {
        _playerHands = _playerReserve;
        _playerReserve.Clear();
    }

    #endregion

    #region IChangeableLife Interface

    public void HealLife(int heal)
    {
        _life += heal;// ライフを回復 上限がないため余計な処理はない
    }

    public async void ReceiveDamage(int damage = 1)
    {
        if(_leaderHand.HandEffect.GetType() == typeof(ShamanData))
        {
            _leaderHand.HandEffect.CardEffect();
            await UniTask.WaitUntil(() =>
                PhaseManager.CurrentPhaseProperty != PhaseParameter.Intervention);

            var shaman =_leaderHand.HandEffect as ShamanData;
            if (shaman.IsReducing) damage--;

        }
        if(_shield > 0)// シールドがあるかどうか判定
        {
            if(_shield - damage >= 0)// シールドの数がダメージより大きかったら
            {
                _shield -= damage;// シールドを削る
                return;
            }
            else// シールドの数がダメージより少なかったら
            {
                damage -= _shield;// ダメージをシールドの数減らす
                _shield = 0;// シールドを0にする
            }
        }

        _life -= damage;// ライフを減らす

        for (int i = 0; i < damage; i++)
        {
            if (_rspShildToken.Count == 0) return;

            if (PlayerManager.Players[0].HandCollection != this)
            {
                PlayerManager
                    .Players[0]
                    .HandCollection
                    .AddHand(_rspShildToken.Pop());
            }
            else
            {
                PlayerManager
                    .Players[1]
                    .HandCollection
                    .AddHand(_rspShildToken.Pop());
            }
        }
    }

    public void GetShield(int num, RSPPlayerHand playerHand = null)
    {
        _shield += num;// シールドを追加 上限がないため余計な処理はない

        if (playerHand != null) _rspShildToken.Push(playerHand);
    }

    #endregion
}
