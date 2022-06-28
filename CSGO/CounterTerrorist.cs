using static CSGO.Program;
using static System.Console;

namespace CSGO
{
    class CounterTerrorist
    {
        public bool IsDead = false;
        public static int BombDefusion_Progress = 0;

        public void KillTerrorist(Terrorist terrorist)
        {
            if (Terrorist.BombPlanted) { if (IsSuccessful(3)) terrorist.IsDead = true; }
            else if (IsSuccessful(5)) terrorist.IsDead = true;

            if (terrorist.IsDead)
            {
                WriteLineGreen($"A counterterrorist killed a terrorist.");
                if (TerroristsDead())
                {
                    WriteLineGreen($"The terrorists have been eliminated!");
                    if (!Terrorist.BombPlanted)
                    {
                        WriteLineGreen($"\nThe counterterrorists won!");
                        GameOver = true;
                    }
                }
                else WriteLine($"Terrorists remaining: {TerroristsRemaining()}.");
            }
            else WriteLine($"A counterterrorist launched a failed attack.");
        }

        public static void DefuseBomb()
        {
            BombDefusion_Progress++;
            if (BombDefusion_Progress < 5)
            {
                WriteLine($"The counterterrorists are defusing the bomb. Progress {BombDefusion_Progress} of 5.");
            }
            else
            {
                WriteLineGreen("The counterterrorists have defused the bomb!");
                WriteLineGreen($"\nThe counterterrorists won!");
                GameOver = true;
            }
        }
    }
}
