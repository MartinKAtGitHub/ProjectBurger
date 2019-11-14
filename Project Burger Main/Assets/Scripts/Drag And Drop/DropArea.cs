using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropArea : MonoBehaviour, IDropHandler
{
   
    public virtual void OnDrop(PointerEventData eventData)
    {
        // diconnect everything
        // reconnect to this specific drop area
        
        //UnlimitedElementsOnDropArea(eventData);
       // _burgerMeatLogic.DropArea = this;
    }


}
