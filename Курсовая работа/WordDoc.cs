using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Drawing.Printing;

namespace Курсовая_работа
{
    class WordDoc : Document
    {
        public Object missingObj = System.Reflection.Missing.Value;
        public Object trueObj = true;
        public Object falseObj = false;

        public Word._Application application;
        public Word._Document document;

        public Object templatePathObj;
        public Word.Range currentRange = null;
        public Word.Table table = null;

        private WordSelection selection;

        public WordSelection Selection
        {
            get { return selection; }
            set { throw new Exception("Ошибка! Свойство InsertedParagraph только для чтения"); }
        }

        public bool Closed
        {
            get
            {
                if (application == null || document == null) { return true; }
                else { return false; }
            }
        }

        public bool Visible
        {
            get
            {
                if (Closed) { throw new Exception("Ошибка при попытке изменить видимость Microsoft Word. Программа или документ уже закрыты."); }
                return application.Visible;

            }
            set
            {
                if (Closed) { throw new Exception("Ошибка при попытке изменить видимость Microsoft Word. Программа или документ уже закрыты."); }
                application.Visible = value;
            }
            // завершение public bool Visible  
        }
        public WordDoc(bool startVisible)
        {
            //создаем обьект приложения word
            application = new Word.Application();

            // если вылетим на этом этапе, приложение останется открытым
            try
            {
                document = application.Documents.Add(ref missingObj, ref missingObj, ref missingObj, ref missingObj);
            }
            catch (Exception error)
            {
                this.Close();
                throw error;
            }
            Visible = startVisible;

        }

        public WordDoc() : this(false) { }

        public WordDoc(string templatePath, bool startVisible)
        {
            //создаем обьект приложения word
            application = new Word.Application();

            // создаем путь к файлу используя имя файла
            templatePathObj = templatePath;

            // если вылетим не этом этапе, приложение останется открытым
            try
            {
                document = application.Documents.Add(ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);
            }
            catch (Exception error)
            {
                this.Close();
                throw error;
            }
            Visible = startVisible;

        }

        public WordDoc(string templatePath)
            : this(templatePath, false) { }

        public static void FillShowTemplate(string pathToTemplate, Action<WordDoc> fillWordDoc)
        {

            // ошибку при открытии обработает вышестоящий код формы
            WordDoc wordDoc = null;
            try
            {
                wordDoc = new WordDoc(pathToTemplate);
                fillWordDoc(wordDoc);
            }
            catch (Exception error)
            {
                if (wordDoc != null) { wordDoc.Close(); }
                throw error;
            }

            wordDoc.Visible = true;
        }
        public void SetSelectionToText(string stringToFind)
        {
            Word.Range foundRange = findRangeByString(stringToFind);
            if (foundRange == null)
            {
                throw new Exception("Ошибка при поиске текста в MS Word. Не удалось найти и выбрать заданный текст: " + stringToFind);
            }
            currentRange = foundRange;
            selection = new WordSelection(foundRange, false);
        }

        public void SetSelectionToText(string containerStr, string stringToFind)
        {

            Word.Range containerRange = null;
            Word.Range foundRange = null;

            containerRange = findRangeByString(containerStr);
            if (containerRange != null)
            {
                foundRange = findRangeByString(containerRange, stringToFind);
            }

            if (foundRange != null)
            {
                currentRange = foundRange;
                selection = new WordSelection(foundRange, false);
            }
            else
            {
                throw new Exception("Ошибка при поиске текста в MS Word. Не удалось найти заданную область для поиска текста: " + containerStr);
            }
        }

        public void InsertFile(string pathToFile)
        {
            if (currentRange == null) { throw new Exception("Ничего не выбрано"); }
            currentRange.InsertFile(pathToFile);
        }

        public void Save(string pathToSave)
        {
            Object pathToSaveObj = pathToSave;
            document.SaveAs(ref pathToSaveObj, Word.WdSaveFormat.wdFormatDocument);
        }

        public void Close()
        {

            if (document != null)
            {
                document.Close(ref falseObj, ref missingObj, ref missingObj);
            }
            application.Quit(ref missingObj, ref missingObj, ref missingObj);
            document = null;
            application = null;
        }

        // поиск строки и ее замена на заданную строку
        public void ReplaceAllStrings(string strToFind, string replaceStr)
        {
            if (Closed) { throw new Exception("Ошибка при обращении к документу Word. Документ уже закрыт."); }

            object strToFindObj = strToFind;
            object replaceStrObj = replaceStr;
            Word.Range wordRange;
            object replaceTypeObj;

            replaceTypeObj = Word.WdReplace.wdReplaceAll;

            try
            {
                for (int i = 1; i <= document.Sections.Count; i++)
                {
                    wordRange = document.Sections[i].Range;
                    Word.Find wordFindObj = wordRange.Find;
                    object[] wordFindParameters = new object[15] { strToFindObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, replaceStrObj, replaceTypeObj, missingObj, missingObj, missingObj, missingObj };
                    wordFindObj.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, wordFindObj, wordFindParameters);
                }
            }
            catch (Exception error)
            {
                throw new Exception("Ошибка при выполнении замене всех строк  в документе Word.  " + error.Message + " (ReplaceAllStrings)");
            }
        }

