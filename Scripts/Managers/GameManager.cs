using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager s_instance;

    public static GameManager Inst { get { Init(); return s_instance; } }

    NaniEngineManager _nani = new NaniEngineManager();
    //ResourceManager _resource = new ResourceManager();
    //UI_Manager _ui_manager = new UI_Manager();
    //SoundManager _sound = new SoundManager();
    DataManager _data = new DataManager();
    //NicknameManager _nickname = new NicknameManager();
    //SceneMManager _scene = new SceneMManager();

    public static NaniEngineManager Nani { get { return Inst._nani; } }
    //public static ResourceManager Resource { get { return instance._resource; } }
    //public static UI_Manager UI { get { return instance._ui_manager; } }
    //public static SoundManager Sound { get { return instance._sound; } }
    public static DataManager Data { get { return Inst._data; } }
    //public static NicknameManager NickName { get { return instance._nickname; } }
    //public static SceneMManager Scene { get { return instance._scene; } }

    void Awake()
    {
        Init();
    }


    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@GameManager");
            if (go == null)
            {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<GameManager>();

            s_instance._nani.Init();
            //s_instance._data.Init();
            //s_instance._scene.Init();
        }
    }
}
