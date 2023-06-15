using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Scenes
{
    GameScene1,
    GameScene2,
    GameScene3,
    MainMenu

}
public struct SceneNames
{
    public static string MainMenu = "MainMenu";
    public static string GameScene1 = "GameScene01";
    public static string GameScene2 = "GameScene02";
    public static string GameScene3 = "GameScene03";
}
public class MainController : MonoBehaviour
{
    void Start()
    {
        PrefabVariables.LoadValues();
        NavigationFramework.SetRootViewNode(new MainMenuController());
    }
}

public class MainMenuController : SceneNode
{
    GameObject mainMenu;
    GameObject storeMenu;
    GameObject settingsMenu;
    GameObject gameStartMenu;
    GameObject gemStoreMenu;
    MyLabel titleLabel;
    MyAudio backgroundMusic;
    MyLabel gameStartcurrency;
    public MainMenuController() : base(SceneNames.MainMenu)
    {
    }

    public override void SceneDidLoad() 
    {
        base.SceneDidLoad();
        mainMenu = GameObject.Find("MainMenuObject");
        storeMenu = GameObject.Find("StoreMenuObject");
        settingsMenu = GameObject.Find("SettingsMenuObject");
        gameStartMenu = GameObject.Find("GameStartObject");
        gemStoreMenu = GameObject.Find("GemStoreObject");
        //Audio
        backgroundMusic = new MyAudio("BackgroundAudio");
        backgroundMusic.SetupAudioVolume(PlayerPrefs.GetFloat("AudioVolume",1));
        //Title
        titleLabel = new MyLabel("MenuTitle");
        SetupMainMenu();
        SetupStoreMenu();
        SetupGameStartMenu();
        SetupSettingsMenu();
        SetupGemStoreMenu();
        ActivateMainMenu();

    }

    private void SetupGemStoreMenu()
    {
        //5EuroButton
        MyButton fiveEuroButton = new MyButton("5EuroButton");
        fiveEuroButton.SetText("5 euro for 500 gold");
        //10Euro button
        MyButton tenEuroButton = new MyButton("10EuroButton");
        tenEuroButton.SetText("10 euro for 1100 gold");
        //20Euro button
        MyButton twentyEuroBUtton = new MyButton("20EuroButton");
        twentyEuroBUtton.SetText("20 euro for 2500 gold");
        //Quit gem store button
        MyButton quitButton = new MyButton("QuitGemStoreButton");
        quitButton.SetText("Return");
        quitButton.OnClick(() =>
        {
            ActivateStoreMenu();
        });
    }

