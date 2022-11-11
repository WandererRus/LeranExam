namespace LeranExam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IResultTable rt = new ResultTable();
            Auth auth = new Auth();
            string action = "auth";
            IMyXmlFile xmlFile = new MyXmlFile();
            IRegister register = new Register(xmlFile);
            IEditor editor = new Editor(xmlFile);
            MassiveVictorins victorins = new MassiveVictorins();
            victorins.Load();
            
            /*editor.NewVictorina();
            editor.EditVictorina();
            IVictorina newVic = new Victorina();
            newVic = xmlFile.GetVictorina("IT.xml");*/

            
            IUser user = new User();
            string[] massiveAuthAction = new string[3] { "set для изменения настроек", "vic для запуска викторины", "table для просмотра результатов" };
            string innerAction = "";
            string setAction = "";
            while (!auth.userInSystem)
            {
                Console.WriteLine("Выберите действие, auth для авторизации, reg для регистрации:");
                action = Console.ReadLine();
                switch (action)
                {
                    case "auth":

                        Console.WriteLine("Введите логин:");
                        user.Login = Console.ReadLine();
                        Console.WriteLine("Введите пароль:");                        
                        user.Password = Console.ReadLine();
                        auth.userInSystem = auth.GetAuth(user, xmlFile);
                        break;
                    case "reg":
                        do
                        {
                            Console.WriteLine("Введите желаемый логин. Если просьба о вводе повторяется, значит данный логин уже занят.");
                            user.Login = Console.ReadLine(); 

                        }
                        while (register.ExistUser(user));
                        Console.WriteLine("Введите желаемый пароль:");
                        user.Password = Console.ReadLine();
                        if (register.SaveNewUser(user))
                            auth.userInSystem = true;
                        break;
                }
               

            }

            while (auth.userInSystem)
            {
                foreach (string str in massiveAuthAction)
                    Console.WriteLine(str);
                innerAction = Console.ReadLine();
                switch (innerAction)
                {
                    case "set": 
                        Console.WriteLine("pass для смены пароля, date для смены даты рождения и all для смены пароля и даты"); 
                        setAction = Console.ReadLine();
                        xmlFile.EditUser("users.xml", user, setAction); break;
                    case "vic": 
                        Console.WriteLine("Напишите имя викторины для ее запуска. Список викторин:"); 
                        foreach (IVictorina victorina in victorins.VictorinaList)
                        {
                            Console.WriteLine($"{victorina.Name}");
                        }
                        IVictorina currentVictorina = xmlFile.GetVictorina(Console.ReadLine() + ".xml");
                        TableItem currenttable = (TableItem)currentVictorina.Start(user);
                        rt.TableItems.Add(currenttable);
                        rt.SaveResultTable();
                        Console.WriteLine(currenttable.ToString());
                        break;
                    case "table":
                        Console.WriteLine(rt.AllResults("IT"));
                        break;
                }
            }
        }
    }

    public interface IMassiveVictorins
    {
        IMyXmlFile XmlFile { get; set; }
        List<IVictorina> VictorinaList { get; set; }
    }

    public interface IVictorina
    {
        public IMyXmlFile XmlFile { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        List<IQuestion> questions { get; set; }
        public ITableItem Start(IUser user);
        public void Save();
        public void Load();

    }

    public interface IQuestion
    {
        public string Name { set; get; }
        List<string> variants { get; set; }
        List<int> answers { get; set; }
        public string ToString();
    }

    public interface IUser
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime BirthDay { get; set; }
        public string ToString();
    }

    public interface IAuth
    {
        public bool GetAuth(IUser user, IMyXmlFile XmlFile );
  
    }

    public interface IRegister
    {
        IMyXmlFile XmlFile { get; set; }
        public bool SaveNewUser(IUser user);
        public bool ExistUser(IUser user);

    }
    public interface ISettings
    {
        public IUser ChangeUserSettingsPassword(IUser user, string password);
        public IUser ChangeUserSettingsBirthDate(IUser user, DateTime date);
        public IUser ChangeUserSettingsAll(IUser user, string password, DateTime date);
    }

    public interface IMyXmlFile
    {
        public bool ExistFile(string path);
        public IVictorina GetVictorina(string path);
        public bool SaveVictorina(IVictorina victorina);
        public IUser GetUser(string path, string login);
        public bool SaveUser(string path, IUser user, bool rewrite = false);
        public bool EditUser(string path, IUser user, string action);
        public IResultTable GetResultTable(string path);
        public bool SaveResultTable(string path, IResultTable rt);
        public IMassiveVictorins GetAllVictorins();

    }

    public interface IResultTable
    {
        public string Name { get; set; }
        IMyXmlFile XmlFile { get; set; }
        List<ITableItem> TableItems { get; set; }
        public string Path { get; set; }
        public void SaveResultTable();
        public string Top20(string name);
        public string AllResults(string name);


    }

    public interface ITableItem
    {
        public int Score { get; set; }
        public string NameUser { get; set; }
        public string NameVictorina { get; set; }
        public string ToString();

    }

    public interface IEditor
    {
        IMyXmlFile XmlFile { get; set; }
        IVictorina Victorina { get; set; }
        public void NewVictorina();

        public void SaveVictorina();

        public void EditVictorina();
    }
    
}