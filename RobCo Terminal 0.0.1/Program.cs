using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Diagnostics.Contracts;
using System.Security.Authentication;

public class terminal
{
    static void Main()
    {

        //creating of an array with all possible passwords
        string[] all_keys = { "EXCHANGED", "RADIATION", "VOLUNTEER", "TERRIFIED", "POISONING", "RETURNING", "AMPLIFIES",
                              "RELEASING", "BATTERIES", "COMPANIES", "IMPLANTED", "SELECTING", "SITUATION", "ENTRANCES",
                              "OPERATING", "DEFEATING", "OFFSPRING", "COMMUNITY", "PANTHEIST", "COUNTRIES", "POSITIONS",
                              "FLAVORING", "REPAIRING", "BARTERING", "PONDEROUS", "FORTIFIED" };
        
        const int LENGTH_OF_PASSWORDS = 9;

        //creating of an array which contains 13 randomly chosen passwords from "all_keys" array
        int amount_of_keys = 13;
        string[] chosen_keys = new string[amount_of_keys];

        //algorithm that allows to choose passwords from "all_keys" array the way they won't be repeated
        Random random_chosen_keys = new Random();
        for (byte i = 0; i < chosen_keys.Length; i++)
        {
            byte a_key_number = (byte)random_chosen_keys.Next(0, all_keys.Length);
            bool coincidence = false;
            for (byte j = 0; j < chosen_keys.Length; j++)
            {
                if (all_keys[a_key_number] == chosen_keys[j])
                {
                    coincidence = true;
                }
            }
            if (!coincidence)
            {
                chosen_keys[i] = all_keys[a_key_number];
            }
            else
            {
                i--;
            }
        }

        //choosing of one right password
        Random random_right_key = new Random();
        int right_key_number = random_right_key.Next(0, chosen_keys.Length);
        string right_key = chosen_keys[right_key_number];

        //array which contains letters/symbols of right password
        char[] right_key_symbols_array = new char[9];
        int rk = 0;
        foreach (char right_key_char in right_key)
        {
            right_key_symbols_array[rk] = right_key_char;
            rk++;
        }

        //array which contains numbers of elements of array "symbols2". These elements are the first letters/symbols of chosen passwords
        ushort[] array_1 = new ushort[amount_of_keys];
        Random random_1 = new Random();

        //algorithm that allows us to choose random numbers of elements of array "symbols2" the way the chosen passwords won't layer on top of each other
        for (int i = 0; i < array_1.Length; i++)
        {
            int ccv = 0;
            ccv = random_1.Next(0, 400);
            bool coincidence = false;
            for (int j = 0; j < array_1.Length; j++)
            {
                if (Math.Abs(array_1[j] - ccv) < LENGTH_OF_PASSWORDS + 1)
                {
                    coincidence = true;
                }
            }
            if (!coincidence)
            {
                array_1[i] = (ushort)ccv;
            }
            else
            {
                i--;
            }
        }

        //array of mathematical symbols
        string[] symbols1 = { "!", "-", "#", "[", "@", ")", "^", "]", "|",
            "<", "=", "?", "{", "(", "\"", "?", ">", "}", ":", "$", "/", ".",
            "\\", "*", "'", ";", "_", "%", ",", "+"};

        //array of mathematical symbols and passwords from "chosen_keys" array
        string[] symbols2 = new string[408];
        Random random_symbols = new Random();
        for (ushort i = 0; i < symbols2.Length; i++)
        {
            short sybmol = (short)random_symbols.Next(0, symbols1.Length);
            symbols2[i] = symbols1[sybmol];
        }
        int n = 0;
        foreach (string chosen_key in chosen_keys)
        {
            int j = array_1[n];
            foreach (char symbol_of_chosen_key in chosen_key)
            {
                symbols2[j] = Convert.ToString(symbol_of_chosen_key);
                j++;
            }
            n++;
        }

        //array of hexdigits
        Random random_hexdigit = new Random();
        ushort hexdigit = (ushort)random_hexdigit.Next(4096, 65140);
        string[] hexdigits = new string[34];
        for (byte i = 0; i < hexdigits.Length; i++)
        {
            hexdigits[i] = "0x" + Convert.ToString(hexdigit, 16).ToUpper();
            hexdigit += 12;
        }

        //array that containse the history of inputs and responses
        char[] response_field = new char[195];

        char space = ' ';

        string showpassword_response = $">Correct     >password:   >{right_key}   ";
        string right_key_response = ">Exact match!>Please wait,>while system>is accessed.";

        byte attempts = 4;
        string square = "█▌";
        

        string entered_key = "";
        bool success = false;
        bool off = false;
        byte end = 0;

        bool white = false;
        bool cyan = true;
        bool yellow = false;
        bool green = false;

        const int AMOUNT_OF_LINES_IN_RESPONSE_FIELD = 15;

        //terminal interface output
        while (attempts != 0 && end < 2 && off == false)
        {

            bool setcolor = false;

            if (white)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (cyan)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else if (yellow)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (green)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.Write("ROBCO INDUSTRIES (TM) TERMLINK PROTOCOL\nENTER PASSWORD NOW\n\n");
            Console.Write($"{attempts} ATTEMPT(S) LEFT: ");
            for (byte i1 = 0; i1 < attempts; i1++)
            {
                Console.Write($"{square}");
            }
            Console.WriteLine('\n');

            int a = 0;
            int b = 0;
            int c = 0;
            for (int i = 0; i < 17; i++)
            {
                Console.Write(hexdigits[a]);

                Console.Write(space);

                for (int i2 = 0; i2 < 12; i2++)
                {
                    Console.Write(symbols2[b]);
                    b++;
                }

                Console.Write(space);

                a += 17;
                Console.Write(hexdigits[a]);
                a -= 17;
                a++;

                Console.Write(space);

                b += 192;
                for (int i2 = 0; i2 < 12; i2++)
                {
                    Console.Write(symbols2[b]);
                    b++;
                }
                b -= 204;

                Console.Write(space);

                if (i < AMOUNT_OF_LINES_IN_RESPONSE_FIELD)
                {
                    for (int i2 = 0; i2 < 13; i2++)
                    {
                        Console.Write(response_field[c]);
                        c++;
                    }
                }

                if (i < 16)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.Write('>');
                }
            }

            entered_key = Console.ReadLine().ToUpper();

            if (end < 1)
            {
                Console.Clear();
            }
            if (entered_key == right_key)
            {
                success = true;
            }
            if (success == true)
            {
                end++;
            }
            if (entered_key == "WHITE")
            {
                white = true;
                cyan = false;
                yellow = false;
                green = false;
                setcolor = true;
            }
            if (entered_key == "CYAN")
            {
                white = false;
                cyan = true;
                yellow = false;
                green = false;
                setcolor = true;
            }
            if (entered_key == "YELLOW")
            {
                white = false;
                cyan = false;
                yellow = true;
                green = false;
                setcolor = true;
            }
            if (entered_key == "GREEN")
            {
                white = false;
                cyan = false;
                yellow = false;
                green = true;
                setcolor = true;
            }
            if (entered_key == "ADDATTEMPT" && attempts < 4)
            {
                attempts++;
            }
            if (entered_key != right_key && entered_key != "SHOWPASSWORD" && entered_key != "ADDATTEMPT" && !setcolor)
            {
                attempts--;
            }
            if (entered_key == "OFF")
            {
                off = true;
            }
            if (entered_key == "ADDATTEMPT" | setcolor)
            {

                for (int v = 0; v < 182; v++)
                {
                    response_field[v] = response_field[v + 13];
                }

                for (int i = 183; i < 195; i++)
                {
                    response_field[i] = ' ';
                }

                response_field[182] = '>';

                int rf = 183;
                foreach (char entered_key_char in entered_key)
                {
                    response_field[rf] = entered_key_char;
                    rf++;
                }
            }

            if (entered_key == "SHOWPASSWORD")
            {
                for (int v = 0; v < 143; v++)
                {
                    response_field[v] = response_field[v + 52];
                }

                for (int i = 143; i < 195; i++)
                {
                    response_field[i] = ' ';
                }

                response_field[143] = '>';

                int rf = 144;
                foreach (char entered_key_char in entered_key)
                {
                    response_field[rf] = entered_key_char;
                    rf++;
                }
                int rf2 = 156;
                foreach (char showpassword_response_char in showpassword_response)
                {
                    response_field[rf2] = showpassword_response_char;
                    rf2++;
                }
            }

            if (entered_key == right_key)
            {
                for (int v = 0; v < 130; v++)
                {
                    response_field[v] = response_field[v + 52];
                }

                for (int i = 130; i < 195; i++)
                {
                    response_field[i] = ' ';
                }

                response_field[130] = '>';

                int rf = 131;
                foreach (char entered_key_char in entered_key)
                {
                    response_field[rf] = entered_key_char;
                    rf++;
                }
                int rf2 = 143;
                foreach (char right_key_response_char in right_key_response)
                {
                    response_field[rf2] = right_key_response_char;
                    rf2++;
                }
            }

            int x = 0;

            if (entered_key != "SHOWPASSWORD" && entered_key != "ADDATTEMPT" && entered_key != right_key && !setcolor)
            {
                char[] incorrect_key_symbols_array = new char[12];
                int ek = 0;
                foreach (char entered_key_char in entered_key)
                {
                    incorrect_key_symbols_array[ek] = entered_key_char;
                    ek++;
                }
                for (int i = 0; i < 9; i++)
                {
                    if (incorrect_key_symbols_array[i] == right_key_symbols_array[i])
                    {
                        x++;
                    }
                }
                string entry_denied_response = $">Entry denied>{x}/{right_key_symbols_array.Length} correct.";

                for (int v = 0; v < 156; v++)
                {
                    response_field[v] = response_field[v + 39];
                }

                for (int i = 156; i < 195; i++)
                {
                    response_field[i] = ' ';
                }

                response_field[156] = '>';

                int rf = 157;
                foreach (char entered_key_char in entered_key)
                {
                    response_field[rf] = entered_key_char;
                    rf++;
                }
                int rf2 = 169;
                foreach (char entry_denied_response_char in entry_denied_response)
                {
                    response_field[rf2] = entry_denied_response_char;
                    rf2++;
                }
            }
        }

        if (attempts == 0)
        {
            Console.Clear();
            Console.WriteLine("TERMINAL LOCKED PLEASE CONTACT AN ADMINISTRATOR");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
        }
    }
}