    private void SetupGameStartMenu()
    {
        gameStartcurrency = new MyLabel("CurrecnyGameStart");
        gameStartcurrency.SetText($"{PrefabVariables.GoldCurrency} gold");
        MyButton back = new MyButton("BackToMenuFromGameStart");
        MyButton start = new MyButton("StartGameButton");
        MyButton ad = new MyButton("AdButton");
        ad.SetText("Get 1000 gold");
        back.SetText("Back");
        start.SetText("Start");
        back.OnClick(() => {
            ActivateMainMenu();
        });
        start.OnClick(() => {
            switch (StartGameData.PickedLevel) 
            {
                case 1:
                    PushSceneNode(SceneProvider.GetSceneNode(Scenes.GameScene1));
                    break;
                case 2:
                    PushSceneNode(SceneProvider.GetSceneNode(Scenes.GameScene2));
                    break;
                case 3:
                    PushSceneNode(SceneProvider.GetSceneNode(Scenes.GameScene3));
                    break;
                default:
                    PushSceneNode(SceneProvider.GetSceneNode(Scenes.GameScene1));
                    break;
            }
            
        });
        //Setup level select
        MyButton level1 = new MyButton("Level1");
        MyButton level2 = new MyButton("Level2");
        MyButton level3 = new MyButton("Level3");
        MyLabel weapon1Label = new MyLabel("WeaponItem01Text");
        MyLabel weapon2Label = new MyLabel("WeaponItem02Text");
        switch (PrefabVariables.Weapon1Level) 
        {
            case 1:
                weapon1Label.SetText("5000");
                break;
            case 2:
                weapon1Label.SetText("10000");
                break;
            case 3:
                weapon1Label.SetText("Max");
                break;
        }
        switch (PrefabVariables.Weapon2Level)
        {
            case 1:
                weapon2Label.SetText("7000");
                break;
            case 2:
                weapon2Label.SetText("12000");
                break;
            case 3:
                weapon2Label.SetText("Max");
                break;
        }
        level1.OnClick(() => {
            StartGameData.PickedLevel = 1;
            level1.GiveMeButtonImageObject().color = Color.green;
            level2.GiveMeButtonImageObject().color = Color.grey;
            level3.GiveMeButtonImageObject().color = Color.grey;
        });
        level2.OnClick(() => {
            StartGameData.PickedLevel = 2;
            level1.GiveMeButtonImageObject().color = Color.gray;
            level2.GiveMeButtonImageObject().color = Color.green;
            level3.GiveMeButtonImageObject().color = Color.grey;
        });
        level3.OnClick(() => {
            StartGameData.PickedLevel = 3;
            level1.GiveMeButtonImageObject().color = Color.gray;
            level2.GiveMeButtonImageObject().color = Color.grey;
            level3.GiveMeButtonImageObject().color = Color.green;
        });
        //Setup weapon pick and levelup
        MyButton weapon01 = new MyButton("WeaponItem01");
        MyButton weapon02 = new MyButton("WeaponItem02");
        weapon01.OnClick(() => {
            if(StartGameData.PickedWeapon!=1)
            {
                StartGameData.PickedWeapon = 1;
                weapon01.GiveMeButtonImageObject().color=Color.green;
                weapon02.GiveMeButtonImageObject().color=Color.gray;
            }
            else if (StartGameData.PickedWeapon==1)
            {
                switch (PrefabVariables.Weapon1Level)
                {
                    case 1:
                        if (PrefabVariables.GoldCurrency>=5000)
                        {
                            PrefabVariables.GoldCurrency -= 50000; 
                            PrefabVariables.Weapon1Level = 2;
                            weapon1Label.SetText("10000");
                            gameStartcurrency.SetText($"{PrefabVariables.GoldCurrency} gold");
                        }
                        break;
                    case 2:
                        if (PrefabVariables.GoldCurrency >= 10000)
                        {
                            PrefabVariables.GoldCurrency -= 100000;
                            PrefabVariables.Weapon1Level = 3;
                            gameStartcurrency.SetText($"{PrefabVariables.GoldCurrency} gold");
                            weapon1Label.SetText("Max");
                        }
                        break;
                }
            }
        });
        weapon02.OnClick(() => {
            if (StartGameData.PickedWeapon != 2)
            {
                StartGameData.PickedWeapon = 2;
                weapon02.GiveMeButtonImageObject().color = Color.green;
                weapon01.GiveMeButtonImageObject().color = Color.gray;
            }
            else if (StartGameData.PickedWeapon == 2)
            {
                switch (PrefabVariables.Weapon2Level)
                {
                    case 1:
                        if (PrefabVariables.GoldCurrency >= 7000)
                        {
                            PrefabVariables.GoldCurrency -= 70000;
                            PrefabVariables.Weapon2Level = 2;
                            weapon2Label.SetText("12000");
                            gameStartcurrency.SetText($"{PrefabVariables.GoldCurrency} gold");
                        }
                        break;
                    case 2:
                        if (PrefabVariables.GoldCurrency >= 12000)
                        {
                            PrefabVariables.GoldCurrency -= 120000;
                            PrefabVariables.Weapon2Level = 3;
                            weapon2Label.SetText("Max");
                            gameStartcurrency.SetText($"{PrefabVariables.GoldCurrency} gold");
                        }
                        break;
                }
            }
        });
    }
    private void ActivateGemShopMenu()
    {
        titleLabel.SetText("Gems");
        gemStoreMenu.SetActive(true);
        mainMenu.SetActive(false);
        storeMenu.SetActive(false);
        settingsMenu.SetActive(false);
        gameStartMenu.SetActive(false);

    }

