namespace McGurkin.Cli;

public class Ui
{
    private static void DrawOptions(List<string> options, int selectedIndex)
    {
        for (int i = 0; i < options.Count; i++)
        {
            if (i == selectedIndex)
                Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"> {options[i]}".PadRight(Console.WindowWidth - 1));
            Console.ResetColor();
        }
    }

    public static void DrawProgressBar(int progress, int total, DateTimeOffset? startTime = null, int progressBarWidth = 40)
    {
        double progressDouble = progress / (double)total;
        int progressPosition = (int)(progressBarWidth * progressDouble);

        string line = "";
        line += "[";
        for (int i = 0; i < progressBarWidth; i++)
        {
            if (i < progressPosition)
                line += "=";
            else
                line += " ";
        }
        line += $"] {(int)(progressDouble * 100)}%";

        if (null != startTime)
        {
            var elapsedTime = DateTimeOffset.Now - startTime;
            var itemTime = elapsedTime / progress;
            var remainingTime = (total - progress) * itemTime;
            var endTime = DateTimeOffset.Now + remainingTime;
            line += $", ETA: {endTime}";
        }

        Console.CursorLeft = 0;
        Console.Write(line);
        if (progress >= total)
            Console.WriteLine();
    }

    public static int DrawSelectOptions(List<string> options)
    {
        int returnValue = -1;
        bool selectionChanging = true;
        Console.CursorVisible = false;

        DrawOptions(options, returnValue);

        do
        {
            if (Console.KeyAvailable) // Check if a key press is available to avoid blocking
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        returnValue--;
                        if (returnValue < 0) returnValue = options.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        returnValue++;
                        if (returnValue >= options.Count) returnValue = 0;
                        break;
                    case ConsoleKey.Enter:
                        selectionChanging = false;
                        break;
                }
            }

            Console.CursorTop -= options.Count;
            DrawOptions(options, returnValue);

        } while (selectionChanging);

        Console.CursorVisible = true;
        return returnValue;
    }
}
