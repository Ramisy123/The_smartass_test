using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;

namespace Курсовая_работа
{
    public enum TextAligment { Left, Center, Right, Justify }

    public enum BorderType { None, Single, Double }

    // класс - параграф MS Word, обертка над обьектом Range который соответствует параграфу (вставленному в документ), дает доступ к стилю текста, 
    //выравниваю, размеру шрифта (возможно дальнейшее расширение, по идее создается внутри класса документа при вставке абзаца как публичное свойство-обьект, 
    //позволяющее заполнять свои поля по необходимости
    class WordSelection : WordDoc
    {
        private Word.Range range;
        private bool insertParagrAfterText = true;
        private Word.WdParagraphAlignment savedAligment;

        public WordSelection(Word.Range inputRange) : this(inputRange, true, true)
        {
        }

        public WordSelection(Word.Range inputRange, bool insertParagrAfterText)
            : this(inputRange, insertParagrAfterText, true)
        {

        }

        public WordSelection(Word.Range inputRange, bool insertParagrAfterText, bool setDefaultStyle)
        {
            range = inputRange;

            this.insertParagrAfterText = insertParagrAfterText;

            if (setDefaultStyle)
            {
                savedAligment = range.ParagraphFormat.Alignment;
                range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                savedAligment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                range.Italic = 0;
                range.Bold = 0;
            }
            else
            {
                savedAligment = range.ParagraphFormat.Alignment;
            }
        }

        //собственно текст параграфа
        public string Text
        {
            get { return range.Text; }
            set
            {
                range.Text = value;
                // обход глюка Word, при заполнении свойства "текст" параграф затирается и текст присоединяется к предыдущему параграфу, 
                //Range начинает указывать на предыдущий параграф
                if (insertParagrAfterText)
                {
                    range.InsertParagraphAfter();
                }
                // обход глюка Word: при вставке текста выравнивание ставится по центру
                range.ParagraphFormat.Alignment = this.savedAligment;

            }
            // завершение public string Text
        }

        public void TextToString(WordDoc ipwWordDoc, string s, int d)
        {
            ipwWordDoc.SetSelectionToText(s);
            this.Text = Convert.ToString(d);
        }

        public void TextToString(WordDoc ipwWordDoc, string s, string d)
        {
            ipwWordDoc.SetSelectionToText(s);
            this.Text = d;
        }
    }
}
