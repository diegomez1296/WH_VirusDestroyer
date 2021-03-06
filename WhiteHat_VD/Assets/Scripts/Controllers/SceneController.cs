﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void LoadScene(bool rootScene, string sceneName, Action nextMethod, bool wasStart)
    {
        if (rootScene)
            SceneManager.LoadScene("_RootScene");

        StartCoroutine(LoadSceneAsynchronously(rootScene, sceneName, nextMethod, wasStart));
        
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void UnloadAllScenes(bool rootScene)
    {
        Scene[] scenes = SceneManager.GetAllScenes();

        if (rootScene)
        {
            SceneManager.UnloadSceneAsync(0);
            SceneManager.UnloadSceneAsync(1); // UI
        }

        for (int i = 2; i < scenes.Length; i++)
        {
            SceneManager.UnloadSceneAsync(scenes[i]);
        }
    }

    public void LoadUIScene()
    {
        StartCoroutine(CoLoadUIScene());
    }

    private IEnumerator CoLoadUIScene()
    {
        yield return null;
        SceneManager.LoadScene("_UI", LoadSceneMode.Additive);
    }

    private IEnumerator LoadSceneAsynchronously (bool uiscene, string sceneName, Action nextMethod, bool wasStart)
    {
        if(uiscene)
            yield return StartCoroutine(CoLoadUIScene());
        HeadPanelController.Instance.loadingScreen.SetSliderValue(0);
        HeadPanelController.Instance.loadingScreen.ChangeVisibility(true);
        yield return new WaitForSeconds(0.3f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            HeadPanelController.Instance.loadingScreen.SetSliderValue(operation.progress);
            yield return new WaitForEndOfFrame();
        }
        HeadPanelController.Instance.loadingScreen.SetSliderValue(1);
        yield return new WaitForSeconds(0.3f);
        nextMethod();

        if(!wasStart)
            yield return new WaitForSeconds(2.5f);

        HeadPanelController.Instance.loadingScreen.ChangeVisibility(false);
        HeadPanelController.Instance.startPanel.ChangeVisibility(false);
    }

    public bool IsSceneLoaded(string name)
    {
        return SceneManager.GetSceneByName(name).isLoaded;
    }

}
