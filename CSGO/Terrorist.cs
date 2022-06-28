using static CSGO.Program;
using static System.Console;

namespace CSGO
{
    class Terrorist
    {
        public bool IsDead = false;
        public static bool BombPlanted = false;
        public static int BombPlanting_Progress = 0;
        public static bool BombSite_Discovered = false;

        public static void FindBombSite()
        {            
            if (IsSuccessful(10))
            {
                BombSite_Discovered = true;
                WriteLineRed($"The terrorists found the bomb site.");
            }
            else WriteLine($"The terrorists searched for the bomb site.");
        }

        public void KillCounterTerrorist(CounterTerrorist ct)
        {
            if (IsSuccessful(7))
            {
                ct.IsDead = true;
                WriteLineRed("A terrorist killed a counterterrorist.");
                if (CounterTerroristsDead())
                {
                    WriteLineRed($"The counterterrorists have been eliminated!");
                    WriteLineRed($"\nThe terrorists won!");
                    GameOver = true;
                }
                else WriteLine($"Counterterrorists remaining: {CounterTerroristsRemaining()}.");

            }
            else WriteLine("A terrorist launched a failed attack.");
        }

        public static void PlantBomb()
        {
            BombPlanting_Progress++;
            if (BombPlanting_Progress < 5)
            {
                WriteLine($"The terrorists are planting the bomb. Progress: {BombPlanting_Progress} of 5.");
            }
            else
            {
                BombPlanted = true;
                WriteLineRed("The terrorists have planted the bomb!");
            }
        }
    }
}
