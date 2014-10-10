// -----------------------------------------------------------------------
// <copyright file="TextMessageParser.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.TrackingDomain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UGitFit.Model;
    using UGitFit.Model.Interfaces;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TextMessageParser
    {
        private string _rawTextMessge = string.Empty;
        private Guid _personId;
       
        NutrtionHistoryItem _nutritionHistory = null;
        WeightHistoryItem _weightHistory = null;
        ExerciseRecord _exercise = null;

        public TextMessageParser(string rawTextMsg, Guid personId, Guid mealTypeId)
        {
            _personId = personId;
            
            if (rawTextMsg.Trim().isNullOrEmpty())
                return;

            this._rawTextMessge = rawTextMsg;
            this.ParseOutExcercise();
            this.ParseOutWeight();
            this.ParseOutNutritionHistory(mealTypeId);
           
        }

        public string ResponseText
        {
            get
            {
                return _rawTextMessge;
            }
        }

        public NutrtionHistoryItem NutritionRecord
        {
            get
            {
                return _nutritionHistory;
            }
        }

        public WeightHistoryItem WeightRecord
        {
            get
            {
                return _weightHistory;
            }
        }

        public ExerciseRecord ExerciseRecord
        {
            get
            {
                return _exercise;
            }
        }

        private void ParseOutNutritionHistory(Guid mealType)
        {
            if (_rawTextMessge.isNullOrEmpty()) 
                return;

            _nutritionHistory = new NutrtionHistoryItem() { MealTypeId = mealType, PersonId = _personId };

            string temp = this.ParseOutNumber("$:");
            if (temp.isNullOrEmpty() == false && temp.IsNumeric())
                _nutritionHistory.Cost = decimal.Parse(temp);

            temp = this.ParseOutNumber("B:");
            if (temp.isNullOrEmpty() == false && temp.IsNumeric())
                _nutritionHistory.CarbGrams = temp.ToInt(0);

            temp = this.ParseOutNumber("F:");
            if (temp.isNullOrEmpty() == false && temp.IsNumeric())
                _nutritionHistory.FatGrams = temp.ToInt(0);

            temp = this.ParseOutNumber("C:");
            if (temp.isNullOrEmpty() == false && temp.IsNumeric())
                _nutritionHistory.Calories = temp.ToInt(0);

            temp = this.ParseOutNumber("S:");
            if (temp.isNullOrEmpty() == false && temp.IsNumeric())
                _nutritionHistory.SurgarGrams = temp.ToInt(0);

            _nutritionHistory.MealText = _rawTextMessge;
        }

        private void ParseOutExcercise()
        {
            string excercise = this.ParseOutExcerciseNote("E:");
            if (excercise.isNullOrEmpty())
                excercise = this.ParseOutExcerciseNote("e:");

            if (!excercise.isNullOrEmpty())
            {
                this._exercise = new ExerciseRecord() { ExcerciseNote = excercise, PersonId = _personId };
            }
        }

        private void ParseOutWeight()
        {
            string weightString = this.ParseOutNumber("W:");
            if (weightString.isNullOrEmpty() == false && weightString.IsNumeric())
            {
                this._weightHistory = new WeightHistoryItem() { PersonId = _personId, Weight = decimal.Parse(weightString) };
            }
        }

        private string ParseOutExcerciseNote(string shortCode)
        {
            string rtnVal = null;
           

            int startIndex = _rawTextMessge.IndexOf(shortCode);
            if (startIndex > -1)
            {
                startIndex += shortCode.Length;
                int endIndex = this.IndexOfNextShortCodeInRawText(startIndex) - startIndex;
                if (endIndex <= 0)
                {
                    endIndex = _rawTextMessge.Length - startIndex;
                }

                rtnVal = _rawTextMessge.Substring(startIndex, endIndex).Trim();
                _rawTextMessge = _rawTextMessge.Replace(shortCode, "").Replace(rtnVal, "").Trim();
            }
            
            return rtnVal;
        }

        private int IndexOfNextShortCodeInRawText(int startPosition)
        {
            string[] otherShortCodes = new string[] { "S:", "s:", "F:", "f:", "C:", "c:", "B:", "b:", "$:" };
            int tempIndex = -1;
            List<int> indexes = new List<int>();

            foreach (string shortCode in otherShortCodes)
            {
                tempIndex = _rawTextMessge.IndexOf(shortCode, startPosition);
                if (tempIndex > -1)
                    indexes.Add(tempIndex);
            }

            if (indexes.Count() == 0)
                return -1;

            indexes.Sort();
            return indexes[0];
        }

        private string ParseOutNumber(string shortCode)
        {
            string rtnVal=null;

            //Check if the short code has a space following
            //(eg. w:158 or w: 158)
            if (this._rawTextMessge.Contains(shortCode + " "))
                shortCode = shortCode + " ";

            //See if the raw text contains the short code
            //(eg does raw Test contain $:)
            if (this._rawTextMessge.Contains(shortCode))
            {
                int startIndex = _rawTextMessge.IndexOf(shortCode);
                if (startIndex > -1)
                {
                    startIndex += shortCode.Length;

                    //Get the index of the next short code in the string
                    int spaceIndex = this.IndexOfNextShortCodeInRawText(startIndex);
                    if (spaceIndex < 0)
                    {
                        //There is no other shortcodes after this shortcode, 
                        //see if there is a space
                        spaceIndex = _rawTextMessge.IndexOf(" ", startIndex);
                    }

                    if (spaceIndex < startIndex)
                    {
                        //there are no other shortcodes and not spaces AFTER this shortcode (this is the last shortcode in the text msg)
                        spaceIndex = _rawTextMessge.Length;
                    }

                    //Get the text following the shortcode
                    string temp = _rawTextMessge.Substring(startIndex, spaceIndex - startIndex).Replace(shortCode, "").Replace(",", "").Replace(" ", "").Replace("-", "");
                    if (temp.IsNumeric())
                    {
                        rtnVal = temp;
                        _rawTextMessge = _rawTextMessge.Replace(shortCode + temp, "").Trim();
                    }
                }


            }

            if (rtnVal.isNullOrEmpty() && shortCode.ToUpper() == shortCode && shortCode.Contains("$") == false)
            {
                rtnVal = ParseOutNumber(shortCode.ToLower());
            }

            return rtnVal;
        }
    }
}
