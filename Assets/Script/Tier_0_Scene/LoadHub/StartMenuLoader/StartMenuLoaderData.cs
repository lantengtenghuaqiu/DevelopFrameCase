using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public sealed class SceneLoader : SingletonDoubleLock<SceneLoader>
{
    // Singleton Instance
    public static SceneLoader Instance()
    {
        {
            if (SingletonDoubleLock<SceneLoader>._instance == null)
            {
                lock (SingletonDoubleLock<SceneLoader>._locker)
                {
                    if (SingletonDoubleLock<SceneLoader>._instance == null)
                    {
                        SingletonDoubleLock<SceneLoader>._instance = new SceneLoader();
                    }
                }
            }
            return SingletonDoubleLock<SceneLoader>._instance;
        }
    }
    private SceneLoader() { name_stack_Scene = new Stack<string>(); }
    private readonly string[] name_tier_ = new string[2] { "Tier_0_Scene", "Tier_1_Scene" };
    public Stack<string> name_stack_Scene;
    public void LoadScene(string scene_name)
    {
        try
        {
            Instance().name_stack_Scene.Push(scene_name);
            SceneManager.LoadScene(scene_name, LoadSceneMode.Additive);
        }
        catch
        {
            SceneManager.LoadScene(0);
        }
    }
    public void UnloadScene()
    {
        if (Instance().name_stack_Scene != null && Instance().name_stack_Scene.Count > 1)
            SceneManager.UnloadSceneAsync(Instance().name_stack_Scene.Pop());
        else
        {
            Debug.Log("当前场景名称为空,卸载场景失败");
            throw new System.ArgumentException("当前场景名称为空,卸载场景失败:{name_CurrentActiveScene}");
        }
    }
}