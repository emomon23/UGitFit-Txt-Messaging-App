// -----------------------------------------------------------------------
// <copyright file="TranslatableEntity.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TranslatableEntity
    {
        private XmlDocument _xmlDoc = null;
        protected string _translationsXML;
        protected int _languageId=-1;

        public TranslatableEntity() { }
        public TranslatableEntity(int languageId)
        {
            _languageId = languageId;
        }
        public TranslatableEntity(int languageId, string referenceText, string xmlString)
        {
            _languageId = languageId;
            _translationsXML = xmlString;
            this.ReferenceText = referenceText;
            this.LoadXMLDoc();
        }

        [StringLength(256)] 
        public string ReferenceText {get;set;}

        public string TranslationXML
        {
            get
            {
                if (_translationsXML.isNullOrEmpty())
                {
                    _translationsXML = "<Translations></Translations>";
                }

                return _translationsXML;
            }
            set
            {
                _translationsXML = value;
                this.LoadXMLDoc();
            }
        }

        public string TranslatedText
        {
            get
            {
                return this.GetTranslation(_languageId);
            }
        }

        public string GetTranslation(int languageId)
        {
            _languageId = languageId;

            if (this._translationsXML.isNullOrEmpty())
                return this.ReferenceText;

            if (this._languageId == -1)
                return this.ReferenceText;

            XmlNode translationNode = this.FindTranslationNode(languageId);

            if (translationNode == null || translationNode.Attributes["text"] ==null)
                return this.ReferenceText;

            return translationNode.Attributes["text"].Value;

        }

        public static string GetTranslation(string xmlSnippet, int languageId)
        {
            TranslatableEntity te = new TranslatableEntity();
            te._languageId = languageId;
            te.TranslationXML = xmlSnippet;

            return te.GetTranslation(languageId);
        }

        public void SetTranslation(int langId, string translatedText)
        {
            XmlNode translationNode = this.FindTranslationNode(langId);
            if (translationNode == null)
            {
                translationNode = _xmlDoc.CreateNode(XmlNodeType.Element, "Translation", null);

                XmlAttribute att = _xmlDoc.CreateAttribute("languageId");
                att.Value = langId.ToString();

                translationNode.Attributes.Append(att);
                
                att = _xmlDoc.CreateAttribute("text");
                translationNode.Attributes.Append(att);
                _xmlDoc.DocumentElement.AppendChild(translationNode);
            }

            translationNode.Attributes["text"].Value = translatedText;
            this._translationsXML = _xmlDoc.OuterXml;
        }

        private XmlNode FindTranslationNode(int languageId)
        {
            XmlNode translationNode = null;

            try
            {
                this.LoadXMLDoc();

                foreach (XmlNode node in _xmlDoc.DocumentElement.ChildNodes)
                {
                    if (node.Attributes["languageId"].Value == languageId.ToString())
                    {
                        translationNode = node;
                        break;
                    }
                }

            }
            catch { }

            return translationNode;
        }

        private void LoadXMLDoc()
        {
            if (_xmlDoc == null)
            {
                _xmlDoc = new XmlDocument();
                _xmlDoc.LoadXml(this.TranslationXML);
            }
        }
    }
}
