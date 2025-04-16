using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public Button startButton;
    public Button optionButton;
    public Button shopButton;
    private UnityAction _action;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _action = () => OnButtonClick(startButton.name);
        startButton.onClick.AddListener(_action);
        
        optionButton.onClick.AddListener(delegate { OnButtonClick(optionButton.name); });
        
        shopButton.onClick.AddListener(()=> OnButtonClick(shopButton.name) );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnButtonClick(string msg)
    {
        Debug.Log($"Click Button: {msg}");
    }
}
