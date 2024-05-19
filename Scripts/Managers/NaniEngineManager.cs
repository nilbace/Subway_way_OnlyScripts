using Naninovel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaniEngineManager
{
    public Camera MainCamera;
    public IInputManager InputManager;
    public IStateManager StateManager;
    public IScriptPlayer ScriptPlayer;

    public void Init()
    {
        // Engine may not be initialized here, so check first.
        if (Engine.Initialized) InitProcess();
        else Engine.OnInitializationFinished += InitProcess;
    }

    private void InitProcess()
    {
        CacheEngineServices();
        
        //���� ī�޶� ���� ����
        MainCamera.clearFlags = CameraClearFlags.Depth;
    }

    /// <summary>
    /// ���� ���Ǵ� �������񽺵��� �̸� ĳ���ص�
    /// </summary>
    void CacheEngineServices()
    {
        MainCamera   = Engine.GetService<ICameraManager>().Camera;
        InputManager = Engine.GetService<IInputManager>();
        StateManager = Engine.GetService<IStateManager>();
        ScriptPlayer = Engine.GetService<IScriptPlayer>();
        //
    }
}

