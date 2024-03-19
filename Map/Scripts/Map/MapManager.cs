using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{


    /// <summary>
    /// 씬 이름(+숫자)
    /// </summary>
    const string BaseSceneName = "Map";
    /// <summary>
    /// 씬 저장용 배열
    /// </summary>
    string[] sceneNames;

    /// <summary>
    /// 씬의 로딩상태를 나타내기 위한 enum
    /// </summary>
    enum SceneLoadState : byte
    {
        Unload =0, // 로딩이 안된 상태
        Loaded // 로딩이 된 상태

    }
    /// <summary>
    /// 씬들의 로딩 상태
    /// </summary>
    SceneLoadState[] sceneLoadState;

    [Tooltip("씬 로드,언로드상태 리스트")]
    List<int> loadedScene = new();  
    List<int> unloadedScene = new();


    MapGenerator Generater;


    public void Start() 
    {
        Generater = GetComponent<MapGenerator>();
        int mapCount = (int)Mathf.Pow(Generater.maximumDepth, 2);
        
        sceneNames = new string[mapCount];
        sceneLoadState = new SceneLoadState[mapCount];
        for(int i = 0; i < mapCount; i++)
        {
            int index = i;                      //일단은 중복 허용이지만 맵개수 늘리면 중복안되게 리스트추가해서 막아야함.
            int number = Random.Range(1, 5);          // <<--맵 전체개수 알아서 구하는 식도 찾아봐야함
            sceneNames[index] = $"{BaseSceneName}{number}";      //<- 맞는지확인필요
            sceneLoadState[index] = SceneLoadState.Unload;
        }
    }

}
