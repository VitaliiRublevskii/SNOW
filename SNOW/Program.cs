

using SNOW;

string path = "myFile.txt";


Cell c1 = new Cell();                        // Пустая ячейка

CellWithPlayer cp1 = new CellWithPlayer();  // Ячейка с игроком 1
CellWithPlayer cp2 = new CellWithPlayer();  // Ячейка с игроком 2

CellWithSnow cs1 = new CellWithSnow();      // Ячейка со снежком

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
            if (j == 2) { field[i].Add(cp1); }
            else { field[i].Add(new CellNumber(j + 1)); }
        }
    }
    else
    {
        field.Add(new List<Cell>(5));
        for (int j = 0; j < 5; j++)
        {
             field[i].Add(new Cell()); 
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
            cell1.Add(new Cell(split[j]));
        }
        fieldRead.Add(cell1);
    }
}

//  ФУНКЦИЯ  :  Вывод  поля  в  консоль
void PrintField(List<List<Cell>> fieldGame)
{
    Console.WriteLine("\t\tСНЕЖКИ:");
    Console.WriteLine();
    Console.WriteLine("\tИгрок  1:");
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
    Console.WriteLine("\tИгрок  2:");
}


FileWrite(field, path);
Console.WriteLine("Записалось начальное поле field в файл");
Console.ReadKey();
FileRead (fieldRead, path);
Console.WriteLine("Считалось начальное поле из файла в fieldRead");
Console.ReadKey();
PrintField(fieldRead);
Console.ReadKey();

fieldRead[2][3] = cs1;
Console.WriteLine("Поменяли ячейку в fieldRead  пустое на звездочку");
Console.ReadKey();
FileWrite(fieldRead, path);

Console.WriteLine(" fieldRead  записали в файл");
Console.ReadKey();

FileRead(fieldRead, path);
Console.ReadKey();

PrintField(fieldRead);


