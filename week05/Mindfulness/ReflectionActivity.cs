using System;
using System.Collections.Generic;
using System.Threading;


public class ReflectionActivity : Activity
{
private List<string> prompts = new List<string> {
"Think of a time when you stood up for someone else.",
"Think of a time when you did something really difficult.",
"Think of a time when you helped someone in need."
};


private List<string> questions = new List<string> {
"Why was this experience meaningful to you?",
"Have you ever done anything like this before?",
"How did you feel when it was complete?"
};


public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience.") { }


public void Perform()
{
StartMessage();
Random rnd = new Random();
Console.WriteLine(prompts[rnd.Next(prompts.Count)]);
PauseWithAnimation(3);


DateTime endTime = DateTime.Now.AddSeconds(duration);
while (DateTime.Now < endTime)
{
Console.WriteLine(questions[rnd.Next(questions.Count)]);
PauseWithAnimation(5);
}


EndMessage();
}
}