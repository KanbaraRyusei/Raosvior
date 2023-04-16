using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    #region Inspector Member

    [SerializeField]
    [Header("決定ボタン")]
    private Button _desideButton;

    [SerializeField]
    [Header("チョキ選択キャンセルボタン")]
    private Button _cancelButton;

    [SerializeField]
    [Header("シールド破壊数追加ボタン")]
    private Button _addBreakCountButton;

    [SerializeField]
    [Header("シールド破壊数削除ボタン")]
    private Button _removeBreakCountButton;

    [SerializeField]
    [Header("カードを戻すボタン")]
    private Button _cardBackButton;

    [SerializeField]
    [Header("カードを戻さないボタン")]
    private Button _notCardBackButton;

    [SerializeField]
    [Header("カードを選択するボタンのプレファブ")]
    private Button _rspHandButtonPrefab;

    [SerializeField]
    [Header("カードを選択するボタンを格納するリスト")]
    private Button[] _handButtons = new Button[4];

    [SerializeField]
    [Header("カードを選択するボタンを格納する親オブジェクト")]
    private Transform _handButtonParent;

    [SerializeField]
    [Header("エネミーのカードの裏面の絵")]
    private Sprite _enemyHandSprite;

    #endregion

    #region Private Member

    private const int MAX_HAND_COUNT = 4;

    #endregion

    #region Unity Method

    private void Awake()
    {
        Generate();
    }

    #endregion

    #region HandSelectPhase

    /// <summary>
    /// 手札選択フェーズの手札を表示する関数
    /// </summary>
    public void SelectAllHand()
    {

    }

    #endregion

    #region CardSelectPhase

    /// <summary>
    /// 選択できるカードを表示する関数
    /// </summary>
    public void SelectRSPCard(IReadOnlyList<RSPPlayerHand> playerHands)
    {

    }

    #endregion

    #region LeaderCardShaman

    /// <summary>
    /// 選択できるチョキのカードを表示する関数
    /// </summary>
    /// <param name="scissorsHands"></param>
    public void SelectCardForLeaderCardShaman
        (IReadOnlyList<UnityAction> methods, UnityAction cancelMethod, 
        UnityAction decideMethod, IEnumerable<RSPPlayerHand> scissorsHands)
    {
        foreach (var method in methods)
        {
            foreach (var sprite in scissorsHands)
            {
                foreach (var hand in _handButtons)
                {
                    if (hand.gameObject.activeSelf == false)
                    {
                        hand.transform.SetParent(_handButtonParent);
                        hand.onClick.AddListener(method);
                        hand.image.sprite = sprite.CardSprite;
                        hand.gameObject.SetActive(true);
                        break;
                    }
                }
                break;
            }
        }

        _cancelButton.onClick.AddListener(cancelMethod);
        _desideButton.onClick.AddListener(decideMethod);

        _cancelButton.gameObject.SetActive(true);
        _desideButton.gameObject.SetActive(true);
    }

    public void InactiveScissorsHandButton
        (IReadOnlyList<UnityAction> methods, UnityAction cancelMethod, UnityAction decideMethod)
    {
        foreach (var method in methods)
        {
            foreach (var button in _handButtons)
            {
                if (button.gameObject.activeSelf == true)
                {
                    button.transform.SetParent(transform);
                    button.onClick.RemoveListener(method);
                    button.gameObject.SetActive(false);
                    break;
                }
            }
        }

        _cancelButton.onClick.RemoveListener(cancelMethod);
        _desideButton.onClick.RemoveListener(decideMethod);

        _cancelButton.gameObject.SetActive(false);
        _desideButton.gameObject.SetActive(false);
    }

    #endregion

    #region FPaperCardJudgmentOfAigis

    /// <summary>
    /// 自身のシールド数を表示する関数
    /// </summary>
    /// <param name="shildCount"></param>
    public void SelectCardForFPaperCardJudgmentOfAigis
        (int shildCount,UnityAction addMethod, UnityAction removeMethod, UnityAction desideMethod)
    {
        _addBreakCountButton.onClick.AddListener(addMethod);
        _removeBreakCountButton.onClick.AddListener(removeMethod);
        _desideButton.onClick.AddListener(desideMethod);

        _addBreakCountButton.gameObject.SetActive(true);
        _removeBreakCountButton.gameObject.SetActive(true);
        _desideButton.gameObject.SetActive(true);
    }

    public void InactiveBreakCountButton
        (UnityAction addMethod, UnityAction removeMethod, UnityAction desideMethod)
    {
        _addBreakCountButton.onClick.RemoveListener(addMethod);
        _removeBreakCountButton.onClick.RemoveListener(removeMethod);
        _desideButton.onClick.RemoveListener(desideMethod);

        _addBreakCountButton.gameObject.SetActive(false);
        _removeBreakCountButton.gameObject.SetActive(false);
        _desideButton.gameObject.SetActive(false);
    }

    #endregion

    #region PaperCardDrainShield

    /// <summary>
    /// 敵のカードを裏向きで表示する関数
    /// </summary>
    /// <param name="enemyHands"></param>
    public void SelectCardForPaperCardDrainShield
        (IReadOnlyList<UnityAction> methods, UnityAction decideMethod)
    {
        foreach (var method in methods)
        {
            foreach (var hand in _handButtons)
            {
                if(hand.gameObject.activeSelf == false)
                {
                    hand.transform.SetParent(_handButtonParent);
                    hand.onClick.AddListener(method);
                    hand.image.sprite = _enemyHandSprite;
                    hand.gameObject.SetActive(true);
                    break;
                }
            }
        }

        _desideButton.onClick.AddListener(decideMethod);
        _desideButton.gameObject.SetActive(true);
    }

    public void InactiveEnemyHandButton
        (IReadOnlyList<UnityAction> methods, UnityAction decideMethod)
    {
        foreach (var method in methods)
        {
            foreach (var button in _handButtons)
            {
                if (button.gameObject.activeSelf == true)
                {
                    button.transform.SetParent(transform);
                    button.onClick.RemoveListener(method);
                    button.gameObject.SetActive(false);
                    break;
                }
            }
        }

        _desideButton.onClick.RemoveListener(decideMethod);
        _desideButton.gameObject.SetActive(false);
    }

    #endregion

    #region ScissorsCardChainAx

    /// <summary>
    /// カードを戻せるかどうかの選択ボタンを表示する関数
    /// </summary>
    public void SelectCardForScissorsCardChainAx(UnityAction cardBackMethod, UnityAction notCardBackMethod)
    {
        _cardBackButton.onClick.AddListener(cardBackMethod);
        _notCardBackButton.onClick.AddListener(notCardBackMethod);

        _cardBackButton.gameObject.SetActive(true);
        _notCardBackButton.gameObject.SetActive(true);
    }

    public void InactiveCardBackButton(UnityAction cardBackMethod, UnityAction notCardBackMethod)
    {
        _cardBackButton.gameObject.SetActive(false);
        _notCardBackButton.gameObject.SetActive(false);

        _cardBackButton.onClick.RemoveListener(cardBackMethod);
        _notCardBackButton.onClick.RemoveListener(notCardBackMethod);
    }

    #endregion

    #region Private Method

    private void Generate()
    {
        for (int i = 0; i < MAX_HAND_COUNT; i++)
        {
            var enemyHand = Instantiate(_rspHandButtonPrefab);
            enemyHand.gameObject.SetActive(false);
            enemyHand.transform.SetParent(transform);
            _handButtons[i] = enemyHand;
        }
    }

    #endregion
}
