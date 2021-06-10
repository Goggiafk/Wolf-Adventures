using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSaveSyst : MonoBehaviour
{
    public GameObject prefabM;
    public string[] saveFilesM;
    public GameObject loadAreaM;
    public Button loadButton;

    void Awake()
    {
        if (PlayerPrefs.HasKey("openLoadMenu"))
        {
            PlayerPrefs.DeleteKey("openLoadMenu");
            loadAreaM.SetActive(true);
            loadSave();
        }
        Start();
    }
    void Start()
    {
        
        if (Directory.Exists(Application.persistentDataPath + "/WolfAdventuresSaves"))
            loadButton.interactable = true;
         else
            loadButton.interactable = false;
    }

    public void DelSave(string path)
    {
        File.Delete(path);
        PlayerPrefs.SetInt("openLoadMenu", 1);
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGame(string path)
    {
        PlayerPrefs.SetString("LoadScene", path);
        SceneManager.LoadScene("WolfAdventure");
    }

    public void loadSave()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/WolfAdventuresSaves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/WolfAdventuresSaves");
        }

        saveFilesM = Directory.GetFiles(Application.persistentDataPath + "/WolfAdventuresSaves");

        for (int i = 0; i < saveFilesM.Length; i++)
        {
            GameObject cosM = Instantiate(prefabM);
            cosM.transform.SetParent(loadAreaM.transform, false);
            cosM.transform.localScale = Vector3.one;
            cosM.transform.localPosition = new Vector3(0, i * (-30), 0);

            var index = i;
            Button[] cosbuttons = FindObjectsOfType<Button>();
            cosbuttons[1].GetComponent<Button>().onClick.AddListener(() =>
            {
                LoadGame(saveFilesM[index]);
            });
            cosbuttons[1].GetComponentInChildren<Text>().text = saveFilesM[index].Replace(Application.persistentDataPath + "/WolfAdventuresSaves", "");
            cosbuttons[0].GetComponentInChildren<Text>().text = "Удолить";
            cosbuttons[0].GetComponent<Button>().onClick.AddListener(() =>
            {
                DelSave(saveFilesM[index]);
            });
        }

        Destroy(prefabM);
        
    }

    IEnumerator Timer(System.Action action)
    {
        for (float f = 0.05f; f <= 0.5f; f += 0.1f)
        {

            yield return new WaitForSeconds(0.1f);
        }
        action();
    }
}
