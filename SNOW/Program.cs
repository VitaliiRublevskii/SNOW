

using SNOW;

string path = "myFile.txt";


Cell c1 = new Cell();                        // Пустая ячейка

CellWithPlayer cp1 = new CellWithPlayer();  // Ячейка с игроком 1
CellWithPlayer cp2 = new CellWithPlayer();  // Ячейка с игроком 2

CellWithSnow cs1 = new CellWithSnow();      // Ячейка со снежком

CellNumber cn = new CellNumber(0);          //  Ячейка с номером 

Console.WriteLine();
Console.WriteLine();


List<List <Cell>> field = new List<List<Cell>> (5);  // первоначальное поле

List<List<Cell>> fieldRead = new List<List<Cell>>(5);  //  поле для считывания из файла

///   ПОЛЕ  (старт игры  /  формирование поля)
for (int i = 0; i < 5; i++)
{
    if (i == 0 || i == 4)
    {
        field.Add(new List<Cell>(5));
        for (int j = 0; j < 5; j++)
        {
            if (i == 0 && j == 2) { field[i].Add(cp1); }
            else if (i == 4 && j == 2) { field[i].Add(cp2); }
            else { field[i].Add(new CellNumber(j + 1)); }
        }
    }
    else
    {
        field.Add(new List<Cell>(5));
        for (int j = 0; j < 5; j++)
        {
             field[i].Add(c1); 
        }
    }
    
}

//  ФУНКЦИЯ  :  ЗАПИСЬ поля в файл  (2-й вариант - по строкам)
void FileWrite (List<List<Cell>> fieldWr, string path)
{
    File.Delete (path);
    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            File.AppendAllText(path, (fieldWr[i][j].S + ":")); // запись в файл
        }
        File.AppendAllText(path, "\n");
    }
}


//  ФУНКЦИЯ  :  Считывание поля  из файла
void FileRead (List<List<Cell>> fieldRead, string path) {
    fieldRead.Clear();
    string[] cellStr = File.ReadAllLines(path);
    for (int i = 0; i < cellStr.Length; i++)
    {
        List<Cell> cell1= new List<Cell>(5);
        string[] split = cellStr[i].Split(':');
        for (int j = 0; j < split.Length; j++)
        {
            if (i == 0 && split[j] == "| >i< |")
                cell1.Add(cp1);
            else if (i == 4 && split[j] == "| >i< |")
                cell1.Add(cp2);
            else if (i > 0 && i < 4 && split[j] == "|     |")
                cell1.Add(c1);
            else if (i >= 0 && i <= 4 && split[j] == "|  *  |")
                cell1.Add(cs1);
            else { cell1.Add(new Cell(split[j])); }
            
        }
        fieldRead.Add(cell1);
    }
}

//  ФУНКЦИЯ  :  Вывод  поля  в  консоль
void PrintField(List<List<Cell>> fieldGame)
{
    Console.WriteLine("\n\tИГРА  В  СНЕЖКИ:");
    Console.WriteLine();
    Console.WriteLine("\t   Игрок  1:");
    Console.WriteLine();
    Console.WriteLine(" * * *         * * *         * * * ");
    Console.WriteLine();
    for (int i = 0; i < fieldGame.Count; i++)
    {
        for (int j = 0; j < fieldGame.Count; j++)
        {
            if (i == 0)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Black;
                fieldGame[i][j].PrintCell();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (i == 4)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                fieldGame[i][j].PrintCell();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else { fieldGame[i][j].PrintCell(); }
        }
        Console.WriteLine();
    }
    Console.WriteLine();
    Console.WriteLine(" * * *         * * *         * * * ");
    Console.WriteLine();
    Console.WriteLine("\t   Игрок  2:\n\n");
}



FileWrite(field, path);
Console.WriteLine("Записалось начальное поле field в файл...");
Console.ReadKey();


