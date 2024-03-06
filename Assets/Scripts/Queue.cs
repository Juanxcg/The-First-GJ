using System;
using System.Collections.Generic;
public class Queue
{
    public Pieces[] array;
    public int front;
    public int rear;
    public int length;
    public Queue()
    {
        array = new Pieces[500];
        front = rear = 0;
        length = 0;
    }
    public void EnQueue(Pieces newPoint)
    {
        array[rear] = newPoint;
        rear = (rear + 1)% 500;
        length++;
    }
    public bool IsEmpty()
    {
        if (front == rear) 
            return true;
        else
            return false;
    }
    public void DeQueue()
    {
        if (IsEmpty())
            return;
        front++;
        length--;
    }
    public Pieces GetFront()
    {
        return array[front];
    }
    public void clean()
    {
        front = rear = 0;
        length = 0;
    }
    public int GetLenth()
    {
        return length;
    }

}
