using System;
using System.Threading;


public class BreathingActivity : Activity
{
public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.") { }


public void Perform()
{
StartMessage();
DateTime endTime = DateTime.Now.AddSeconds(duration);


while (DateTime.Now < endTime)
{
Console.WriteLine("Breathe in...");
PauseWithAnimation(4);
Console.WriteLine("Breathe out...");
PauseWithAnimation(4);
}


EndMessage();
}
}