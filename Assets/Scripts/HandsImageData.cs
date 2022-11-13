using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandsImageData : MonoBehaviour
{
    public Image[] HandsImage => _handsImage;

    [SerializeField]
    Image[] _handsImage;
}
