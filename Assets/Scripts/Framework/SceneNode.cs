using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ISceneFramework
{
    string SceneName { get; set; }
    bool IsLoaded { get; set; }
    bool IsRegisteredLoad { get; set; }
    SceneNode ParentSceneNode { get; }
}
public class SceneNode : ISceneFramework
{
    public string SceneName { get; set; }
    public bool IsLoaded { get; set; }
    public bool IsRegisteredLoad { get; set; }

    public SceneNode ParentSceneNode { get; set;}
    private List<SceneNode> ChildSceneNodes = new List<SceneNode>();

    public SceneNode(string sceneName) 
    {
        SceneName = sceneName;
    }
    private void RegisterOnLoadCompete(Scene scene, LoadSceneMode mode) 
    {
        if (scene.name==SceneName)
        {
            IsLoaded = true;
            SceneDidLoad();
            UnregisterLoad();
        }
    }
    public void PushSceneNode(SceneNode node) 
    {
        node.RegisterLoad();
        node.SceneWillShow();

        UnregisterLoad();
        SceneBeGone();

        node.ParentSceneNode= this;

        NavigationFramework.PresentViewNode(node);
        
    }

    public void RegisterLoad()
    {
        if (IsRegisteredLoad) return;
        IsRegisteredLoad = true;
        SceneManager.sceneLoaded += RegisterOnLoadCompete;
    }

    public void UnregisterLoad()
    {
        if (!IsRegisteredLoad) return;
        IsRegisteredLoad = false;
        SceneManager.sceneLoaded -= RegisterOnLoadCompete;
    }

    public void AddChildToSceneLoader(SceneNode node) 
    {
        node.RegisterLoad();
        node.SceneWillShow();
        node.ParentSceneNode= this;
        ChildSceneNodes.Add(node);
        SceneManager.LoadSceneAsync(node.SceneName, LoadSceneMode.Additive);
    }

    public void RemoveChildNodeFromParent() 
    {
        ParentSceneNode.RemoveChildNode(this);
    }

    public void RemoveChildNode(SceneNode node)
    {
        ChildSceneNodes.Remove(node);
        node.ParentSceneNode = null;
        node.SceneBeGone();
        SceneManager.UnloadSceneAsync(node.SceneName);
    }

    public void PopToParentSceneNode() 
    {
        if (NavigationFramework.ActiveNode==this)
        {
            NavigationFramework.RemoveViewNode();
        }
    }

    public virtual void SceneDidLoad()
    {
        //override
    }
    public virtual void SceneWillShow()
    {
        //override
    }
    public virtual void SceneBeGone()
    {
        //override
    }
}


