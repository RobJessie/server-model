using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Transform[] lineQueue;
    public List<Customer> customers;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool addCustomer(Customer newCust) //Return false if line is full
    {
        if (customers.Count == lineQueue.Length)
            return false;
        customers.Add(newCust);
        newCust.moveTo(lineQueue[customers.Count-1].position);
        return true;
    }

    public void removeCustomer()
    {
        if (customers.Count > 0)
        { 
            customers[0].leaveStore();
            customers.RemoveAt(0);
        }
        if(customers.Count > 0) //If there are still customers in line, move up
        {
            int pos = 0;
            foreach(Customer customer in customers)
            {
                customer.moveTo(lineQueue[pos].position);
                pos++;
            }
        }
    }

    public void removeCustomer(int pos)
    {
        if (customers.Count > pos) //Make sure there are that many customers
        {
            customers[pos].leaveStore();
            customers.RemoveAt(pos);
        }
        if (customers.Count > pos) //Move up any customers behind them
        {
            for (int i = pos; i < customers.Count; i++)
            {
                customers[i].moveTo(lineQueue[i].position);
            }
        }
    }

}
