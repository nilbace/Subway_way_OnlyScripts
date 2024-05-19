using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class DataManager : MonoBehaviour
{
    NowStoryName _nowStory;
    public NowStoryName NowStory { get { return _nowStory; } 
        set 
        { 
            _nowStory = value;
            if (HomeScreen.Inst.isActiveAndEnabled) HomeScreen.Inst.UpdateUIs();
        } 
    }
    
    [ContextMenu("���丮 ó������ ������")]
    void ResetStory()
    {
        NowStory = NowStoryName.ù��°_�;
    }
}