    private void ActivateGameStartMenu()
    {
        titleLabel.SetText("Level");
        gameStartcurrency.SetText($"{PrefabVariables.GoldCurrency} gold");
        mainMenu.SetActive(false);
        storeMenu.SetActive(false);
        settingsMenu.SetActive(false);
        gameStartMenu.SetActive(true);
        gemStoreMenu.SetActive(false);

    }
    private void ActivateSettingsMenu()
    {
        titleLabel.SetText("Settings");
        mainMenu.SetActive(false);
        storeMenu.SetActive(false);
        gameStartMenu.SetActive(false);
        settingsMenu.SetActive(true);
        gemStoreMenu.SetActive(false);
    }

    private void ActivateStoreMenu()
    {
        titleLabel.SetText("Store");
        mainMenu.SetActive(false);
        storeMenu.SetActive(true);
        settingsMenu.SetActive(false);
        gameStartMenu.SetActive(false);
        gemStoreMenu.SetActive(false);
    }

    private void ActivateMainMenu()
    {
        titleLabel.SetText("Space Bandits");
        mainMenu.SetActive(true);
        storeMenu.SetActive(false);
        settingsMenu.SetActive(false);
        gameStartMenu.SetActive(false);
        gemStoreMenu.SetActive(false);
    }

    private void SetupStoreMenu()
    {
        MyLabel currency = new MyLabel("Currecny");
        currency.SetText($"{PrefabVariables.GoldCurrency} gold");
        MyLabel gems = new MyLabel("Gem");
        gems.SetText($"{PrefabVariables.GemCurrency} gems");
        MyButton addGemButton = new MyButton("AddGemButton");
        addGemButton.OnClick(() => {
            ActivateGemShopMenu();
        });
        MyButton backToMenuFromStore = new MyButton("BackToMenuFromStore");
        backToMenuFromStore.SetText("Back");
        backToMenuFromStore.OnClick(() => {
            PrefabVariables.SaveValues();
            ActivateMainMenu();
        });
        //ShopItems
        MyButton shipItem01 = new MyButton("ShipItem01");
        MyButton shipItem02 = new MyButton("ShipItem02");
        shipItem01.GiveMeButtonImageObject().color = TranslatePlayerPrefabIntoColor(0);
        shipItem02.GiveMeButtonImageObject().color = TranslatePlayerPrefabIntoColor(1);
        shipItem01.OnClick(() => {
            if(PrefabVariables.PickedShip!=0)
            {
                shipItem01.GiveMeButtonImageObject().color = Color.green;
                shipItem02.GiveMeButtonImageObject().color = Color.yellow;
                PrefabVariables.PickedShip = 0;
            }
        });
        shipItem02.OnClick(() => {
            if (PrefabVariables.GoldCurrency >= 5000 && PrefabVariables.ownedShips[1] == "n")
            {
                PrefabVariables.GoldCurrency -= 5000;
                shipItem02.GiveMeButtonImageObject().color = Color.yellow;
                PrefabVariables.ownedShips[1] = "y";
                currency.SetText($"{PrefabVariables.GoldCurrency}");
            }
            else if (PrefabVariables.ownedShips[1]=="y") 
            {
                shipItem02.GiveMeButtonImageObject().color= Color.green;
                shipItem01.GiveMeButtonImageObject().color = Color.yellow;
                PrefabVariables.PickedShip = 1;
            }
        });
    }

    private Color TranslatePlayerPrefabIntoColor(int ship)
    {
        if (PrefabVariables.PickedShip == ship)
        {
            return Color.green;
        }
        else if (PrefabVariables.ownedShips[ship] == "y")
        {
            return Color.yellow;
        }
        else 
        {
            return Color.red;
        }
    }

