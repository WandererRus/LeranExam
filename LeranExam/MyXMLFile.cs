using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LeranExam
{
    public class MyXmlFile : IMyXmlFile
    {
        public bool ExistFile(string path)
        {
            bool fileExist = File.Exists(path);
            return fileExist;
        }

        public IMassiveVictorins GetAllVictorins()
        {
            MassiveVictorins mv = new MassiveVictorins();
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("viclist.xml");
            foreach (XmlElement element in xdoc.GetElementsByTagName("victorina"))
            {
                mv.VictorinaList.Add(GetVictorina(element.GetAttribute("name") + ".xml"));
            }
            return mv;
        }

        public IResultTable GetResultTable(string path)
        {
            IResultTable rt = new ResultTable();
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);
            foreach (XmlElement element in xdoc.GetElementsByTagName("tableitem"))
            {
                TableItem ti = new TableItem();
                
                ti.Score = Int32.Parse(element.GetElementsByTagName("score")[0].InnerText);
                ti.NameUser = element.GetElementsByTagName("nameuser")[0].InnerText;
                ti.NameVictorina = element.GetElementsByTagName("namevictorina")[0].InnerText;

                rt.TableItems.Add(ti);
            }
            return rt;
        }

        public IUser GetUser(string path, string login)
        {
            IUser user = new User();
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);
            foreach (XmlElement element in xdoc.GetElementsByTagName("user"))
            {

                if (element.GetElementsByTagName("login")[0].InnerText == login)
                {
                    user.Name = element.GetElementsByTagName("name")[0].InnerText;
                    user.Login = element.GetElementsByTagName("login")[0].InnerText;
                    user.Password = element.GetElementsByTagName("password")[0].InnerText;
                    user.BirthDay = Convert.ToDateTime(element.GetElementsByTagName("dateofbirth")[0].InnerText);
                }
                    

            }
            return user;
        }
        public IVictorina GetVictorina(string path)
        {
            IVictorina v = new Victorina();
            Question quest = new Question();

            XDocument xdoc = XDocument.Load(path);

            XElement vic = xdoc.Element("victorina");
            if (vic != null)
            {
                XAttribute Vname = vic.Attribute("name");
                v.Name = Vname.Value;
                v.Path = v.Name + ".xml";
                v.questions.Clear();
                foreach (XElement question in vic.Elements("question"))
                {

                    XAttribute Qname = question.Attribute("name");
                    List<string> variants = new List<string>(4);
                    List<int> answers = new List<int>(4);
                    foreach (XElement variant in question.Elements("variant"))
                    {

                        variants.Add(variant.Value);
                        answers.Add(Int32.Parse(variant.Attribute("answer").Value));
                    }
                    quest.Name = Qname.Value;
                    quest.variants = variants;
                    quest.answers = answers;
                    
                    v.questions.Add(quest);
                }
            }
            return v;
        }

        public bool SaveResultTable(string path, IResultTable rt)
        {
            XElement root;
            XmlDocument xdoc = new XmlDocument();
            if (!ExistFile(path))
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                fs.Close();
            }
            else
            {
                xdoc.Load(path);
                xdoc.RemoveAll();
            }
            
            root = new XElement("resulttable");
            foreach (TableItem ti in rt.TableItems)
            {
                XElement element = new XElement("tableitem");
                element.Add(
                    new XElement("score", ti.Score),
                    new XElement("nameuser", ti.NameUser),
                    new XElement("namevictorina", ti.NameVictorina));
                root.Add(element);
            }

            if (root.HasElements)
            {
                root.Save(path);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaveUser(string path, IUser user, bool rewrite = false)
        {
            List<IUser> users = new List<IUser>();
            XElement root;
            XmlDocument xdoc = new XmlDocument();
            if (!ExistFile(path))
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                fs.Close();
                users.Add(user);
            }
            else
            {            
                xdoc.Load("users.xml");
                foreach (XmlElement element in xdoc.GetElementsByTagName("user"))
                {
                    users.Add(new User(element.GetElementsByTagName("name")[0].InnerText,
                        element.GetElementsByTagName("login")[0].InnerText,
                        element.GetElementsByTagName("password")[0].InnerText,
                        Convert.ToDateTime(element.GetElementsByTagName("dateofbirth")[0].InnerText)));
                    
                }
                xdoc.RemoveAll();
            }
            users.Add(user);
            root = new XElement("users");
            foreach (IUser u in users)
            {
                XElement element = new XElement("user");
                element.Add(
                    new XElement("name", u.Name),
                    new XElement("login", u.Login),
                    new XElement("password", u.Password),
                    new XElement("dateofbirth", u.BirthDay));
                root.Add(element);
            }

            if (root.HasElements)
            {
                root.Save("users.xml");
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EditUser(string path, IUser user, string action)
        {
            Settings set = new Settings();
            List<IUser> users = new List<IUser>();
            XElement root;
            XmlDocument xdoc = new XmlDocument();
            int indexUser = -1;
            if (!ExistFile(path))
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                fs.Close();
                users.Add(user);
            }
            else
            {
                xdoc.Load("users.xml");
                foreach (XmlElement element in xdoc.GetElementsByTagName("user"))
                {
                    users.Add(new User(element.GetElementsByTagName("name")[0].InnerText,
                        element.GetElementsByTagName("login")[0].InnerText,
                        element.GetElementsByTagName("password")[0].InnerText,
                        Convert.ToDateTime(element.GetElementsByTagName("dateofbirth")[0].InnerText)));

                }
                xdoc.RemoveAll();
            }
            switch (action)
            {
                case "pass": Console.WriteLine("Смена пароля. Укажите новое значение."); user = set.ChangeUserSettingsPassword(user,Console.ReadLine()); break;
                case "date": Console.WriteLine("Смена даты рождения. Укажите новое значение."); user = set.ChangeUserSettingsBirthDate(user, Convert.ToDateTime(Console.ReadLine())); break;
                case "all": Console.WriteLine("Смена пароля и даты рождения. Укажите новые значения в указанном порядке."); user = set.ChangeUserSettingsAll(user, Console.ReadLine(), Convert.ToDateTime(Console.ReadLine())); break;
            }
            foreach (IUser iu in users)
            {

                if (iu.Login == user.Login)
                    indexUser = users.IndexOf(iu);
            }
            if (indexUser != -1)
                users[indexUser] = user;
            root = new XElement("users");
            foreach (IUser u in users)
            {
                XElement element = new XElement("user");
                element.Add(
                    new XElement("name", u.Name),
                    new XElement("login", u.Login),
                    new XElement("password", u.Password),
                    new XElement("dateofbirth", u.BirthDay));
                root.Add(element);
            }

            if (root.HasElements)
            {
                root.Save("users.xml");
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaveVictorina(IVictorina victorina)
        {
            try
            {
                XDocument xdoc = new XDocument();
                XElement v = new XElement("victorina");
                XAttribute vName = new XAttribute("name", victorina.Name);
                v.Add(vName);
                for (int i = 0; i < 2; i++)
                {
                    XElement vQuestion = new XElement("question");
                    XAttribute vQuestionName = new XAttribute("name", victorina.questions[i].Name);
                    vQuestion.Add(vQuestionName);
                    for (int j = 0; j < 4; j++)
                    {
                        XElement variant = new XElement("variant", victorina.questions[i].variants[j]);
                        XAttribute answer = new XAttribute("answer", victorina.questions[i].answers[j]);
                        variant.Add(answer);
                        vQuestion.Add(variant);
                    }
                    v.Add(vQuestion);
                }
                xdoc.Add(v);
                xdoc.Save(victorina.Path);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public MyXmlFile()
        {

        }
    }
}
