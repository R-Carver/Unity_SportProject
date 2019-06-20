using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{   

    public static GameController _instance;
    // Start is called before the first frame update

    public Academy_Combined academy;

    //Agents
    public DefAgent_ManCov defAgent;
    public PassAgent passAgent;

    void Awake()
    {
        _instance = this;
    }
    void Start()
    {   
        GameObject passAgentGo = GameObject.Find("Passer");
        passAgent = passAgentGo.GetComponent<PassAgent>();

        GameObject defAgentGo = GameObject.Find("Defender");
        defAgent = defAgentGo.GetComponent<DefAgent_ManCov>();


        RouteController routeController = new RouteController();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameController GetInstance()
    {
        return _instance;
    }
}