bool exit = true;
int raund = 0;
while (exit)
{
    string steps = new string("");
    string steps2 = new string("");
    char[] step = new char[4];
    

    for (int r1 = 0; r1 < 1; r1++)
    {

        //  ИГРОК  1   -   АТАКА
        raund++;
        Console.Clear();
        Console.WriteLine($"\tРАУНД  №  {raund}");
        FileRead(fieldRead, path);
        PrintField(fieldRead);
        bool f = true;
        while (f)
        {
            Console.WriteLine("\tИгрок 1, введите свои шаги для стрельбы снежкамм (от 1 до 4 шагов)\n Для выхода введите 0");
            steps = Console.ReadLine(); // строка шагов
            step = steps.ToCharArray(); // массив символов (шаги)
            int numVal;
            bool isNumber = int.TryParse(steps, out numVal);
            f = false;
            //  ПРОВЕРКА  на  правильность  ввода  шагов
            if (steps.Length <= 0 || steps.Length > 4) // проверка на количетсво шагов и все сиволы - цифры
            {
                Console.WriteLine("Неверное количество шагов (должно быть мин.1 - макс.4");
                f = true;
            }
            if (steps == "0") { exit = false; break; }
            else if (!isNumber)
            {
                Console.WriteLine("В Вашем выборе есть символы, отличные от цифр");
                f = true;
            }
            else
            {
                for (int i = 0; i < step.Length; i++)
                {
                    if (step[i] != '1' && step[i] != '2' && step[i] != '3' && step[i] != '4' && step[i] != '5')
                    {
                        Console.WriteLine("В Ваших шагах есть поля за границами игрового поля");
                        f = true;
                        break;
                    }
                }
                // проверка  на то что шаги должны быть рядом
                for (int i = 0; i < step.Length; i++)
                {
                    int step1 = 0;
                    int.TryParse(step[0].ToString(), out step1);

                    if (step1 - fieldRead[0].FindIndex(x => x == cp1) != 0 && step1 - fieldRead[0].FindIndex(x => x == cp1) != 2)   // ?????
                    {
                        Console.WriteLine("Шаги должны быть на соседние с игроком клетки (прыгать нельзя)");
                        Console.WriteLine($"Сейчас игрок 2 находится на позиции {((fieldRead[0].FindIndex(x => x == cp1) + 1))}");
                        f = true;
                        break;
                    }

                }
            }

        }
        Console.WriteLine("Хороший выбор)))");
        Thread.Sleep(1000);
        int x = 0, y = 0;
        //  передвижение игрока 2 (в атаке)  и снежков
        if (exit)
        {
            for (int i = 0; i < step.Length; i++)
            {
                //FileRead(fieldRead, path);
                Console.Clear();
                if (step[i] != 0)
                {
                    int.TryParse(step[i].ToString(), out x); //  в х  записываем шаг игрока
                }
                //   поиск снежков

                if (fieldRead[4].FindIndex(x => x == cs1) >= 0)  // если  * в поле с номерами
                {
                    fieldRead[4][fieldRead[4].FindIndex(x => x == cs1)] = new CellNumber((fieldRead[4].FindIndex(x => x == cs1) + 1));
                }
                if (fieldRead[3].FindIndex(x => x == cs1) >= 0)  //  если * в поле 1
                {
                    fieldRead[4][fieldRead[3].FindIndex(x => x == cs1)] = cs1;
                    fieldRead[3][fieldRead[3].FindIndex(x => x == cs1)] = c1;
                }
                if (fieldRead[2].FindIndex(x => x == cs1) >= 0)  //  если * в поле 2
                {
                    fieldRead[3][fieldRead[2].FindIndex(x => x == cs1)] = cs1;
                    fieldRead[2][fieldRead[2].FindIndex(x => x == cs1)] = c1;
                }
                if (fieldRead[1].FindIndex(x => x == cs1) >= 0)  //  если * в поле 3
                {
                    fieldRead[2][fieldRead[1].FindIndex(x => x == cs1)] = cs1;
                    fieldRead[1][fieldRead[1].FindIndex(x => x == cs1)] = c1;
                }

                //  передвижение игрока 2 и броски снежков
                if ((x - 1) < fieldRead[0].FindIndex(x => x == cp1)) // x = 2 (x-1)=1, cp1 = 2 (left)
                {
                    fieldRead[0][x - 1] = cp1; // step[0] = 2 - > field[4][j] - j = 1
                    fieldRead[0][x] = new CellNumber(x + 1);
                    fieldRead[1][x - 1] = cs1; // 1-й снежок вылетел
                }
                else if ((x) > fieldRead[0].FindIndex(x => x == cp1))  // right
                {
                    fieldRead[0][x - 1] = cp1; // step[0] = 2 - > field[4][j] - j = 1
                    fieldRead[0][x - 2] = new CellNumber(x - 1);
                    fieldRead[1][x - 1] = cs1; // 1-й снежок вылетел
                }
                PrintField(fieldRead);
                FileWrite(fieldRead, path);  //  после круга - записали в файл
                Thread.Sleep(1000);
                //Console.Clear(); 
            }
            //   ИГРОК  2  -  ЗАЩИТА
            f = true;
            while (f)
            {
                Console.WriteLine("\tИгрок 2, введите свои шаги для уклонения от снежков (от 1 до 4 шагов)");
                steps2 = Console.ReadLine(); // строка шагов
                step = steps2.ToCharArray(); // массив символов (шаги)
                int numVal;
                bool isNumber = int.TryParse(steps2, out numVal);
                f = false;
                //  ПРОВЕРКА  на  правильность  ввода  шагов
                if (steps2.Length <= 0 || steps2.Length > 4) // проверка на количетсво шагов и все сиволы - цифры
                {
                    Console.WriteLine("Неверное количество шагов (должно быть мин.1 - макс.4");
                    f = true;
                }
                else if (steps2.Length != steps.Length)
                {
                    Console.WriteLine($"Неверное количество шагов (должно быть такое же как у Игрока 1: {steps.Length} шага)");
                    f = true;
                }
                else if (!isNumber)
                {
                    Console.WriteLine("В Вашем выборе есть символы, отличные от цифр");
                    f = true;
                }
                else
                {
                    for (int k = 0; k < step.Length; k++)
                    {
                        if (step[k] != '1' && step[k] != '2' && step[k] != '3' && step[k] != '4' && step[k] != '5')
                        {
                            Console.WriteLine("В Ваших шагах есть поля за границами игрового поля");
                            f = true;
                            break;
                        }
                    }
                    for (int k = 0; k < step.Length; k++)
                    {
                        int step1 = 0;
                        int.TryParse(step[0].ToString(), out step1);

                        if (step1 - fieldRead[4].FindIndex(x => x == cp2) != 0 && step1 - fieldRead[4].FindIndex(x => x == cp2) != 2)   // ?????
                        {
                            Console.WriteLine("Шаги должны быть на соседние с игроком клетки (прыгать нельзя)");
                            Console.WriteLine($"Сейчас игрок 1 находится на позиции {((fieldRead[4].FindIndex(x => x == cp2) + 1))}");
                            f = true;
                            break;
                        }

                    }
                }

            }
            Console.WriteLine("Хороший выбор)))");
            Thread.Sleep(1000);
            //  передвижение игрока 2 (в защите)  и  снежков
            for (int k = 0; k < step.Length; k++)
            {
                FileRead(fieldRead, path);
                Console.Clear();
                if (step[k] != 0)
                {
                    int.TryParse(step[k].ToString(), out y); //  в х  записываем шаг игрока
                }

                //  передвижение игрока 2 
                if (((y - 1) < fieldRead[4].FindIndex(x => x == cp2)) && (fieldRead[4].FindIndex(x => x == cp2) != -1)) // x = 2 (x-1)=1, cp1 = 2 (left)
                {
                    fieldRead[4][y - 1] = cp2; // step[0] = 2 - > field[4][j] - j = 1
                    fieldRead[4][y] = new CellNumber(y + 1);
                }
                else if ((y > fieldRead[4].FindIndex(x => x == cp2)) && (fieldRead[4].FindIndex(x => x == cp2) != -1))  // right
                {
                    fieldRead[4][y - 1] = cp2; // step[0] = 2 - > field[4][j] - j = 1
                    fieldRead[4][y - 2] = new CellNumber(y - 1);
                }
                //   поиск снежков (которые летят в игрока 2)

                if (fieldRead[4].FindIndex(x => x == cs1) >= 0)  // если  * в поле с номерами
                {
                    fieldRead[4][fieldRead[4].FindIndex(x => x == cs1)] = new CellNumber((fieldRead[4].FindIndex(x => x == cs1) + 1));
                    if (fieldRead[4].FindIndex(x => x == cp2) < 0)
                    {
                        Console.WriteLine("Снежок попал в ИГРОКА 2 ! \n >ИГРОК  2  ПРОИГРАЛ<");
                        Console.ReadKey();
                        exit = false;
                        break;
                    }
                }
                if (fieldRead[3].FindIndex(x => x == cs1) >= 0)  //  если * в поле 1
                {
                    fieldRead[4][fieldRead[3].FindIndex(x => x == cs1)] = cs1;
                    fieldRead[3][fieldRead[3].FindIndex(x => x == cs1)] = c1;

                }
                if (fieldRead[2].FindIndex(x => x == cs1) >= 0)  //  если * в поле 2
                {
                    fieldRead[3][fieldRead[2].FindIndex(x => x == cs1)] = cs1;
                    fieldRead[2][fieldRead[2].FindIndex(x => x == cs1)] = c1;
                }
                if (fieldRead[1].FindIndex(x => x == cs1) >= 0)  //  если * в поле 3
                {
                    fieldRead[2][fieldRead[1].FindIndex(x => x == cs1)] = cs1;
                    fieldRead[1][fieldRead[1].FindIndex(x => x == cs1)] = c1;
                }

                PrintField(fieldRead);
                FileWrite(fieldRead, path);  //  после круга - записали в файл
                Thread.Sleep(1000);

            }

            
        }
        

        
    }

    if (exit)
    {
        for (int r1 = 0; r1 < 1; r1++)
        {

            //  ИГРОК  2   -   АТАКА
            raund++;
            Console.Clear();
            Console.WriteLine($"\tРАУНД  №  {raund}");
            FileRead(fieldRead, path);
            PrintField(fieldRead);
            bool f = true;
            while (f)
            {
                Console.WriteLine("\tИгрок 2, введите свои шаги для стрельбы снежкамм (от 1 до 4 шагов)\n Для выхода введите 0");
                steps = Console.ReadLine(); // строка шагов
                step = steps.ToCharArray(); // массив символов (шаги)
                int numVal;
                bool isNumber = int.TryParse(steps, out numVal);
                f = false;
                //  ПРОВЕРКА  на  правильность  ввода  шагов
                if (steps.Length <= 0 || steps.Length > 4) // проверка на количетсво шагов и все сиволы - цифры
                {
                    Console.WriteLine("Неверное количество шагов (должно быть мин.1 - макс.4");
                    f = true;
                }
                if (steps == "0") { exit = false; break; }
                else if (!isNumber)
                {
                    Console.WriteLine("В Вашем выборе есть символы, отличные от цифр");
                    f = true;
                }
                else
                {
                    for (int i = 0; i < step.Length; i++)
                    {
                        if (step[i] != '1' && step[i] != '2' && step[i] != '3' && step[i] != '4' && step[i] != '5')
                        {
                            Console.WriteLine("В Ваших шагах есть поля за границами игрового поля");
                            f = true;
                            break;
                        }
                    }
                    // проверкa  на то что шаги должны быть рядом
                    for (int i = 0; i < step.Length; i++)
                    {
                        int step1 = 0;
                        int.TryParse(step[0].ToString(), out step1);

                        if (step1 - fieldRead[4].FindIndex(x => x == cp2) != 0 && step1 - fieldRead[4].FindIndex(x => x == cp2) != 2)   // ?????
                        {
                            Console.WriteLine("Шаги должны быть на соседние с игроком клетки (прыгать нельзя)");
                            Console.WriteLine($"Сейчас игрок 2 находится на позиции {((fieldRead[4].FindIndex(x => x == cp2) + 1))}");
                            f = true;
                            break;
                        }

                    }
                }

            }
            Console.WriteLine("Хороший выбор)))");
            Thread.Sleep(1000);
            int x = 0, y = 0;
            //  передвижение игрока 2 (в атаке)  и снежков
            if (exit)
            {
                for (int i = 0; i < step.Length; i++)
                {
                    //FileRead(fieldRead, path);
                    Console.Clear();
                    if (step[i] != 0)
                    {
                        int.TryParse(step[i].ToString(), out x); //  в х  записываем шаг игрока
                    }
                    //   поиск снежков

                    if (fieldRead[0].FindIndex(x => x == cs1) >= 0)  // если  * в поле с номерами
                    {
                        fieldRead[0][fieldRead[0].FindIndex(x => x == cs1)] = new CellNumber((fieldRead[0].FindIndex(x => x == cs1) + 1));
                    }
                    if (fieldRead[1].FindIndex(x => x == cs1) >= 0)  //  если * в поле 1
                    {
                        fieldRead[0][fieldRead[1].FindIndex(x => x == cs1)] = cs1;
                        fieldRead[1][fieldRead[1].FindIndex(x => x == cs1)] = c1;
                    }
                    if (fieldRead[2].FindIndex(x => x == cs1) >= 0)  //  если * в поле 2
                    {
                        fieldRead[1][fieldRead[2].FindIndex(x => x == cs1)] = cs1;
                        fieldRead[2][fieldRead[2].FindIndex(x => x == cs1)] = c1;
                    }
                    if (fieldRead[3].FindIndex(x => x == cs1) >= 0)  //  если * в поле 3
                    {
                        fieldRead[2][fieldRead[3].FindIndex(x => x == cs1)] = cs1;
                        fieldRead[3][fieldRead[3].FindIndex(x => x == cs1)] = c1;
                    }

                    //  передвижение игрока 2 и броски снежков
                    if ((x - 1) < fieldRead[4].FindIndex(x => x == cp2)) // x = 2 (x-1)=1, cp1 = 2 (left)
                    {
                        fieldRead[4][x - 1] = cp2; // step[0] = 2 - > field[4][j] - j = 1
                        fieldRead[4][x] = new CellNumber(x + 1);
                        fieldRead[3][x - 1] = cs1; // 1-й снежок вылетел
                    }
                    else if ((x) > fieldRead[4].FindIndex(x => x == cp2))  // right
                    {
                        fieldRead[4][x - 1] = cp2; // step[0] = 2 - > field[4][j] - j = 1
                        fieldRead[4][x - 2] = new CellNumber(x - 1);
                        fieldRead[3][x - 1] = cs1; // 1-й снежок вылетел
                    }
                    PrintField(fieldRead);
                    FileWrite(fieldRead, path);  //  после круга - записали в файл
                    Thread.Sleep(1000);
                    //Console.Clear(); 
                }

                //   ИГРОК  1  -  ЗАЩИТА
                f = true;
                while (f)
                {
                    Console.WriteLine("\tИгрок 1, введите свои шаги для уклонения от снежков (от 1 до 4 шагов)");
                    steps2 = Console.ReadLine(); // строка шагов
                    step = steps2.ToCharArray(); // массив символов (шаги)
                    int numVal;
                    bool isNumber = int.TryParse(steps2, out numVal);
                    f = false;
                    //  ПРОВЕРКА  на  правильность  ввода  шагов
                    if (steps2.Length <= 0 || steps2.Length > 4) // проверка на количетсво шагов и все сиволы - цифры
                    {
                        Console.WriteLine("Неверное количество шагов (должно быть мин.1 - макс.4");
                        f = true;
                    }
                    else if (steps2.Length != steps.Length)
                    {
                        Console.WriteLine($"Неверное количество шагов (должно быть такое же как у Игрока 1: {steps.Length} шага)");
                        f = true;
                    }
                    else if (!isNumber)
                    {
                        Console.WriteLine("В Вашем выборе есть символы, отличные от цифр");
                        f = true;
                    }
                    else
                    {
                        for (int i = 0; i < step.Length; i++)
                        {
                            if (step[i] != '1' && step[i] != '2' && step[i] != '3' && step[i] != '4' && step[i] != '5')
                            {
                                Console.WriteLine("В Ваших шагах есть поля за границами игрового поля");
                                f = true;
                                break;
                            }
                        }
                        for (int i = 0; i < step.Length; i++)
                        {
                            int step1 = 0;
                            int.TryParse(step[0].ToString(), out step1);

                            if (step1 - fieldRead[0].FindIndex(x => x == cp1) != 0 && step1 - fieldRead[0].FindIndex(x => x == cp1) != 2)   // ?????
                            {
                                Console.WriteLine("Шаги должны быть на соседние с игроком клетки (прыгать нельзя)");
                                Console.WriteLine($"Сейчас игрок 1 находится на позиции {((fieldRead[0].FindIndex(x => x == cp1) + 1))}");
                                f = true;
                                break;
                            }

                        }
                    }

                }
                Console.WriteLine("Хороший выбор)))");
                Thread.Sleep(1000);
                //  передвижение игрока 1 (в защите)  и  снежков
                for (int i = 0; i < step.Length; i++)
                {
                    FileRead(fieldRead, path);
                    Console.Clear();
                    if (step[i] != 0)
                    {
                        int.TryParse(step[i].ToString(), out y); //  в х  записываем шаг игрока
                    }

                    //  передвижение игрока 1 
                    if (((y - 1) < fieldRead[0].FindIndex(x => x == cp1)) && (fieldRead[0].FindIndex(x => x == cp1) != -1)) // x = 2 (x-1)=1, cp1 = 2 (left)
                    {
                        fieldRead[0][y - 1] = cp1; // step[0] = 2 - > field[4][j] - j = 1
                        fieldRead[0][y] = new CellNumber(y + 1);
                    }
                    else if ((y > fieldRead[0].FindIndex(x => x == cp1)) && (fieldRead[0].FindIndex(x => x == cp1) != -1))  // right
                    {
                        fieldRead[0][y - 1] = cp1; // step[0] = 2 - > field[4][j] - j = 1
                        fieldRead[0][y - 2] = new CellNumber(y - 1);
                    }
                    //   поиск снежков

                    if (fieldRead[0].FindIndex(x => x == cs1) >= 0)  // если  * в поле с номерами
                    {
                        fieldRead[0][fieldRead[0].FindIndex(x => x == cs1)] = new CellNumber((fieldRead[0].FindIndex(x => x == cs1) + 1));
                        if (fieldRead[0].FindIndex(x => x == cp1) < 0)
                        {
                            Console.WriteLine("Снежок попал в ИГРОКА 1 ! \n >ИГРОК  1  ПРОИГРАЛ<");
                            Console.ReadKey();
                            exit = false;
                            break;
                        }
                    }
                    if (fieldRead[1].FindIndex(x => x == cs1) >= 0)  //  если * в поле 1
                    {
                        fieldRead[0][fieldRead[1].FindIndex(x => x == cs1)] = cs1;
                        fieldRead[1][fieldRead[1].FindIndex(x => x == cs1)] = c1;

                    }
                    if (fieldRead[2].FindIndex(x => x == cs1) >= 0)  //  если * в поле 2
                    {
                        fieldRead[1][fieldRead[2].FindIndex(x => x == cs1)] = cs1;
                        fieldRead[2][fieldRead[2].FindIndex(x => x == cs1)] = c1;
                    }
                    if (fieldRead[3].FindIndex(x => x == cs1) >= 0)  //  если * в поле 3
                    {
                        fieldRead[2][fieldRead[3].FindIndex(x => x == cs1)] = cs1;
                        fieldRead[3][fieldRead[3].FindIndex(x => x == cs1)] = c1;
                    }

                    PrintField(fieldRead);
                    FileWrite(fieldRead, path);  //  после круга - записали в файл
                    Thread.Sleep(1000);

                }
            }
            


        }
    }
    




}





