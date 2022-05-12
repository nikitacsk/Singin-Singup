string pathacc = "/Users/nikitacsk/Documents/account.txt"; // путь для macOs
string pathtime = "/Users/nikitacsk/Documents/time.txt";
string pathinfo = "/Users/nikitacsk/Documents/test.txt";

List<Person> children = new List<Person> {};
string[] infoacc = File.ReadAllLines(pathacc); // считование файла с логином и паролем
for(int i=0; i<infoacc.Length; i+=2)
{
    children.Add(new Person(infoacc[i], infoacc[i + 1]));
}


Console.Write("Sing in or Sing up: "); // регестрироваться или войти
string choice = Console.ReadLine();

if (choice == "Sing in")
{
    int index = -1;
    bool checker = false;
    while (checker == false) // проверка ли существует такой аккаунт с паролем
    {
        Console.Write("Login: ");
        string login = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();
        foreach(Person p in children)
        {
            index++;
            if(p.Login == login && p.Password == password)
            {
                checker = true;
                string[] time = File.ReadAllLines(pathtime);
                time[index * 2] = $"{login} input:{DateTime.Now}";
                File.WriteAllLines(pathtime, time);
                break;
            }
        }
        if (checker == false) Console.WriteLine("Wrong login or password");
    }

    string action = " "; // выбор дейстий на аккаунте просмотреть личную информацию или выйти с системы
    while (action != "exit")
    {
        Console.Write("What do you want do?(view my information or exit)");
        action = Console.ReadLine();
        if (action == "view my information")
        {
            string[] info = File.ReadAllLines(pathinfo);
            Console.WriteLine(info[index]);
        }
        else if(action != "exit")Console.WriteLine("Unknown command");
    }
    if (action == "exit")
    {
        string[] time = File.ReadAllLines(pathtime);
        time[index * 2 + 1] = $"{children[index].Login} output:{DateTime.Now}";
        File.WriteAllLines(pathtime, time);
        Console.WriteLine("Finish program");
    }
}
else if (choice == "Sing up")
{
    Console.Write("Login: "); // регестрация нового пользователя 
    string? hlog = Console.ReadLine();
    while(hlog == null)
    {
        Console.WriteLine("Login must be not null ");
        hlog = Console.ReadLine();
    }
    Console.Write("Password: ");
    string? hpas = Console.ReadLine();
    while (hpas == null)
    {
        Console.WriteLine("Password must be not null ");
        hpas = Console.ReadLine();
    }
    children.Add(new Person(hlog, hpas)); // добавление нового пользователя в масив классов и файл с логинами и паролями
    await File.AppendAllTextAsync(pathacc, hlog);
    await File.AppendAllTextAsync(pathacc, "\n");
    await File.AppendAllTextAsync(pathacc, hpas);
    await File.AppendAllTextAsync(pathacc, "\n");

    Console.Write("Write information about you: "); // добавление личной информации
    string information = Console.ReadLine();
    await File.AppendAllTextAsync(pathinfo, information);
    await File.AppendAllTextAsync(pathinfo,  "\n");

    await File.AppendAllTextAsync(pathtime, $"{hlog} input: {DateTime.Now}");
    await File.AppendAllTextAsync(pathtime, "\n");

    string action = " "; // 
    while (action != "exit")
    {
        Console.WriteLine("What do you want do?(view my information or exit)");
        action = Console.ReadLine();
        if (action == "view my information")
        {
            string[] info = File.ReadAllLines(pathinfo);
            Console.WriteLine(info[info.Length - 1]);
        }
        else if(action != "exit") Console.WriteLine("Unknown command");
    }
    if (action == "exit")
    {
        await File.AppendAllTextAsync(pathtime, $"{hlog} output: {DateTime.Now}");
        await File.AppendAllTextAsync(pathtime, "/n");
        Console.WriteLine("Finish program");
    }

}
else Console.WriteLine("Unknown  command");

class Person
{
    public string Login { get; set; }
    public string Password { get; set; }
    public Person (string login, string password)
    {
        Login = login;
        Password = password;
    }
}