using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_работа
{
    class Person : User
    {
        public string name;
        public string sex;
        public int age;
        public int res;

        public int[] answers1 = new int[6] { 0, 0, 0, 0, 0, 0 };
        public string[] answers2 = new string[6] { "", "", "", "", "", "" };

        public int[] rightAnswers1 = new int[6] { 3, 2, 3, 1004, 4, 2 };
        public string[] rightAnswers2 = new string[6] { "Дело не сдвинется с места, если ничего не предпринимать", "Ласточка весну начинает, соловей кончает", "Дважды в год лето не бывает", "Зимой солнце сквозь слезы смеется", "В июне первую ягоду в рот кладут, а вторую домой несут", "Осенью скот жиреет, человек добреет" };

        public Person() : this("No name", "No sex", 0000) { }

        public Person(string ipName) : this(ipName, "No sex", 0000) { }

        public Person(string ipName, string ipSex) : this(ipName, ipSex, 0000) { }

        public Person(string ipName, string ipSex, Int16 ipAge)
        {
            this.name = ipName;
            this.sex = ipSex;
            this.age = ipAge;
            res = 0;
        }

        public void SetAnser(int ipA, int ipQ) { answers1[ipQ] = ipA; }

        public void SetAnser(string ipA, int ipQ) { answers2[ipQ] = ipA; }

        public void SetAnser(int[] ipA) { answers1 = ipA;  }

        public void SetAnser(string[] ipA) { answers2 = ipA; }

        public int[] Anser1 
        {
            get { return answers1; }
            set { this.answers1 = value; }
        }

        public string[] Anser2
        {
            get { return answers2; }
            set { this.answers2 = value; }
        }

        public int Result
        {
            set { this.res = value; }
            get 
            {
                res = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (answers1[i] == rightAnswers1[i]) res++;
                    if (answers2[i] == rightAnswers2[i]) res++;
                }
                return res;
            }
        }
    }
}
