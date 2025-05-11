using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    public Button startButton;
    public Button optionButton;
    public Button shopButton;

    public GameObject gameOverPanel;
    public GameObject hpBarPanel;
    public GameObject scorePanel;
    public GameObject menuPanel;

    private UnityAction _action;

    void Start()
    {
        _action = () => OnButtonClick(startButton.name);
        startButton.onClick.AddListener(_action);
        
        optionButton.onClick.AddListener(delegate { OnButtonClick(optionButton.name); });
        
        shopButton.onClick.AddListener(()=> OnButtonClick(shopButton.name) );
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

    // Update is called once per frame
    void Update()
    {
        if (gameOverPanel.activeSelf)
        {
            hpBarPanel.SetActive(false);
            scorePanel.SetActive(false);
        }
    }
    
    public void OnButtonClick(string msg)
    {
        Debug.Log($"Click Button: {msg}");
    }

    public void ActivateGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void ActivateMenuPanel()
    {
        if(menuPanel.activeSelf)
            menuPanel.SetActive(false);
        else
            menuPanel.SetActive(true);
    }
    
}
