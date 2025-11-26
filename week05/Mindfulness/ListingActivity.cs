using System;
using System.Collections.Generic;
using System.Threading;


public class ListingActivity : Activity
{
private List<string> prompts = new List<string> {
"Who are people that you appreciate?",
"What are personal strengths of yours?",
"Who are some of your personal heroes?"
};


public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by listing as many things as you can in a certain area.") { }


public void Perform()
{
StartMessage();
Random rnd = new Random();
Console.WriteLine(prompts[rnd.Next(prompts.Count)]);
PauseWithAnimation(3);


DateTime endTime = DateTime.Now.AddSeconds(duration);
int count = 0;
while (DateTime.Now < endTime)
{
Console.Write("Enter item: ");
Console.ReadLine();
count++;
}


Console.WriteLine($"You listed {count} items!");
EndMessage();
}
}