        private Word.Range findRangeByString(string stringToFind)
        {
            if (Closed) { throw new Exception("Ошибка при обращении к документу Word. Документ уже закрыт."); }
            object stringToFindObj = stringToFind;
            Word.Range wordRange;
            bool rangeFound;

            for (int i = 1; i <= document.Sections.Count; i++)
            {
                wordRange = document.Sections[i].Range;
                Word.Find wordFindObj = wordRange.Find;
                object[] wordFindParameters = new object[15] { stringToFindObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj };
                rangeFound = (bool)wordFindObj.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, wordFindObj, wordFindParameters);
                if (rangeFound) { return wordRange; }
            }

            // если ничего не нашли, возвращаем null
            return null;
        }

        private Word.Range findRangeByString(Word.Range containerRange, string stringToFind)
        {
            if (Closed) { throw new Exception("Ошибка при обращении к документу Word. Документ уже закрыт."); }
            object stringToFindObj = stringToFind;
            bool rangeFound;
            Word.Find wordFindObj = containerRange.Find;
            object[] wordFindParameters = new object[15] { stringToFindObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj };
            rangeFound = (bool)wordFindObj.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, wordFindObj, wordFindParameters);
            if (rangeFound) { return containerRange; }
            else { return null; }

        }
        public void TextToTemplate(Person pers)
        {
            this.SetSelectionToText("#И");
            this.Selection.Text = Convert.ToString(pers.name);
            this.SetSelectionToText("#П");
            this.Selection.Text = Convert.ToString(pers.sex);
            this.SetSelectionToText("#В");
            this.Selection.Text = Convert.ToString(pers.age);
            this.SetSelectionToText("#1");
            this.Selection.Text = Convert.ToString(pers.answers1[0]);
            this.SetSelectionToText("#2");
            this.Selection.Text = Convert.ToString(pers.answers1[1]);
            this.SetSelectionToText("#3");
            this.Selection.Text = Convert.ToString(pers.answers1[2]);
            this.SetSelectionToText("#4");
            this.Selection.Text = Convert.ToString(pers.answers1[3]);
            this.SetSelectionToText("#5");
            this.Selection.Text = Convert.ToString(pers.answers1[4]);
            this.SetSelectionToText("#6");
            this.Selection.Text = Convert.ToString(pers.answers1[5]);
            this.SetSelectionToText("#П1");
            this.Selection.Text = Convert.ToString(pers.answers2[0]);
            this.SetSelectionToText("#П2");
            this.Selection.Text = Convert.ToString(pers.answers2[1]);
            this.SetSelectionToText("#П3");
            this.Selection.Text = Convert.ToString(pers.answers2[2]);
            this.SetSelectionToText("#П4");
            this.Selection.Text = Convert.ToString(pers.answers2[3]);
            this.SetSelectionToText("#П5");
            this.Selection.Text = Convert.ToString(pers.answers2[4]);
            this.SetSelectionToText("#П6");
            this.Selection.Text = Convert.ToString(pers.answers2[5]);
            this.SetSelectionToText("#Р");
            this.Selection.Text = Convert.ToString(pers.res);
        }

        public void TextToFree(Person pers)
        {
            this.SetSelectionToText("#1");
            this.Selection.Text = Convert.ToString(pers.answers1[0]);
            this.SetSelectionToText("#2");
            this.Selection.Text = Convert.ToString(pers.answers1[1]);
            this.SetSelectionToText("#3");
            this.Selection.Text = Convert.ToString(pers.answers1[2]);
            this.SetSelectionToText("#4");
            this.Selection.Text = Convert.ToString(pers.answers1[3]);
            this.SetSelectionToText("#5");
            this.Selection.Text = Convert.ToString(pers.answers1[4]);
            this.SetSelectionToText("#6");
            this.Selection.Text = Convert.ToString(pers.answers1[5]);
            this.SetSelectionToText("#П1");
            this.Selection.Text = Convert.ToString(pers.answers2[0]);
            this.SetSelectionToText("#П2");
            this.Selection.Text = Convert.ToString(pers.answers2[1]);
            this.SetSelectionToText("#П3");
            this.Selection.Text = Convert.ToString(pers.answers2[2]);
            this.SetSelectionToText("#П4");
            this.Selection.Text = Convert.ToString(pers.answers2[3]);
            this.SetSelectionToText("#П5");
            this.Selection.Text = Convert.ToString(pers.answers2[4]);
            this.SetSelectionToText("#П6");
            this.Selection.Text = Convert.ToString(pers.answers2[5]);
            this.SetSelectionToText("#Р");
            this.Selection.Text = Convert.ToString(pers.res);
        }

    }
}

