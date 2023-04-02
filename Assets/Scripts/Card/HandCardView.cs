using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// 手札ビュー
/// </summary>
public class HandCardView : MonoBehaviour
{
    /// <summary>
    /// ドラッグ時のソーティングオーダー
    /// </summary>
    private static int s_dragOrder = 100;

    /// <summary>
    /// キャンバス
    /// </summary>
    Canvas _canvas = null;

    /// <summary>
    /// Observable対応イベントトリガー
    /// </summary>
    ObservableEventTrigger _trigger = null;

    /// <summary>
    /// ドラッグ開始時のポジション
    /// </summary>
    Vector3 _beginDragPosition = Vector3.zero;

    /// <summary>
    /// ドラッグ開始時のソーティングオーダー
    /// </summary>
    int _beginDragSortingOrder = 0;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _trigger = GetComponent<ObservableEventTrigger>();

        // ドラッグ開始
        _trigger.OnBeginDragAsObservable()
            .TakeUntilDestroy(gameObject)
            .Subscribe(OnBeginDrag);

        // ドラッグ中
        _trigger.OnDragAsObservable()
            .TakeUntilDestroy(gameObject)
            .Subscribe(OnDrag);

        // ドラッグ終了
        _trigger.OnEndDragAsObservable()
            .TakeUntilDestroy(gameObject)
            .Subscribe(OnEndDrag);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        // 位置・ソーティングオーダー記憶
        _beginDragPosition = transform.localPosition;
        _beginDragSortingOrder = _canvas.sortingOrder;

        // このカードが最前面に表示されるようにする
        _canvas.sortingOrder = s_dragOrder;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // TODO:
        // ドラッグされた位置にカードの座標を変える
        transform.localPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        // TODO:
        // 表示優先度を元に戻す
        _canvas.sortingOrder = _beginDragSortingOrder;

        // 技カード配置スペースの近くに置かれたら、使用カードが決まったことを通知
        // このビューは非表示にする
        // 手札の近くに置かれたら、その位置に応じて手札の順番を変える
        // それ以外の位置は、ドラッグ開始前の位置に戻す

        transform.localPosition = _beginDragPosition;
    }
}
