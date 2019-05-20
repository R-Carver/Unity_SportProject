using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteController
{
    public static RouteController Instance{get; protected set;}

    //Routes are for now hardcoded here
    public Route[] routes;

    //for now lets keep track if the receiver finished its route here
    public bool currentRouteFinished;

    public RouteController()
    {
        
        if(Instance != null)
        {
            Console.WriteLine("There should never be two RouteControllers");
        }
        Instance = this;

        InitRoutes();
        
    }

    //for now create the routes manually here
    private void InitRoutes()
    {   
        //for now increase this manually
        routes = new Route[7];

        Route route1 = new Route(2);
        route1.addWayPoint(0, new Vector3(0,0,-5));
        route1.addWayPoint(1, new Vector3(-5, 0, 0));
        routes[0] = route1;

        //slant route
        Route route2 = new Route(2);
        route2.addWayPoint(0, new Vector3(0,0,-2));
        route2.addWayPoint(1, new Vector3(-5,0,-5));
        routes[1] = route2;

        //post route
        Route route3 = new Route(1);
        route3.addWayPoint(0, new Vector3(0,0,-10));
        routes[2] = route3;

        //In-route close
        Route route4 = new Route(2);
        route4.addWayPoint(0, new Vector3(0,0,-2));
        route4.addWayPoint(1, new Vector3(-7, 0, 0));
        routes[3] = route4; 

        //Comeback route
        Route route5 = new Route(2);
        route5.addWayPoint(0, new Vector3(0,0,-7));
        route5.addWayPoint(1, new Vector3(-5,0,4));
        routes[4] = route5;

        Route route6 = new Route(2);
        route6.addWayPoint(0, new Vector3(0,0,-9));
        route6.addWayPoint(1, new Vector3(-5,0,0));
        routes[5] = route6;

        //slant2
        Route route7 = new Route(1);
        route7.addWayPoint(0, new Vector3(-6,0,-6));
        routes[6] = route7;
    }

    public Route GetRandomRoute()
    {
        int numRoutes = routes.Length;
        int randIndex = UnityEngine.Random.Range(0,numRoutes);

        return routes[6];
        //return routes[randIndex];
    }
}