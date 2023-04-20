namespace HabitTracker.barakisbrown;

using System;

internal class Helpers
{
    static string GetMenu()
    {
        string menu = @"
        
        Main Menu

        What would you like to do?
        
        Type 0 to Close Application.
        Type 1 to View All Records.
        Type 2 to Insert Record
        Type 3 to Delete Record
        Type 4 to Update Record.
        ----------------------------------
        Please Select (1-4) OR 0 to exit
        ";

        return menu;
    }
}
