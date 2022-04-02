using System;
using System.Windows.Forms;

public class mouseTagMain
{
  static void Main(string[] args)
  {
    System.Console.WriteLine("Welcome to the Mouse Tag Program!");

    //create a mouseTagForm object to run the UI
    mouseTagForm rat = new mouseTagForm();
    Application.Run(rat);

    System.Console.WriteLine("Mouse Tag Program Will Now Terminate!");
    System.Console.WriteLine("Hit Enter.");
  }//end of main method

}//end of mouseTagMain
