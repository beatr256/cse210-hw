using System;
using System.Threading;


public class Activity
{
protected string name;
protected string description;
protected int duration;


public Activity(string name, string description)
{
this.name = name;
this.description = description;
}


public void SetDuration(int seconds)
{
duration = seconds;
}


public void StartMessage()
{
Console.WriteLine($"Starting {name} Activity!");
Console.WriteLine(description);
Console.WriteLine($"Duration: {duration} seconds");
Console.WriteLine("Get ready...");
PauseWithAnimation(3);
}


public void EndMessage()
{
Console.WriteLine("Well done!");
PauseWithAnimation(2);
Console.WriteLine($"{name} activity completed. Duration: {duration} seconds.\n");
PauseWithAnimation(2);
}


protected void PauseWithAnimation(int seconds)
{
for (int i = 0; i < seconds; i++)
{
Console.Write(".");
Thread.Sleep(1000);
}
Console.WriteLine();
}
}