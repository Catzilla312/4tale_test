using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static UnityEvent CoinPickUp = new UnityEvent();
    
    public static void EmitCoinPickUp(){
        CoinPickUp.Invoke();
    }
}
