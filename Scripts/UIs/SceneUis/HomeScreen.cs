using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Naninovel;

public class HomeScreen : MonoBehaviour
{
    public static HomeScreen Inst;
    public TMP_Text TMP_NowStory;

    private void Awake()
    {
        if (Inst == null) Inst = this;
        else Destroy(this);
    }
   
    
    [ContextMenu("������Ʈ ����")]
    public void UpdateUIs()
    {
        TMP_NowStory.text = GameManager.Data.NowStory.ToString().UnderscoreToSpace();
    }

    public async void Transfer()
    {
        string ScriptName = GameManager.Data.NowStory.ToString();
    
        // 2. Switch cameras.
        var naniCamera = Engine.GetService<ICameraManager>().Camera;
        naniCamera.enabled = true;

        // 3. Load and play specified script (if assigned).
        await GameManager.Nani.ScriptPlayer.PreloadAndPlayAsync(ScriptName);

        // 4. Enable Naninovel input.
        GameManager.Nani.InputManager.ProcessInput = true;

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        UpdateUIs();
    }
}
