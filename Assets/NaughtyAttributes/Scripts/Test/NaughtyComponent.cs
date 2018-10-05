using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using Sourav.Utilities.Scripts.Algorithms.Shuffle;

public class NaughtyComponent : MonoBehaviour
{
    [Header("myInt")]
    [ShowIf("showMyInt")]
    [EnableIf("enableMyInt")]
    public int myInt;
    public bool showMyInt = true;
    public bool enableMyInt = true;

    [Header("myFloat")]
    [HideIf("HideMyFloat")]
    [DisableIf("DisabledMyFloat")]
    public float myFloat;
    public bool hideMyFloat = false;
    public bool disableMyFloat = false;

    [Header("sprite")]
    [ShowAssetPreview]
    public Sprite sprite;

    [Header("zero")]
    [ReadOnly]
    public Vector3 zero = Vector3.zero;

    private int[] values = new int[] { 1, 2, 3, 4 };

    [Header("dropdown")]
    [Dropdown("values")]
    public int dropdown;

    private string[] strings = new string[] { "One", "Two", "Three", "Four", "Five"};

    [Header("dropdown")]
    [Dropdown("strings")]
    public string dropdown2;

    [Header("minMaxSlider")]
    [MinMaxSlider(10, 100)]
    public Vector2 minMaxSlider;

    [Header("slider")]
    [Slider(0, 10)]
    public int slider;

    [Header("health")]
    [ProgressBar]
    public float health = 10;

    [Header("list")]
    [ReorderableList]
    public float[] list;

    [ReorderableList]
    public List<string> stringList;

    //[Header("textArea")]
    //[ResizableTextArea]
    //public string textArea;

    private bool HideMyFloat()
    {
        return this.hideMyFloat;
    }

    private bool DisabledMyFloat()
    {
        return this.disableMyFloat;
    }

    [Button()]
    public void PrintValuesInList()
    {
        for (int i = 0; i < list.Length; i++)
        {
            Debug.Log(i+" : "+list[i]);
        }
    }

    [Button()]
    public void ShuffleThis()
    {
        Shuffle<string>(ref stringList);
    }

    [Button()]
    public void ShuffleFloat()
    {
        Shuffle<float>(ref list);
    }

    private void Shuffle<T>(ref T[] iArray)
    {
        FisherYatesShuffle shuffle = new FisherYatesShuffle(iArray.Length, 0);
        shuffle.ShuffleList();
        List<int> shuffleList = shuffle.ShuffledList;
        List<T> shuffledList = new List<T>();

        for (int i = 0; i < shuffleList.Count; i++)
        {
            shuffledList.Add(iArray[shuffleList[i]]);
        }
        iArray = shuffledList.ToArray();
    }

    private void Shuffle<T>(ref List<T> ilist)
    {
        FisherYatesShuffle shuffle = new FisherYatesShuffle(ilist.Count, 0);
        shuffle.ShuffleList();
        List<int> shuffleList = shuffle.ShuffledList;
        List<T> shuffledList = new List<T>();

        for (int i = 0; i < shuffleList.Count; i++)
        {
            shuffledList.Add(ilist[shuffleList[i]]);
        }
        ilist = shuffledList;
    }
}
