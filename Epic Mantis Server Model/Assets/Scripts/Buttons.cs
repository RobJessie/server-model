using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public Customer customer;
    public Line line1;
    public Line line2;
    public Transform doorway;
    public Transform theVoid;

    public void createCustomer(int lineNum)
    {
        Customer cust = Instantiate(customer);
        if(lineNum == 1)
            cust.line = line1;
        else
            cust.line = line2;
        cust.doorway = doorway.position;
        cust.theVoid = theVoid.position;
    }

    public void removeCustomer(int lineNum)
    {
        if (lineNum == 1)
            line1.removeCustomer();
        else
            line2.removeCustomer();
    }

    public void removeThirdCustomer(int lineNum)
    {
        if (lineNum == 1)
            line1.removeCustomer(2);
        else
            line2.removeCustomer(2);
    }
 
}
