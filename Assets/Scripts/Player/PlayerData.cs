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
    public int Life { get; private set; } = 5;

    /// <summary>
    /// プレイヤーのシールドの読み取り専用プロパティ
    /// </summary>
    public int Shield { get; private set; }

    /// <summary>
    /// プレイヤーのリーダーカードの読み取り専用プロパティ
    /// </summary>
    public LeaderPlayerHand LeaderHand { get; private set; }

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
    public RSPPlayerHand PlayerSetHand { get; private set; }

    #endregion

    #region Member Variables

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

    #region IHandCollection Interface

    public void SetHand(RSPPlayerHand playerHand)
    {
        _playerHands.Remove(playerHand);// 手札のListからカードを削除する
        PlayerSetHand = playerHand;// 場にカードをセット
    }

    public void SetCardOnReserve()
    {
        if (PlayerSetHand != null)
        {
            _playerReserve.Add(PlayerSetHand);// セットされているカードをリザーブに送る
            PlayerSetHand = null;// セットされていたカードを消す
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
        _playerHands.Add(PlayerSetHand);
        PlayerSetHand = null;// セットされていたカードを消す
    }

    public void SetLeaderHand(LeaderPlayerHand leader)
    {
        LeaderHand = leader;
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
        Life += heal;// ライフを回復 上限がないため余計な処理はない
    }

    public async void ReceiveDamage(int damage = 1)
    {
        if(LeaderHand.HandEffect.GetType() == typeof(ShamanData))
        {
            LeaderHand.HandEffect.CardEffect();
            await UniTask.WaitUntil(() =>
                PhaseManager.CurrentPhaseProperty != PhaseParameter.Intervention);

            var shaman =LeaderHand.HandEffect as ShamanData;
            if (shaman.IsReducing) damage--;

        }
        if(Shield > 0)// シールドがあるかどうか判定
        {
            if(Shield - damage >= 0)// シールドの数がダメージより大きかったら
            {
                Shield -= damage;// シールドを削る
                return;
            }
            else// シールドの数がダメージより少なかったら
            {
                damage -= Shield;// ダメージをシールドの数減らす
                Shield = 0;// シールドを0にする
            }
        }

        Life -= damage;// ライフを減らす

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
        Shield += num;// シールドを追加 上限がないため余計な処理はない

        if (playerHand != null) _rspShildToken.Push(playerHand);
    }

    #endregion

    #region Private Methods

    private void Init()// 初期化する関数
    {
        Life = LIFE_DEFAULT;
        Shield = 0;
    }

    #endregion
}