    private void SetupSettingsMenu()
    {
        MyLabel sliderTitle = new MyLabel("SliderTitle");
        sliderTitle.SetText("Volume");
        //SoundSlider
        GameObject slider = GameObject.Find("SoundSlider");
        Slider volume = slider.GetComponent<Slider>();
        volume.onValueChanged.AddListener(delegate { SoundVolumeChanged(volume.value);});
        //Controller button
        MyButton controllerButton = new MyButton("ControllsSettings");
        controllerButton.SetText("Touch controller");
        if (PlayerPrefs.GetInt("UseSensors", 0) == 0)
        {
            controllerButton.SetText("Touch controller: ON");
        }
        else
        {
            controllerButton.SetText("Touch controller: OFF");
        }
        controllerButton.OnClick(() =>
        {
            if (PlayerPrefs.GetInt("UseSensors", 0) == 0)
            {
                PlayerPrefs.SetInt("UseSensors", 1);
                controllerButton.SetText("Touch controller: OFF");
            }
            else 
            {
                PlayerPrefs.SetInt("UseSensors", 0);
                controllerButton.SetText("Touch controller: ON");
            }
        });
        //Back button
        MyButton backBUtton = new MyButton("BackToMenuFromSettings");
        backBUtton.SetText("Back");
        backBUtton.OnClick(() =>
        {
            ActivateMainMenu();
        });
    }

    private void SoundVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("AudioVolume", value);
        backgroundMusic.SetupAudioVolume(value);
    }

    private void SetupMainMenu()
    {
        //StartButton
        MyButton playButton = new MyButton("StartButton");
        playButton.SetText("Start");
        playButton.OnClick(() => {
            ActivateGameStartMenu();
            //PushSceneNode(SceneProvider.GetSceneNode(Scenes.GameScene1));
        });
        //Store button
        MyButton storeButton = new MyButton("StoreButton");
        storeButton.SetText("Store");
        storeButton.OnClick(() =>
        {
            ActivateStoreMenu();
        });
        //Settings button
        MyButton settingsBUtton = new MyButton("SettingsButton");
        settingsBUtton.SetText("Options");
        settingsBUtton.OnClick(() =>
        {
            ActivateSettingsMenu();
        });
        //Quit button
        MyButton quitButton = new MyButton("QuitButton");
        quitButton.SetText("Quit");
        quitButton.OnClick(() =>
        {
            PrefabVariables.SaveValues();
            Application.Quit();
        });
    }
}

public class GameScene1 : SceneNode 
{
    GameObject pauseMenu;
    GameObject deathMenu;
    public GameScene1() : base(SceneNames.GameScene1) { }

    public override void SceneDidLoad()
    {
        base.SceneDidLoad();
        Screen.autorotateToPortrait = true;
        pauseMenu = GameObject.Find("PauseMenu");
        deathMenu = GameObject.Find("DeathMenu");
        MyAudio backgroundAudio = new MyAudio("BackgroundAudio");
        backgroundAudio.SetupAudioVolume(PlayerPrefs.GetFloat("AudioVolume"));
        MyButton resumeButton = new MyButton("ResumeButton");
        resumeButton.SetText("Resume");
        resumeButton.OnClick(() => {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        });
        MyButton quitButton = new MyButton("QuitButton");
        quitButton.SetText("Quit");
        quitButton.OnClick(() => {             
            Time.timeScale = 1;
            PrefabVariables.SaveValues();
            NavigationFramework.RemoveViewNode();
        });
        MyButton pauseButton = new MyButton("PauseButton");
        pauseButton.OnClick(() => {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        });
        pauseMenu.SetActive(false);

        MyLabel deathText = new MyLabel("DeathText");
        deathText.SetText("Death");
        MyButton quitAfterDeathButton = new MyButton("QuitAfterDeathButton");
        quitAfterDeathButton.SetText("Quit");
        quitAfterDeathButton.OnClick(() => {
            Time.timeScale = 1;
            PrefabVariables.SaveValues();
            NavigationFramework.RemoveViewNode();
        });
        deathMenu.SetActive(false);


    }
    public override void SceneBeGone()
    {
        base.SceneBeGone();
        PrefabVariables.SaveValues();
    }
}
public class GameScene2 : SceneNode
{
    GameObject pauseMenu;
    GameObject deathMenu;
    public GameScene2() : base(SceneNames.GameScene2){}

