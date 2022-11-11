using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeranExam
{
    public class Editor : IEditor
    {
        public IMyXmlFile XmlFile { get; set; }
        public IVictorina Victorina { get; set; }

        public Editor(IMyXmlFile file)
        {
            Victorina = new Victorina();
            XmlFile = file;
        }
        public void EditVictorina()
        {
            Console.WriteLine("Укажите имя редактируемой викторины");

            Victorina = XmlFile.GetVictorina(Console.ReadLine().ToLower() + ".xml");
            Console.WriteLine("Укажите новое имя викторины или skip для перехода к вопросам");

            string answerUser = Console.ReadLine();
            if (!answerUser.Contains("skip"))
            {
                Victorina.Name = answerUser;
            }
            foreach (Question quest in Victorina.questions)
            {
                Console.WriteLine("Текущий вопрос " + quest.Name + ". Редактировать? skip если нет");
                answerUser = Console.ReadLine();
                if (!answerUser.Contains("skip"))
                {
                    quest.Name = answerUser;
                }
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine("Вопрос /n {0} /n , является {1} ответом", quest.variants[i], Convert.ToBoolean(quest.answers[i]));
                }
                Console.WriteLine("Укажите цифры редактируемых вопросов от 1 до 4 через пробел или напишите all для редактирования всех вопросов");
                answerUser = Console.ReadLine();
                if (answerUser.Contains("all"))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Console.WriteLine("Новый вопрос");
                        quest.variants[i] = Console.ReadLine();
                        Console.WriteLine("Новый ответ");
                        quest.answers[i] = Int32.Parse(Console.ReadLine());
                    }
                }
                else if (answerUser.Contains("skip"))
                {
                    continue;
                }
                else
                {

                    if (answerUser.Length > 1)
                    {
                        string[] anss;
                        anss = answerUser.Split(" ");
                        foreach (string a in anss)
                        {
                            int index = Int32.Parse(a);
                            Console.WriteLine("Новый вопрос");
                            quest.variants[index - 1] = Console.ReadLine();
                            Console.WriteLine("Новый ответ");
                            quest.answers[index - 1] = Int32.Parse(Console.ReadLine());
                        }
                    }
                    else
                    {
                        int index = Int32.Parse(answerUser);
                        Console.WriteLine("Новый вопрос");
                        quest.variants[index] = Console.ReadLine();
                        Console.WriteLine("Новый ответ");
                        quest.answers[index] = Int32.Parse(Console.ReadLine());
                    }

                }

            }
        }

        public void NewVictorina()
        {
            try
            {
                List<Question> questions = new List<Question>();
                Console.WriteLine("Укажите имя викторины");
                string name = Console.ReadLine();
                for (int i = 0; i < 2; i++)
                {
                    Question question = new Question();
                    Console.WriteLine("Укажите имя вопроса");
                    question.Name = Console.ReadLine();


                    for (int j = 0; j < 4; j++)
                    {
                        Console.WriteLine("Укажите {0} вариант ответа", j + 1);
                        question.variants[j] = Console.ReadLine();
                        Console.WriteLine("Напишите 1 если ответ правильный или 0 если не правильный");
                        question.answers[j] = Int32.Parse(Console.ReadLine());
                    }
                    questions.Add(question);
                }
                Victorina = new Victorina();
                Victorina.Name = name;
                for (int i = 0; i < 2; i++)
                {
                    Victorina.questions[i] = questions[i];
                }
                SaveVictorina();
            }
            catch (Exception ex)

            {
                Console.WriteLine(ex.Message + "/n" + ex.StackTrace + "/n" + ex.Source);
            }
        }

        public void SaveVictorina()
        {
            XmlFile.SaveVictorina(Victorina);
        }
    }
}
