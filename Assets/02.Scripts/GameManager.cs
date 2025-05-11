using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;//for Singleton
    
    [Header("References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public List<Transform> points = new();//spawn point list
    public List<GameObject> monsterPool = new();//object pool
    public GameObject monsterPrefab;
    private Transform _spawnPointParent;
    
    [Header("Attributes")]
    private int _totScore = 0;
    public int maxMonsters = 10;
    public bool isGameOver;
     
    public bool IsGameOver
    {
        get{ return isGameOver; }
        set{ isGameOver = value; }
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        _spawnPointParent = GameObject.Find("SpawnPointGroup")?.transform;
        if (_spawnPointParent)
        {
            foreach (Transform point in _spawnPointParent)
            {
                if (point != null)
                    points.Add(point);
            }
        }
        for (int i = 0; i < maxMonsters; i++)
        {
            var monster = Instantiate(monsterPrefab);
            monster.name = $"Monster_{i}";
            monster.SetActive(false); // 비활성화
            monsterPool.Add(monster); // 풀에 추가
        }

        InvokeRepeating(nameof(GenerateMonster), 0f, 3f); //3초마다 반복적으로 생성
    }

    
    void Update()
    {
        if (isGameOver)
        {
            if (PlayerPrefs.GetInt("HighScore") < _totScore)
            {
                
                PlayerPrefs.SetInt("HighScore", _totScore);
                PlayerPrefs.Save();
            }
        }
            // Time.timeScale = 0;
        UpdateGameOverPanel();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.ActivateMenuPanel();
            if(UIManager.Instance.menuPanel.activeSelf == true)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }

    void GenerateMonster()
    {
        if (isGameOver) return;
        GameObject monster = GetMonsterPool();
        if (monster != null)
        {
            int rand = Random.Range(0, points.Count);
            monster.transform.position = points[rand].position;
            monster.SetActive(true);
        }
    }
    public void DisplayScore(int score)
    {
        _totScore += score;
        scoreText.text = $"<color=#00FF00>SCORE :</color> <color=#FF0000>{_totScore:D5}</color>";
    }

    void UpdateGameOverPanel()
    {
        gameOverText.text = $"<color=#FF0000>GAME OVER !!!</color>\nScore: {_totScore:D5}\nHighScore: {PlayerPrefs.GetInt("HighScore",0):D5}";
    }

    GameObject GetMonsterPool()
    {
        foreach (GameObject m in monsterPool)
        {
            if (!m.activeSelf)
                return m;
        }

        return null;
    }
}
