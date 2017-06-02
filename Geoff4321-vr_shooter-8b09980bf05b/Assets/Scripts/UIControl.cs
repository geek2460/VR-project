using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {
    public GameObject UIobject;
    public GameObject UIobject2;
    public Text myText;
    static int songIdx = 0;
    private List<string> songList;
    private List<string> songFullList;
    private List<string> songDirectList;
    public AudioSource myAudio;
    
    // Use this for initialization
    void Start () {
        songList = new List<string>();
        songFullList = new List<string>();
        songDirectList = new List<string>();
        string path = "Assets/Resources/songs/";
        var info = new DirectoryInfo(path);
        var DirectoryInfo = info.GetDirectories();
        foreach (var directory in DirectoryInfo)
        {
            Debug.Log("Directory: " + directory.Name);
            var subInfo = new DirectoryInfo(path+ directory.Name);
            var subDirectoryInfo = subInfo.GetDirectories();
            foreach (var subDirectory in subDirectoryInfo)
            {
                //Debug.Log("\t songDirectory: " + subDirectory.Name);
                songList.Add(subDirectory.Name);
                //Debug.Log("songs/" + directory.Name + "/" + subDirectory.Name + "/" + subDirectory.Name);
                songFullList.Add("songs/"+directory.Name + "/" + subDirectory.Name+"/"+ subDirectory.Name);

                songDirectList.Add("Assets/Resources/songs/" + directory.Name + "/" + subDirectory.Name);
                Debug.Log("Assets/Resources/songs/" + directory.Name + "/" + subDirectory.Name);
                

            }
        }
        Debug.Log("songs number: " + songDirectList.Count);

        /*StreamWriter writer = new StreamWriter(songDirectList[0] + "/score.txt");
        writer.WriteLine("0");
        writer.Close();
        */
        StreamReader scoreReader = new StreamReader(songDirectList[songIdx] + "/score.txt");
        myText.text = songList[songIdx] + "\nTop score:" + scoreReader.ReadLine();
        scoreReader.Close();
        myAudio.clip = Resources.Load<AudioClip>(songFullList[0]);
        myAudio.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(UIobject.activeSelf)
                UIobject.SetActive(false);
            else
                UIobject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (UIobject2.activeSelf)
                UIobject2.SetActive(false);
            else
                UIobject2.SetActive(true);
        }
        /*debug change song*/
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeSongLeft();
        }
        /*debug change song*/
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeSongRight();
        }
        /*debug change song*/
        if (Input.GetKeyDown(KeyCode.B))
        {
            SongComfirm();
        }

    }

    public void ChangeSongRight()
    {
        songIdx++;
        if (songIdx == songList.Count)
            songIdx = 0;
        StreamReader scoreReader = new StreamReader(songDirectList[songIdx] + "/score.txt");
        myText.text = songList[songIdx] + "\nTop score:" + scoreReader.ReadLine();
        scoreReader.Close();
        myAudio.clip = Resources.Load<AudioClip>(songFullList[songIdx]);
        myAudio.Play();
    }

    public void ChangeSongLeft()
    {
        songIdx--;
        if (songIdx == -1)
            songIdx = songList.Count-1;
        StreamReader scoreReader = new StreamReader(songDirectList[songIdx] + "/score.txt");
        myText.text = songList[songIdx] + "\nTop score:" + scoreReader.ReadLine();
        scoreReader.Close();
        myAudio.clip = Resources.Load<AudioClip>(songFullList[songIdx]);
        myAudio.Play();
    }

    public void SongComfirm()
    {
        Debug.Log(songList[songIdx]);
    }
}
