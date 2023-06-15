using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class NavigationFramework
{
    private static readonly Stack<SceneNode> _sceneNodeStack = new Stack<SceneNode>();
    public static SceneNode ActiveNode => _sceneNodeStack.Peek();
    public static SceneNode RootNode { get; private set; }

    public static void PresentViewNode(SceneNode node) 
    {
        if (_sceneNodeStack.Count==0) RootNode = node;
        _sceneNodeStack.Push(node);
        SceneManager.LoadScene(node.SceneName);
    }

    public static void RemoveViewNode() 
    {
        if (_sceneNodeStack.Count > 0) 
        {
            _sceneNodeStack.Pop();
            ActiveNode.RegisterLoad();
            SceneManager.LoadScene(ActiveNode.SceneName);
        }
        else
        {
            SceneManager.LoadScene(RootNode.SceneName);
        }
    }

    public static void ReloadViewNode() 
    {
        if (_sceneNodeStack.Count > 0)
        {
            ActiveNode.RegisterLoad();
            SceneManager.LoadScene(ActiveNode.SceneName);
        }
        else
        {
            SceneManager.LoadScene(RootNode.SceneName);
        }
    }

    public static void SetRootViewNode(SceneNode node) 
    {
        _sceneNodeStack.Clear();
        node.RegisterLoad();
        node.SceneWillShow();
        PresentViewNode(node);
    }

    public static void PopToRootViewNode() 
    {
        _sceneNodeStack.Clear();
        RootNode.SceneWillShow();
        RootNode.RegisterLoad();
        PresentViewNode(RootNode);
    }

}
