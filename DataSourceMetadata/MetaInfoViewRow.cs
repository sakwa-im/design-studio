using System.ComponentModel;
using System.Windows.Forms;

namespace SourceMeta
{ 
    public class MetaInfoViewRow
    {

        private string _Type;
        private string _Name;
        private string _Value;
        private string _Relation;
        private string _Description;


        [Browsable(true)]
        public string Type
        {
            get {
                return this._Type;      
            }
            set
            {
                this._Type = value;
            }
        }

        [Browsable(true)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        [Browsable(true)]
        public string Value
        {
            get {
                return this._Value;  
            }
            set
            {
                this._Value = value;
            }
        }

        [Browsable(true)]
        public string Relation
        {
            get {
                return this._Relation; 
            }
            set
            {
                this._Relation = value;
            }
        }

        [Browsable(true)]
        public string Description
{
            get {
                return this._Description;      
            }
            set
            {
                this._Description = value;
            }
        }

        [Browsable(false)]
        public MetaObject MetaObject;

        public override bool Equals(object obj)
        {
            MetaInfoViewRow castObj = (obj as MetaInfoViewRow);

            return 
                (castObj.Type == this.Type &&
                 castObj.Name == this.Name &&
                 castObj.Relation == this.Relation &&
                 castObj.Description == this.Description);
        }
        
    }
}
