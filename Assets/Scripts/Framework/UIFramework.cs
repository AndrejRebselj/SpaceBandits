using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public interface UIFramework
{
    public void InitWithGameObject(GameObject gameObject); 
}
public enum LabelMode
{
    Standard, TextMeshPro, None
}

public class MyButton : UIFramework
{
    public Button button;
    public MyLabel label;
    public GameObject gameObject;
    public Image buttonImage;

    public string Name { get; set; }

    public MyButton(string buttonName) 
    {
        var buttonObject = GameObject.Find(buttonName);
        if (buttonObject!= null)
        {
            InitWithGameObject(buttonObject);
        }
    }
    public void InitWithGameObject(GameObject buttonGameObject)
    {
        gameObject = buttonGameObject;
        button = buttonGameObject.GetComponent<Button>();
        buttonImage = buttonGameObject.GetComponent<Image>();
        if (button==null)
        {
            Debug.LogWarning("Object is not a button - " + buttonGameObject.name);
            return;
        }
        label = new MyLabel(buttonGameObject.transform.GetChild(0).gameObject);
        Name = buttonGameObject.name;
    }

    public void SetText(string text) 
    {
        label?.SetText(text);
    }
    public void OnClick(UnityAction action) 
    {
        button.onClick.AddListener(action);
    }
    public void SetImgOpacity(float opa) 
    {
        Color color = buttonImage.color;
        color.a = opa;
        buttonImage.color = color;
    }

    public Image GiveMeButtonImageObject()=>gameObject.GetComponent<Image>();


}
public class MyLabel : UIFramework
{
    public TextMeshProUGUI textMP;
    public Text text;
    public GameObject gameObject;

    public LabelMode Mode = LabelMode.None;

    public MyLabel(string labelName)
    {
        var labelObject = GameObject.Find(labelName);
        if (labelObject != null)
        {
            InitWithGameObject(labelObject);
        }
    }

    public MyLabel(GameObject gameObject)
    {
        InitWithGameObject(gameObject);
    }

    public void InitWithGameObject(GameObject labelObject)
    {
        gameObject = labelObject;

        textMP = labelObject.GetComponentInChildren<TextMeshProUGUI>();
        if (textMP != null)
        {
            Mode = LabelMode.TextMeshPro;
        }
        else
        {
            text = labelObject.GetComponentInChildren<Text>();
            if (text != null)
            {
                Mode = LabelMode.Standard;
            }
        }
    }
    public void SetText(string newText)
    {
        if (gameObject == null)
        {
            return;
        }

        switch (Mode)
        {
            case LabelMode.Standard:
                text.text = newText;
                break;
            case LabelMode.TextMeshPro:
                textMP.text = newText;
                break;
            default:
                break;
        }
    }
    public string Text()
    {
        switch (Mode)
        {
            case LabelMode.Standard:
                return text.text;
            case LabelMode.TextMeshPro:
                return textMP.text;
            default:
                return null;
        }
    }
}
public class UImage : UIFramework
{
    private Image image;
    public GameObject gameObject;

    public UImage(string imageName)
    {
        var imageObject = GameObject.Find(imageName);
        if (imageObject != null)
        {
            InitWithGameObject(imageObject);
        }
    }

    public UImage(GameObject gameObject)
    {
        InitWithGameObject(gameObject);
    }

    public void InitWithGameObject(GameObject imageObject)
    {
        gameObject = imageObject;

        image = imageObject.GetComponent<Image>();
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetColor(Color color)
    {
        image.color = color;
    }
}

public class MyAudio : UIFramework 
{
    private AudioSource audioSource;
    public GameObject audioGameObject;

    public MyAudio(string audioName)
    {
        var audioObject = GameObject.Find(audioName);
        if (audioObject != null)
        {
            InitWithGameObject(audioObject);
        }
    }

    public MyAudio(GameObject gameObject)
    {
        InitWithGameObject(gameObject);
    }

    public void InitWithGameObject(GameObject gameObject)
    {
        audioGameObject = gameObject;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void SetupAudioVolume(float volume) 
    {
        audioSource.volume = volume;
    }
}