    public override void SceneDidLoad()
    {
        base.SceneDidLoad();
        Screen.autorotateToPortrait = true;
        pauseMenu = GameObject.Find("PauseMenu");
        deathMenu = GameObject.Find("DeathMenu");
        MyAudio backgroundAudio = new MyAudio("BackgroundAudio");
        backgroundAudio.SetupAudioVolume(PlayerPrefs.GetFloat("AudioVolume"));
        MyButton resumeButton = new MyButton("ResumeButton");
        resumeButton.SetText("Resume");
        resumeButton.OnClick(() => {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        });
        MyButton quitButton = new MyButton("QuitButton");
        quitButton.SetText("Quit");
        quitButton.OnClick(() => {
            Time.timeScale = 1;
            PrefabVariables.SaveValues();
            NavigationFramework.RemoveViewNode();
        });
        MyButton pauseButton = new MyButton("PauseButton");
        pauseButton.OnClick(() => {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        });
        pauseMenu.SetActive(false);

        MyLabel deathText = new MyLabel("DeathText");
        deathText.SetText("Death");
        MyButton quitAfterDeathButton = new MyButton("QuitAfterDeathButton");
        quitAfterDeathButton.SetText("Quit");
        quitAfterDeathButton.OnClick(() => {
            Time.timeScale = 1;
            PrefabVariables.SaveValues();
            NavigationFramework.RemoveViewNode();
        });
        deathMenu.SetActive(false);
    }
    public override void SceneBeGone()
    {
        base.SceneBeGone();
        PrefabVariables.SaveValues();
    }
}
public class GameScene3 : SceneNode
{
    GameObject pauseMenu;
    GameObject deathMenu;
    public GameScene3() : base(SceneNames.GameScene3) { }

    public override void SceneDidLoad()
    {
        base.SceneDidLoad();
        Screen.autorotateToPortrait = true;
        pauseMenu = GameObject.Find("PauseMenu");
        deathMenu = GameObject.Find("DeathMenu");
        MyAudio backgroundAudio = new MyAudio("BackgroundAudio");
        backgroundAudio.SetupAudioVolume(PlayerPrefs.GetFloat("AudioVolume"));
        MyButton resumeButton = new MyButton("ResumeButton");
        resumeButton.SetText("Resume");
        resumeButton.OnClick(() => {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        });
        MyButton quitButton = new MyButton("QuitButton");
        quitButton.SetText("Quit");
        quitButton.OnClick(() => {
            Time.timeScale = 1;
            PrefabVariables.SaveValues();
            NavigationFramework.RemoveViewNode();
        });
        MyButton pauseButton = new MyButton("PauseButton");
        pauseButton.OnClick(() => {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        });
        pauseMenu.SetActive(false);

        MyLabel deathText = new MyLabel("DeathText");
        deathText.SetText("Death");
        MyButton quitAfterDeathButton = new MyButton("QuitAfterDeathButton");
        quitAfterDeathButton.SetText("Quit");
        quitAfterDeathButton.OnClick(() => {
            Time.timeScale = 1;
            PrefabVariables.SaveValues();
            NavigationFramework.RemoveViewNode();
        });
        deathMenu.SetActive(false);
    }
    public override void SceneBeGone()
    {
        base.SceneBeGone();
        PrefabVariables.SaveValues();
    }
}

public class MainMenu : SceneNode
{
    public MainMenu() : base(SceneNames.MainMenu) { }
}

public struct SceneProvider 
{
    public static SceneNode GetSceneNode(Scenes scene) 
    {
        switch (scene)
        {
            case Scenes.GameScene1:
                return new GameScene1();
            case Scenes.GameScene2:
                return new GameScene2();
            case Scenes.GameScene3:
                return new GameScene3();
            case Scenes.MainMenu:
                return new MainMenu();
            default:
                throw new System.NotImplementedException();
        }
    }
}




