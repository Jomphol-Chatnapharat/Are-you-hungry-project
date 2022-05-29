using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static UnityEvent OnPaused, OnResumed;

    private bool m_isPause;
    private Image m_image;

    public void BackToMainMenu()
    {
        foreach(GameObject go in GetDontDestroyOnLoadObjects())
        {
            Destroy(go);
        }

        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        OnResumed.Invoke();

        m_image.enabled = false;

        m_isPause = false;

        Time.timeScale = 1;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        OnPaused = new UnityEvent();
        OnResumed = new UnityEvent();

        m_image = GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !m_isPause)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        m_image.enabled = true;

        OnPaused.Invoke();

        m_isPause = true; 

        Time.timeScale = 0;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public static List<GameObject> GetDontDestroyOnLoadObjects()
    {
        List<GameObject> result = new List<GameObject>();

        List<GameObject> rootGameObjectsExceptDontDestroyOnLoad = new List<GameObject>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            rootGameObjectsExceptDontDestroyOnLoad.AddRange(SceneManager.GetSceneAt(i).GetRootGameObjects());
        }

        List<GameObject> rootGameObjects = new List<GameObject>();
        Transform[] allTransforms = Resources.FindObjectsOfTypeAll<Transform>();
        for (int i = 0; i < allTransforms.Length; i++)
        {
            Transform root = allTransforms[i].root;
            if (root.hideFlags == HideFlags.None && !rootGameObjects.Contains(root.gameObject))
            {
                rootGameObjects.Add(root.gameObject);
            }
        }

        for (int i = 0; i < rootGameObjects.Count; i++)
        {
            if (!rootGameObjectsExceptDontDestroyOnLoad.Contains(rootGameObjects[i]))
                result.Add(rootGameObjects[i]);
        }

        //foreach( GameObject obj in result )
        //    Debug.Log( obj );

        return result;
    }
}