/*
while (true)
{
    PrintField();
    Console.WriteLine("Игрок 1, введите свои шаги (от 1 до 4 шагов)");

    string steps = Console.ReadLine(); // строка шагов
    char[] step = steps.ToCharArray(); // массив символов (шаги)
    int numVal;
    bool isNumber = int.TryParse(steps, out numVal);
    
    if (steps.Length <= 0 || steps.Length > 4) // проверка на количетсво шагов и все сиволы - цифры
    {
        Console.WriteLine("Неверное количество шагов (должно быть мин.1 - макс.4");
        break;
    }
    else if (!isNumber)
    {
        Console.WriteLine("В Вашем выборе есть символы, отличные от цифр");
        break;
    }
    else 
    {
        for (int i = 0; i < step.Length; i++)
        {
            if (step[i] != '1' && step[i] != '2' && step[i] != '3' && step[i] != '4' && step[i] != '5')
            {
                Console.WriteLine("В Ваших шагах есть поля за границами игрового поля");
                break;
            }
            //else if ((int)step[i] - (int)step[i-1] != 1 || (int)step[i] - (int)step[i - 1] == 0) 
            //{
            //    Console.WriteLine("Шаги должны быть на соседне клетки");
            //}
        }
    }
    
    Console.WriteLine("Выбор правильный");
    Console.ReadKey();

    for (int i = 0; i < 1;i++) 
    {
        
        int x1;
        int.TryParse(step[0].ToString(), out x1); // 2
        int x2;
        int.TryParse(step[1].ToString(), out x2);  // 1  
        int x3;
        int.TryParse(step[2].ToString(), out x3);  // 2

        if (x1 <= field[4].FindIndex(x => x == cp1)) // cp1 в начале игры должно быть 2
        {
            field[4][x1 - 1] = new CellWithPlayer(); // step[0] = 2 - > field[4][j] - j = 1
            field[4][x1] = new CellNumber(x1 + 1);  // откуда ушел игрок - делаем ячейкой с номером
            field[3][x1- 1] = new CellWithSnow(); // 1-й снежок вылетел
            PrintField();
            Console.ReadKey();



            field[4][x2 - 1] = new CellWithPlayer(); // step[0] = 2 - > field[4][j] - j = 1
            field[4][x2] = new CellNumber(x1);  // откуда ушел игрок - делаем ячейкой с номером
            field[2][x1 - 1] = new CellWithSnow();  // 1- снежок второй полет
            field[3][x1 - 1] = new Cell();           // отсюда 1-й снежок улетел
            field[3][x2 - 1] = new CellWithSnow();  //  2- снежок вылетел
            PrintField();
            Console.ReadKey();

            field[4][x3 - 1] = new CellWithPlayer(); // step[0] = 2 - > field[4][j] - j = 1
            field[4][x2 - 1] = new CellNumber(x2);  // откуда ушел игрок - делаем ячейкой с номером
            field[1][x1 - 1] = new CellWithSnow(); // 1-й снежок
            field[2][x1 - 1] = new Cell();  // 1- снежок улетел
            field[2][x2 - 1] = new CellWithSnow(); // 2-й снежок
            field[3][x2 - 1] = new Cell();  //  2- снежок улетел
            field[3][x3 - 1] = new CellWithSnow(); // 3-й снежок вылетел
            PrintField();
            Console.ReadKey();
        }
        else {
            field[4][x1 - 1] = new CellWithPlayer(); // step[0] = 4 - > field[4][j] - j = 3 
            field[4][x1 - 2] = new CellNumber(x1 - 1);  // откуда ушел игрок - делаем ячейкой с номером
            field[3][x1 - 1] = new CellWithSnow(); // 1-й снежок вылетел
            PrintField();
            Console.ReadKey();



            field[4][x2 - 1] = new CellWithPlayer(); // step[0] = 5 - > field[4][j] - j = 4
            field[4][x2 - 2] = new CellNumber(x1);  // откуда ушел игрок - делаем ячейкой с номером
            field[2][x1 - 1] = new CellWithSnow();  // 1- снежок второй полет
            field[3][x1 - 1] = new Cell();           // отсюда 1-й снежок улетел
            field[3][x2 - 1] = new CellWithSnow();  //  2- снежок вылетел
            PrintField();
            Console.ReadKey();

            field[4][x3 - 1] = new CellWithPlayer(); // step[0] = 2 - > field[4][j] - j = 1
            field[4][x2 - 1] = new CellNumber(x2);  // откуда ушел игрок - делаем ячейкой с номером
            field[1][x1 - 1] = new CellWithSnow(); // 1-й снежок
            field[2][x1 - 1] = new Cell();  // 1- снежок улетел
            field[2][x2 - 1] = new CellWithSnow(); // 2-й снежок
            field[3][x2 - 1] = new Cell();  //  2- снежок улетел
            field[3][x3 - 1] = new CellWithSnow(); // 3-й снежок вылетел
            PrintField();
            Console.ReadKey();
        }
        
        
        

    }







    



}


 
*/


/*
//  Вывод после считывания
for (int i = 0; i < 5; i++)
{
    for (int j = 0; j < 5; j++)
    {
        fieldRead[i][j].PrintCell();
    }
    Console.WriteLine();
}
*/



/*
List<Cell> c = new List<Cell>();

for (int i = 0; i < 5; i++)
{
    c.Add(new CellWithPlayer());
    
}

foreach (var item in c)
{
    item.PrintCell();
}

foreach (var fs in field)
{
    foreach (var f in fs)
    {
        f.PrintCell();

    }
    Console.WriteLine();
}





//  ЗАПИСЬ поля в файл  (1-й вариант поэлементам)
for (int i = 0; i < 5; i++)
{

    for (int j = 0; j < 5; j++)
    {
        File.AppendAllText(path, (field[i][j].S + "\n")); // запись в файл
    }
}
*/