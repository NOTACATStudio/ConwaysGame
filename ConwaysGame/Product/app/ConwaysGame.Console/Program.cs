using System;
using ConwaysGame.Cells;

namespace ConwaysGame.Console
{
  class Program
  {
    static void Main(string[] args)
    {
      var executioner = new ConwayExecutioner();
      
      executioner.ExecuteConway(executioner.SetupView());
    }
  }
}
